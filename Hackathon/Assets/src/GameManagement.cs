using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour {
	public Map map;
    public Transform center;
    public GameObject blocks;
    public Player player;
    public Transform enemy;
    public Transform enemyParent;
    public Vector3[] spawnPoints;

    bool isRotating;
    int currAxis;
    int blockPerFrame;
    int loadNumber;
    GameObject currBlockParent;
    int frameTimer;
    
	// Use this for initialization
	void Awake () {
		map = new Map ();
        isRotating = false;
        blockPerFrame = 10;
        loadNumber = 0;
        InitializeMap();
        currAxis = 0;
        frameTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (frameTimer == 6000)
        {
            frameTimer = 0;
        }
        if (frameTimer % 300 == 0)
        {
            Transform robot = Object.Instantiate(enemy, enemyParent, true);
            robot.transform.position = spawnPoints[0] + new Vector3(0.2f, 0.1f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(! isRotating && currAxis != 0)
                LoadSide(0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isRotating && currAxis != 1)
                LoadSide(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isRotating && currAxis != 2)
                LoadSide(2);
        }
    }

    void LoadSide(int side)
    {
        foreach (BoxCollider bc in  currBlockParent.GetComponentsInChildren<BoxCollider>())
        {
            Destroy(bc.gameObject);
        }
        Destroy(currBlockParent.gameObject);
        //Debug.Log("Loading side " + side + ".");
        isRotating = true;
        int x = (int) (player.gameObject.transform.position.x/1f) + 10, y = (int)(player.gameObject.transform.position.y / 1f) + 10;
        //Debug.Log("x " + x + ", y " + y);
        int layer = -1;
        if (((currAxis == 0 || currAxis == 2) && side == 1) || (currAxis == 1 && side == 2))
        {
            layer = (int)(player.transform.position.y / 1f) + 10;
        }
        else
        {
            layer = (int)(player.transform.position.x / 1f) + 10;
        }
        //Debug.Log("Layer " + layer);
        GameObject empty = new GameObject("blockParent");
        if (side == 0)                  //x
        {
            for (int i = 0; i < 20; i ++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (!map.mapData[layer, i, j])
                    {
                        GameObject blo = Instantiate(blocks, new Vector3(i - 10 , j - 10 , -1f), Quaternion.identity);
                        blo.transform.SetParent(empty.transform);
                        currBlockParent = empty;
                    }
                }
            }
        }
        if (side == 1)                  //y
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (!map.mapData[i, layer, j])
                    {
                        GameObject blo = Instantiate(blocks, new Vector3(i - 10, j - 10, -1f), Quaternion.identity);
                        blo.transform.SetParent(empty.transform);
                        currBlockParent = empty;
                    }
                }
            }
        }
        if (side == 2)                  //z
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (!map.mapData[i, j, layer])
                    {
                        GameObject blo = Instantiate(blocks, new Vector3(i - 10, j - 10, -1f), Quaternion.identity);
                        blo.transform.SetParent(empty.transform);
                        currBlockParent = empty;
                    }
                }
            }
        }
        isRotating = false;
    }

    void InitializeMap()
    {
        GameObject empty = new GameObject("blockParent");
        currBlockParent = empty;
        for (int i = 0; i < 20; i ++)
        {
            for (int j = 0; j < 20; j ++)
            {
                if (!map.mapData[i, j, 1])
                {
                    GameObject blo = Instantiate(blocks, new Vector3(i - 10, j - 10, -1f), Quaternion.identity);
                    blo.transform.SetParent(empty.transform);
                }
            }
        }
    }
}
