using UnityEngine;
using System.Collections;

public enum MovingState
{
	None = 0,
	Positive = 1,
	Negative = -1
}

public class Tank : MonoBehaviour 
{
	public float movingSpeed = 2f;
	public float bodyAngularSpeed = 30f;
	public float headAngularSpeed = 45f;
	public float fireDelay = 0.25f;
	public Transform muzzle;
	public Transform head;
	public Bullet bullet;

	private MovingState movingForward = MovingState.None;
	private MovingState turnBodyRight = MovingState.None;
	private MovingState turnHeadRight = MovingState.Negative;

	private Rigidbody body;

	private float fireTime;
	private bool canFire;

	public void MoveForward(MovingState state)
	{
		this.movingForward = state;
	}

	public void TurnBodyRight(MovingState state)
	{
		this.turnBodyRight = state;
	}
	
	public void TurnHeadRight(MovingState state)
	{
		this.turnHeadRight = state;
	}

	public void Fire()
	{
		if (this.canFire) 
		{
			Instantiate(this.bullet, this.muzzle.position, this.muzzle.rotation);
			this.canFire = false;
			Invoke("CanFire", this.fireDelay);
		}
	}

	void Start () 
	{
		this.body = this.GetComponent<Rigidbody>();
		this.canFire = true;
	}
	
	void FixedUpdate () 
	{
		var deltaMove = (float) this.movingForward * this.movingSpeed * this.transform.forward * Time.deltaTime;
		this.body.MovePosition(this.body.position + deltaMove);
		
		Quaternion deltaBodyRotation = Quaternion.Euler((float)this.turnBodyRight * this.bodyAngularSpeed * this.transform.up * Time.deltaTime);
		this.body.MoveRotation(this.body.rotation * deltaBodyRotation);

		var deltaHeadRotation = -(float)this.turnHeadRight * this.headAngularSpeed * this.transform.up * Time.deltaTime;
		this.head.Rotate(deltaHeadRotation);
	}
	
	private void CanFire()
	{
		this.canFire = true;
	}
}
