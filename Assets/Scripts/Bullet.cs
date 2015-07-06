using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public float outRange = 120f;
	public float speed = 20f;
	public int damage = 10;

	private Rigidbody body;
	private Vector3 forward;

	private Tank owner;

	public void SetOwner(Tank tank)
	{
		this.owner = tank;
	}
	
	void Start () 
	{
		this.body = this.GetComponent<Rigidbody>();
		this.forward = this.transform.forward;
	}
	
	void FixedUpdate () 
	{
		var deltaMove = this.speed * this.forward * Time.deltaTime;
		var position = this.body.position + deltaMove;

		this.body.MovePosition(position);	

		if (position.magnitude > this.outRange) 
		{
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Tank") 
		{
			var tank = other.GetComponent<Tank>();
			if (tank != null) 
			{
				this.Hit(tank);
			}
		}
	}

	private void Hit(Tank tank)
	{
		if(this.owner == null || this.owner != tank && (this.owner.team == 0 || this.owner.team != tank.team))
		{
			tank.Hit(this.damage);
			Destroy(this.gameObject);
		}
	}
}
