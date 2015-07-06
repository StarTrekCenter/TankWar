using UnityEngine;
using System.Collections;

public class RandomTank : MonoBehaviour 
{
	public float changeStatusDelay = 1f;

	private Tank tank;

	private MovingState forward;
	private MovingState turnBody;
	private MovingState turnHead;
	private bool fire;

	void Start () 
	{
		this.tank = this.GetComponent<Tank>();

		this.ChangeStatus();
	}
	
	void Update () 
	{
		this.tank.MoveForward(this.forward);
		this.tank.TurnBodyRight(this.turnBody);
		this.tank.TurnHeadRight(this.turnHead);
		
		if (this.fire) 
		{
			this.tank.Fire();
		}
	}

	private void ChangeStatus()
	{
		this.forward = Tank.NumberToMovingState(Random.Range(-1, 1));
		this.turnBody = Tank.NumberToMovingState(Random.Range(-1, 1));
		this.turnHead = Tank.NumberToMovingState(Random.Range(-1, 1));
		this.fire = Random.Range(0, 2) == 1;
		
		Invoke("ChangeStatus", this.changeStatusDelay);
	}
}
