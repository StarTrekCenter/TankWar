using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour 
{

	private Tank tank;

	void Start () 
	{
		this.tank = this.GetComponent<Tank>();
	}
	
	void Update () 
	{
		var horizontal = Input.GetAxisRaw("Horizontal");
		this.tank.TurnBodyRight(this.NumberToMovingState(horizontal));

		var vertical = Input.GetAxisRaw("Vertical");
		this.tank.MoveForward(this.NumberToMovingState(vertical));

		var fire = Input.GetAxis("Fire1");
		if (fire > 0) 
		{
			this.tank.Fire();
		}

		var headTurnRight = Input.GetKey(KeyCode.Q);
		var headTurnLeft = Input.GetKey(KeyCode.E);
		this.tank.TurnHeadRight(this.BoolToMovingState(headTurnRight, headTurnLeft));
		
	}

	private MovingState NumberToMovingState(float number)
	{
		return number > 0 
			? MovingState.Positive
			: number < 0 
				? MovingState.Negative
				: MovingState.None;
	}
	
	private MovingState BoolToMovingState(bool positive, bool nagative)
	{
		return positive
			? MovingState.Positive
				: nagative
				? MovingState.Negative
				: MovingState.None;
	}
}
