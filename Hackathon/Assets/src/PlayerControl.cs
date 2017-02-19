using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public Player player;
	public Transform cameraTrans;

	Ray cameraRay;
	RaycastHit rayHit;
	// Use this for initialization
	void Awake()
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 movingDirection = new Vector3 (Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		gameObject.transform.position = gameObject.transform.position + movingDirection;

		if (Input.GetMouseButton(0))
		{
			Vector3 fireDirection;
			cameraRay.origin = cameraTrans.position;
			cameraRay.direction = Input.mousePosition;
			if (Physics.Raycast (cameraRay, out rayHit, 1000f, LayerMask.GetMask ("default"))) {
				fireDirection = rayHit.point - player.gameObject.transform.position;
				player.weapon.Fire (fireDirection);
			} else {
				Debug.Log ("Can't Find Firing target!");
			}
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
		{
			cameraTrans.position.z --;
		}
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
		{
			cameraTrans.position.z ++;
		}

	}
}
