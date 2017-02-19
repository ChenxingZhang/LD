using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public Weapon weapon = null;
    public Sprite[] sprites;

	float hp;

	void Awake()
	{
		hp = 10f;
        PickUpWeapon(0);
	}

    // Use this for initialization
    public void PickUpWeapon(int weaponNumber)
    {
        SpriteRenderer sr =  gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[weaponNumber];
        weapon = gameObject.GetComponent<Rifle>();
    }
	public void TakeDamage(float dmg, GameObject dmgSrc)
	{
		OnTakeDamage (dmg, dmgSrc);
	}

	void OnTakeDamage(float dmg, GameObject dmgSrc)
	{
		hp -= dmg;
	}
}
