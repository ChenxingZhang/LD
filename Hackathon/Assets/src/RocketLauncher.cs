using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, Weapon {


	bool canFire = true;
	int ammo = 10;
	float cd = 1.35f;
	// Use this for initialization

	public void Fire (Vector3 dir)
	{
		if (canFire) 
		{
			EnterCD ();
			ammo--;
			//GameObject bullet = GameObject.Instantiate(bullet, gameObject.transformations, transformations.rotation) as Rigidbody;
			//play firing anim
			//create projectiles
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
