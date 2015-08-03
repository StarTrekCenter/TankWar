using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public Tank playerTank;
	public Tank enimyTank;
	public int enimyNumber = 10;
	public float playerGroundSize = 30f;
	public float tankSize = 4f;

	public int teamMemberNumber = 5;
	
	
	public IList<Tank> tanks;

	private Vector3 tankPosition;

	public static void AddTank(Tank tank)
	{
		instance.tanks.Add(tank);
	}

	public static void RemoveTank(Tank tank)
	{
		instance.tanks.Remove(tank);
	}

	void Awake ()
	{
		if (instance == null) 
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);

		this.tanks = new List<Tank>();

		this.InitTwoTeams();
		// this.InitOnePlayerAndRandomEnemy();
	}

	void InitTwoTeams()
	{
		this.tanks.Clear();

		var leftSide = new Rect (-this.playerGroundSize, -this.playerGroundSize, 2f * this.playerGroundSize, 0.8f * this.playerGroundSize);
		var rightSide = new Rect (this.playerGroundSize, 0.2f * this.playerGroundSize, -2f * this.playerGroundSize, 0.8f * this.playerGroundSize);

		for (int i = 0; i < this.teamMemberNumber; i++) 
		{
			Vector3 position = getValidRoomPosition (leftSide);
			
			var tank = Instantiate(this.playerTank, position, this.RandomTankQuaternion()) as Tank;
			tank.team = 1;
		}

		for (int i = 0; i < this.teamMemberNumber; i++) 
		{
			Vector3 position = getValidRoomPosition (rightSide);
			
			var tank = Instantiate(this.enimyTank, position, this.RandomTankQuaternion()) as Tank;
			tank.team = 2;
		}
	}

	void InitOnePlayerAndRandomEnemy()
	{
		this.tanks.Clear();

		this.tankPosition = new Vector3(0, 0, 0);
		Instantiate(this.playerTank, tankPosition, Quaternion.identity);

		for (int i = 0; i < this.enimyNumber; i++) 
		{
			Vector3 position = getValidRoomPosition ();

			Instantiate(this.enimyTank, position, this.RandomTankQuaternion());
		}
	}

	Vector3 getValidRoomPosition ()
	{
		return this.getValidRoomPosition (new Rect (-this.playerGroundSize, -this.playerGroundSize, 2 * this.playerGroundSize, 2 * this.playerGroundSize));
	}

	Vector3 getValidRoomPosition (Rect rect)
	{
		Vector3 position;
		var positionSuccess = true;
		
		var maxCount = 100;
		var count = 0;
		do {
			position = this.RandomTankPosition (rect);
			foreach (var tank in this.tanks) 
			{
				if ((tank.transform.position - position).magnitude < this.tankSize) 
				{
					positionSuccess = false;
					break;
				}
			}
			count++;
		}
		while (!positionSuccess && count < maxCount);

		return position;
	}

	private Vector3 RandomTankPosition(Rect rect)
	{
		this.tankPosition.Set(Random.Range(rect.xMin, rect.xMax), 0, Random.Range(rect.yMin, rect.yMax));
		return this.tankPosition;
	}

	private Quaternion RandomTankQuaternion()
	{
		this.tankPosition.Set(Random.Range(-1, 1), 0, Random.Range(-1, 1));
		return Quaternion.Euler(this.tankPosition);
	}
}
