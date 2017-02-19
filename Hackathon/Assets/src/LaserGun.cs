using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour, Weapon {


	bool canFire = true;
	int ammo = 15;
	float cd = 1.1f;
	// Use this for initialization


	public void Fire (Vector3 dir)
	{
		if (canFire) 
		{
			EnterCD ();
			ammo--;

			//GameObject bullet = (GameObject)Instantiate (Resources.Load("Bullet"),gameObject.transform);
			//bullet.transform.LookAt;

			//play firing anim
		}
		
	}

	public void AddAmmo (int amt)
	{
		ammo += amt;
	}

	void EnterCD()
	{
		canFire = false;
		StartCoroutine (coldDown());
	}

	IEnumerator coldDown()
	{
		yield return new WaitForSeconds (cd);
		canFire = true;
	}
}
