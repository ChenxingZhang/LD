using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map{

	public bool[,,] mapData = new bool[20,20,20];  // true for walkable
	public int[,,] tempMark = new int[20,20,20];
	// Use this for initialization
	public Map()
	{
		
	}

	void InitializeMap()
	{
		//initialize the two arrays.
		//put random blocks onto the map.
		//half of the map tiles will be solid block.
		for (int i = 0; i < 20; i ++)
		{
			for ( int j = 0; j < 20 ; j ++)
			{
				for (int k = 0; k < 20 ; k ++)
				{
					mapData[i,j,k] = (Random.Range (0 , 1) == 0);
					tempMark[i,j,k] = 0;
				}
			}
		}

		//check cavities
		for (int i = 0; i < 20; i ++)
		{
			for ( int j = 0; j < 20 ; j ++)
			{
				for (int k = 0; k < 20 ; k ++)
				{
					if (mapData[i,j,k])
					{
						CheckCavity
					}
				}
			}
		}

	}

	void ResetMarks()
	{
		for (int i = 0; i < 20; i ++)
		{
			for ( int j = 0; j < 20 ; j ++)
			{
				for (int k = 0; k < 20 ; k ++)
				{
					tempMark [i, j, k] = 0;
				}
			}
		}
	}

	void CheckCavity()
	{
		
	}


}
