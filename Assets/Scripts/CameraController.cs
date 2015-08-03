using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public float moveStep = 2f;
	public float scaleStep = 1.1f;
	public float xMoveRate = 0.5f;
	public float yMoveRate = 0.866f;

	Vector3 originPosition;
	float originSize;
	Camera camera;

	Vector3 tempPosition;
	
	void Start () 
	{
		this.camera = this.GetComponent<Camera> ();

		this.originPosition = this.transform.position;
		this.originSize = this.camera.orthographicSize;
		this.tempPosition = new Vector3 ();
	}
	
	public void Up()
	{
		var currentPosition = this.transform.position;
		tempPosition.Set (
			currentPosition.x + this.moveStep * this.xMoveRate, 
			currentPosition.y + this.moveStep * this.yMoveRate,
			currentPosition.z);

		this.transform.position = tempPosition;
	}
	
	public void Down()
	{
		var currentPosition = this.transform.position;
		tempPosition.Set (
			currentPosition.x - this.moveStep * this.xMoveRate, 
			currentPosition.y - this.moveStep * this.yMoveRate,
			currentPosition.z);
		
		this.transform.position = tempPosition;
	}
	
	public void Left()
	{
		var currentPosition = this.transform.position;
		tempPosition.Set (
			currentPosition.x, 
			currentPosition.y,
			currentPosition.z + this.moveStep);
		
		this.transform.position = tempPosition;
	}
	
	public void Right()
	{
		var currentPosition = this.transform.position;
		tempPosition.Set (
			currentPosition.x, 
			currentPosition.y,
			currentPosition.z - this.moveStep);
		
		this.transform.position = tempPosition;
	}

	public void Out()
	{
		this.camera.orthographicSize /= this.scaleStep;
	}

	public void In()
	{
		this.camera.orthographicSize *= this.scaleStep;
	}

	public void Reset()
	{
		this.transform.position = this.originPosition;
		this.camera.orthographicSize = this.originSize;
	}
}
