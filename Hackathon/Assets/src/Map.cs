using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{

	public bool[,,] mapData = new bool[20, 20, 20];
	// true for walkable
	bool[,,] tempMark = new bool[20, 20, 20];

	int cavityCount;
	Vector3 tempV3;
	// Use this for initialization
	public Map ()
	{
		InitializeMap ();
	}

	void InitializeMap ()
	{
		//initialize the two arrays.
		//put random blocks onto the map.
		//half of the map tiles will be solid block.
		for (int i = 0; i < 20; i++) {
			for (int j = 0; j < 20; j++) {
				for (int k = 0; k < 20; k++) {
					mapData [i, j, k] = (Random.Range (0, 2) == 0);    // 2/3 of the cubes will be solid at first (changed later)
					tempMark [i, j, k] = false;

					if (i == 19 || j == 19 || k == 19) {               //lock the outer surface of the cube.
						mapData [i, j, k] = false;
					}
				}
			}
		}
		mapData [1, 1, 1] = true;                                       // starting point to check cavities.

		//check cavities
//		for (int i = 0; i < 20; i ++)
//		{
//			for ( int j = 0; j < 20 ; j ++)
//			{
//				for (int k = 0; k < 20 ; k ++)
//				{
//					if (mapData[i,j,k] && !tempMark[i,j,k])
//					{
//						CheckCavity (i, j, k);
//					}
//				}
//			}
//		}

		//link the cavities
		for (int i = 0; i < 20; i++) {
			for (int j = 0; j < 20; j++) {
				for (int k = 0; k < 20; k++) {
					if (mapData [i, j, k] && !tempMark [i, j, k]) {
						LinkCavity (i, j, k);
						ResetMarks ();
						CheckCavity (1, 1, 1);
					}
				}
			}
		}
	}

	void ResetMarks ()
	{
		for (int i = 0; i < 20; i++) {
			for (int j = 0; j < 20; j++) {
				for (int k = 0; k < 20; k++) {
					tempMark [i, j, k] = false;
				}
			}
		}
	}

	void CheckCavity (int i, int j, int k)
	{
		if (tempMark [i, j, k]) {
			return;
		}
		tempMark [i, j, k] = true;

		//one way check because we always start checking at 1,1,1
		if (mapData [i, j, k + 1]) {
			CheckCavity (i, j, k + 1);
		}
		if (mapData [i, j + 1, k]) {
			CheckCavity (i, j + 1, k);
		}
		if (mapData [i + 1, j, k]) {
			CheckCavity (i + 1, j, k + 1);
		}
	}

	void LinkCavity (int i, int j, int k)
	{
		//BFS to find nearest cavity
		mapData [i, j, k] = false;
		Vector3 dest = FindNearest (i, j, k);
		mapData [i, j, k] = true;
		int a = (int)Mathf.Min (dest.x, i), b = (int)Mathf.Min (dest.y, j), c = (int)Mathf.Min (dest.z, k);
		while (a != (int)Mathf.Max (dest.x, i) && b != (int)Mathf.Max (dest.y, j) && c != (int)Mathf.Max (dest.z, k)) {
			int dir = Random.Range (0, 2);
			if (a == (int)Mathf.Max (i, dest.x) - 1 && dir == 0) {
				mapData [++ a, b, c] = true;
			}else
			if (b == (int)Mathf.Max (j, dest.y) - 1 && dir == 1) {
				mapData [a, ++ b, c] = true;
			}else
			if (c == (int)Mathf.Max (k, dest.z) - 1 && dir == 2) {
				mapData [a, b, ++ c] = true;
			}
		}	
	}

	Vector3 FindNearest (int i, int j, int k)
	{
		
		if (mapData [i, j, k]) {
			return new Vector3 (i, j, k);
		}

		if (tempMark [i, j, k]) {
			return new Vector3 (-1, -1, -1);
		}

		//use temp mark to mark down checked block.
		tempMark [i, j, k] = true;

		if (!mapData [i + 1, j, k]) {
			tempV3 = FindNearest (i + 1, j, k);
			if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
				return tempV3;
		} else {
			return new Vector3 (i + 1, j, k);
		}
		if (!mapData [i, j + 1, k]) {
			tempV3 = FindNearest (i, j + 1, k);
			if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
				return tempV3;
		} else {
			return new Vector3 (i, j + 1, k);
		}
		if (!mapData [i, j, k + 1]) {
			tempV3 = FindNearest (i, j, k + 1);
			if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
				return tempV3;
		} else {
			return new Vector3 (i, j, k + 1);
		}

		if (!mapData [i - 1, j, k]) {
			tempV3 = FindNearest (i + 1, j, k);
			if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
				return tempV3;
		} else {
			return new Vector3 (i - 1, j, k);
		}
		if (!mapData [i, j - 1, k]) {
			tempV3 = FindNearest (i, j - 1, k);
			if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
				return tempV3;
		} else {
			return new Vector3 (i, j - 1, k);
		}
		if (!mapData [i, j, k - 1]) {
			tempV3 = FindNearest (i, j, k - 1);
			if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
				return tempV3;
		} else {
			return new Vector3 (i, j, k - 1);
		}

		return new Vector3 (-1, -1, -1);
	}
		
}
