using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public Player player;
	public Transform cameraTrans;
    public Camera mainCam;

	Ray cameraRay;
	RaycastHit rayHit;
	// Use this for initialization
	void Awake()
	{
        mainCam = GameObject.FindObjectOfType<Camera>();
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 movingDirection = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		gameObject.transform.position = gameObject.transform.position + movingDirection * 0.1f;

		if (Input.GetMouseButton(0))
		{
			Vector3 fireDirection;
			//cameraRay.origin = cameraTrans.position;
			//Vector3 dir = Input.mousePosition - cameraTrans.position;
   //         dir.z = -cameraTrans.position.z;
   //         cameraRay.direction = dir;
            cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast (cameraRay, out rayHit, 10000f, LayerMask.GetMask ("Default"))) {
				fireDirection = rayHit.point - player.gameObject.transform.position;
                fireDirection.z = -0f;
               // Debug.Log("Firing target: " + player.gameObject.transform.position + ", " + rayHit.point + ", " + fireDirection);
                if (player.weapon != null)
				    player.weapon.Fire (fireDirection);
			} else {
				//Debug.Log ("Can't Find Firing target: " + cameraRay.direction + "" + cameraRay.origin);
			}
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
		{
            Vector3 vec = cameraTrans.position;
            vec.z--;
			cameraTrans.position = vec;
		}
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
		{
            Vector3 vec = cameraTrans.position;
            vec.z++;
            cameraTrans.position = vec;
        }

	}
}
