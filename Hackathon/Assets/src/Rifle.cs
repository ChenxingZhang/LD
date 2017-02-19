using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour, Weapon {
	public Transform rifleBullet;

	Transform playerPos;
	bool canFire = true;
	int ammo = 155;
	float cd = 0.125f;
	// Use this for initialization

	public void Fire (Vector3 dir)
	{
		if (canFire) 
		{
			EnterCD ();
			ammo--;

			//play firing anim
			playerPos = FindObjectOfType<Player>().transform;
			Transform bullet = Object.Instantiate(rifleBullet, playerPos);
			RifleBulletControl bc = bullet.gameObject.GetComponent<RifleBulletControl> ();

			bc.Fire (dir);
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
