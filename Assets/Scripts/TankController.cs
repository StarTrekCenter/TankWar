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
		this.tank.TurnBodyRight(Tank.NumberToMovingState(horizontal));

		var vertical = Input.GetAxisRaw("Vertical");
		this.tank.MoveForward(Tank.NumberToMovingState(vertical));

		var fire = Input.GetAxis("Fire1");
		if (fire > 0) 
		{
			this.tank.Fire();
		}

		var headTurnRight = Input.GetKey(KeyCode.Q);
		var headTurnLeft = Input.GetKey(KeyCode.E);
		this.tank.TurnHeadRight(Tank.BoolToMovingState(headTurnRight, headTurnLeft));
	}
}
