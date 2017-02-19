using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour, Weapon {
	public Transform rifleBullet;
	public PlayerControl player;

	Transform playerPos;
	bool canFire = true;
	int ammo = 155;
	float cd = 0.125f;
	// Use this for initialization

	public void Fire (Vector3 dir)
	{
        //Debug.Log("Firing!");
		if (canFire) 
		{
			EnterCD ();
			ammo--;

			//play firing anim
			playerPos = FindObjectOfType<Player>().transform;
			Transform bullet = Object.Instantiate(rifleBullet, playerPos, true);
            bullet.transform.position = playerPos.position + new Vector3(0.2f, 0.1f, 0f);
			RifleBulletControl bc = bullet.gameObject.GetComponent<RifleBulletControl> ();
			player.FireSound ();

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
