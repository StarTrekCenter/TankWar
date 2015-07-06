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

		this.Init();
	}

	void Init()
	{
		this.tanks.Clear();

		this.tankPosition = new Vector3(0, 0, 0);
		Instantiate(this.playerTank, tankPosition, Quaternion.identity);

		for (int i = 0; i < this.enimyNumber; i++) 
		{
			var positionSuccess = true;
			var maxCount = 100;
			var count = 0;
			Vector3 position;
			do
			{
				position = this.RandomTankPosition();
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
			while(!positionSuccess && count < maxCount);

			Instantiate(this.enimyTank, position, this.RandomTankQuaternion());
		}
	}

	private Vector3 RandomTankPosition()
	{
		this.tankPosition.Set(Random.Range(-playerGroundSize, playerGroundSize), 0, Random.Range(-playerGroundSize, playerGroundSize));
		return this.tankPosition;
	}

	private Quaternion RandomTankQuaternion()
	{
		this.tankPosition.Set(Random.Range(-1, 1), 0, Random.Range(-1, 1));
		return Quaternion.Euler(this.tankPosition);
	}
}
