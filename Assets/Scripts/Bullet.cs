using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public float outRange = 120f;
	public float speed = 20f;

	private Rigidbody body;
	private Vector3 forward;
	
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
}
