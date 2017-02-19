using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public Weapon weapon = null;

	int hp;


	// Use this for initialization
	public void TakeDamage(int dmg, GameObject dmgSrc)
	{
		OnTakeDamage (dmg, dmgSrc);
	}

	void OnTakeDamage(int dmg, GameObject dmgSrc)
	{
		
	}
}
