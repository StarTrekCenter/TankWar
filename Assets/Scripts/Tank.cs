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
	public int team;

	public int health;

	private MovingState movingForward = MovingState.None;
	private MovingState turnBodyRight = MovingState.None;
	private MovingState turnHeadRight = MovingState.Negative;

	private Rigidbody body;
	private Animator anim;

	private float fireTime;
	private bool canFire;
	private bool isDead;

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
		if (!this.isDead && this.canFire) 
		{
			var bullet = Instantiate(this.bullet, this.muzzle.position, this.muzzle.rotation) as Bullet;
			bullet.SetOwner(this);

			this.canFire = false;
			Invoke("CanFire", this.fireDelay);
		}
	}

	public void Hit(int damage)
	{
		this.health -= damage;

		if (this.health <= 0) 
		{
			this.Die();
		}
	}

	public void RemoveItself()
	{
		Destroy(this.gameObject);
	}

	void Start () 
	{
		this.body = this.GetComponent<Rigidbody>();
		this.anim = this.GetComponent<Animator>();

		this.canFire = true;
		this.isDead = false;

		GameManager.AddTank(this);
	}
	
	void FixedUpdate () 
	{
		if (this.isDead) 
		{
			return;
		}

		var deltaMove = (float) this.movingForward * this.movingSpeed * this.transform.forward * Time.deltaTime;
		this.body.MovePosition(this.body.position + deltaMove);
		
		Quaternion deltaBodyRotation = Quaternion.Euler((float)this.turnBodyRight * this.bodyAngularSpeed * this.transform.up * Time.deltaTime);
		this.body.MoveRotation(this.body.rotation * deltaBodyRotation);

		var deltaHeadRotation = -(float)this.turnHeadRight * this.headAngularSpeed * this.transform.up * Time.deltaTime;
		this.head.Rotate(deltaHeadRotation);
	}
	
	public static MovingState NumberToMovingState(float number)
	{
		return number > 0 
			? MovingState.Positive
				: number < 0 
				? MovingState.Negative
				: MovingState.None;
	}
	
	public static MovingState BoolToMovingState(bool positive, bool nagative)
	{
		return positive
			? MovingState.Positive
				: nagative
				? MovingState.Negative
				: MovingState.None;
	}

	private void Die()
	{
		this.isDead = true;
		this.anim.SetTrigger("Destory");
		GameManager.RemoveTank(this);
	}
	
	private void CanFire()
	{
		this.canFire = true;
	}
}
