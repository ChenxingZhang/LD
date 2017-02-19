using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{

    public bool[,,] mapData = new bool[20, 20, 20];
    // true for walkable
    bool[,,] tempMark = new bool[20, 20, 20];
    bool[,,] secMark = new bool[20, 20, 20];
    bool once = true;
    int cavityCount = 0, mainCavCount = 0;
    Vector3 tempV3;
    // Use this for initialization
    public Map()
    {
        InitializeMap();
    }

    void InitializeMap()
    {
        //initialize the two arrays.
        //put random blocks onto the map.
        //half of the map tiles will be solid block.
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                for (int k = 0; k < 20; k++)
                {
                    mapData[i, j, k] = (Random.Range(0, 2) == 0);    // 2/3 of the cubes will be solid at first (changed later)
                    tempMark[i, j, k] = false;
                    secMark[i, j, k] = false;

                    if (i == 19 || j == 19 || k == 19 || i == 0 || j == 0 || k == 0)
                    {               //lock the outer surface of the cube.
                        mapData[i, j, k] = false;
                    }
                    if (mapData[i, j, k])
                    {
                        cavityCount++;
                    }
                }
            }
        }
        mapData[1, 1, 1] = true;                                       // starting point to check cavities.
        tempMark[1, 1, 1] = true;
        //while (mainCavCount + 1 != cavityCount)
        //{
        //    //link the cavities
        //    for (int i = 0; i < 20; i++)
        //    {
        //        for (int j = 0; j < 20; j++)
        //        {
        //            for (int k = 0; k < 20; k++)
        //            {
        //                if (mapData[i, j, k] && !tempMark[i, j, k])
        //                {     //if the cavity is not connected to the maion cavity
        //                    LinkCavity(i, j, k);
        //                    goto Outside;
        //                }
        //            }
        //        }
        //    }
        //}
        //Outside:

        //    ResetMarks();
        //    CheckCavity(1, 1, 1);                          //Mark again
        //    Debug.Log(mainCavCount + " : " + cavityCount);
        
    }

    void ResetMarks()
    {
        cavityCount--;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                for (int k = 0; k < 20; k++)
                {
                    tempMark[i, j, k] = false;
                }
            }
        }
        tempMark[1, 1, 1] = true;
        mainCavCount = 0;
    }

    void ResestSecMarks()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                for (int k = 0; k < 20; k++)
                {
                    secMark[i, j, k] = false;
                }
            }
        }
    }

    void CheckCavity(int i, int j, int k)
    {

        tempMark[i, j, k] = true;
        mainCavCount++;
        if (mainCavCount == cavityCount)
        {
            return;
        }


        //one way check because we always start checking at 1,1,1
        if (k + 1 > 19)
        {
            return;
        }
        if (mapData[i, j, k + 1] && !tempMark[i, j, k + 1])
        {
            CheckCavity(i, j, k + 1);
        }
        if (j + 1 > 19)
        {
            return;
        }
        if (mapData[i, j + 1, k] && !tempMark[i, j + 1, k])
        {
            CheckCavity(i, j + 1, k);
        }
        if (i + 1 > 19)
        {
            return;
        }
        if (mapData[i + 1, j, k] && !tempMark[i + 1, j, k])
        {
            CheckCavity(i + 1, j, k);
        }
        if (k - 1 < 0)
        {
            return;
        }
        if (mapData[i, j, k - 1] && !tempMark[i, j, k - 1])
        {
            CheckCavity(i, j, k - 1);
        }
        if (j - 1 < 0)
        {
            return;
        }
        if (mapData[i, j - 1, k] && !tempMark[i, j - 1, k])
        {
            CheckCavity(i, j - 1, k);
        }
        if (i - 1 < 0)
        {
            return;
        }
        if (mapData[i - 1, j, k] && !tempMark[i - 1, j, k])
        {
            CheckCavity(i - 1, j, k);
        }


    }

    void LinkCavity(int i, int j, int k)
    {
        //BFS to find nearest cavity
        mapData[i, j, k] = false;
        ResestSecMarks();
        Vector3 dest = new Vector3(1f, 1f, 1f);
        if (once)
        dest = FindNearest(i, j, k);
        Debug.Log("Src: " + i + ", " + j + ", " + k);
        Debug.Log(dest);

        int x = (int)dest.x, y = (int)dest.y, z = (int)dest.z;
        Debug.Log(tempMark[x, y, z] + ", " + tempMark[i, j, k]);
        int a = -1, b = -1, c = -1;
        if (x >= i)
        {
            a = 1;
        }
        if (y >= j)
        {
            b = 1;
        }
        if (z >= k)
        {
            c = 1;
        }
        mapData[i, j, k] = true;
        while (i != x || y != j || z != k)
        {
            int dir = Random.Range(0, 3);
            if (dir == 0)
            {
                if (i == x)
                {
                    continue;
                }
                if (mapData[i + a, j, k])
                {
                    i += a;
                    continue;
                }
                else
                {
                    i += a;
                    mapData[i, j, k] = true;
                    cavityCount++;
                    Debug.Log("Added a cavity!");
                }
            }
            else if (dir == 1)
            {
                if (y == j)
                {
                    continue;
                }
                if (mapData[i, j + b, k])
                {
                    j += b;
                    continue;
                }
                else
                {
                    j += b;
                    mapData[i, j, k] = true;
                    cavityCount++;
                    Debug.Log("Added a cavity!");
                }
            }
            else if (dir == 2)
            {
                if (z == k)
                {
                    continue;
                }
                if (mapData[i, j, k + c])
                {
                    k += c;
                    continue;
                }
                else
                {
                    k += c;
                    mapData[i, j, k] = true;
                    cavityCount++;
                    Debug.Log("Added a cavity!");
                }
            }
        }
    }

    Vector3 FindNearest(int i, int j, int k)
    {
        once = false;
        //Debug.Log(i + ", " + j + ", " + k);
        if (mapData[i, j, k] && tempMark[i, j, k])
        {
            return new Vector3(i, j, k);
        }
        if (mapData[i, j, k] && secMark[i, j, k])
        {
            return new Vector3(-1, -1, -1);
        }
        if (tempMark[i, j, k] && !mapData[i, j, k])
        {
            return new Vector3(-1, -1, -1);
        }

        //use temp mark to mark down checked block.
        tempMark[i, j, k] = true;
        if (i + 1 > 19)
        {
            return new Vector3(-1, -1, -1);
        }
        else
        if (!mapData[i + 1, j, k] || (!tempMark[i + 1, j, k] && !secMark[i + 1, j, k]))
        {
            secMark[i + 1, j, k] = true;
            tempV3 = FindNearest(i + 1, j, k);
            if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
                return tempV3;
        }
        else if (tempMark[i + 1, j, k])
        {
            return new Vector3(i + 1, j, k);
        }

        if (j + 1 > 19)
        {
            return new Vector3(-1, -1, -1);
        }
        else
        if (!mapData[i, j + 1, k] || (!tempMark[i, j + 1, k] && !secMark[i, j + 1, k]))
        {
            secMark[i , j + 1, k] = true;
            tempV3 = FindNearest(i, j + 1, k);
            if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
                return tempV3;
        }
        else if (tempMark[i, j + 1, k])
        {
            return new Vector3(i, j + 1, k);
        }

        if (k + 1 > 19)
        {
            return new Vector3(-1, -1, -1);
        }
        else
        if (!mapData[i, j, k + 1] || (!tempMark[i , j, k + 1] && !secMark[i , j, k + 1]))
        {
            secMark[i, j, k + 1] = true;
            tempV3 = FindNearest(i, j, k + 1);
            if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
                return tempV3;
        }
        else if (tempMark[i, j, k + 1])
        {
            return new Vector3(i, j, k + 1);
        }

        if (i - 1 < 0)
        {
            return new Vector3(-1, -1, -1);
        }
        else if (!mapData[i - 1, j, k] || (!tempMark[i - 1, j, k] && !secMark[i - 1, j, k]))
        {
            secMark[i - 1, j, k] = true;
            tempV3 = FindNearest(i - 1, j, k);
            if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
                return tempV3;
        }
        else if (tempMark[i - 1, j, k])
        {
            return new Vector3(i - 1, j, k);
        }

        if (j - 1 < 0)
        {
            return new Vector3(-1, -1, -1);
        }
        else if (!mapData[i, j - 1, k] || (!tempMark[i, j - 1, k] && !secMark[i , j - 1, k]))
        {
            secMark[i , j - 1, k] = true;
            tempV3 = FindNearest(i, j - 1, k);
            if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
                return tempV3;
        }
        else if (tempMark[i , j - 1, k])
        {
            return new Vector3(i, j - 1, k);
        }

        if (k - 1 < 0)
        {
            return new Vector3(-1, -1, -1);
        }
        else if (!mapData[i, j, k - 1] || (!tempMark[i , j, k - 1] && !secMark[i , j, k - 1]))
        {
            secMark[i , j, k - 1] = true;
            tempV3 = FindNearest(i, j, k - 1);
            if (tempV3.x != -1f && tempV3.y != -1f && tempV3.z != -1f)
                return tempV3;
        }
        else if (tempMark[i, j, k - 1])
        {
            return new Vector3(i, j, k - 1);
        }

        return new Vector3(-1, -1, -1);
    }

}
