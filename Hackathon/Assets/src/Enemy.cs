using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public Image img;       // the img component of the enemy sprite, used for color changing.
	public Animator anim;	// the animator of the sprite

	bool isAttacking, isAlive;
	float attackTime;
	float dmg;
	float hp;
	float timeTracker;
	int layerStatus;		//we can use transform.pos to track how far are they to the current layer at the time player changes the axis.
	void Awake()
	{
		isAttacking = false;
		isAlive = true;
		attackTime = 0.8f;
		dmg = 2.0f;
		timeTracker = 0f;
		//TODO: want to check if the enemy is on the same layer with player.
	}

	void Update () {

        if (layerStatus != 0)                   //on diferent layers
        {
            if (timeTracker < 1.0f)
            {
                timeTracker += 0.01f;
            }
            else
            {
                timeTracker = 0f;
                layerStatus += 0 - layerStatus;
                if (layerStatus == 0)
                {
                    //anim.SetTrigger("ShowUp");
                    //StartCoroutine(StartWalking);
                    //TODO: Map.GetRandomRespawnPoint();
                }
            }
        }
        else {                                      //on the same layer
             
        }
    }

	public void TakeDamage(float dmg)
	{
		hp -= dmg;
		if (hp <= 0)
		{
			//anim.SetTrigger("Die");
			isAlive = false;
            Respawn();
		}
	}

	public void AttackPlayer(Collider col)
	{
		//anim.SetTrigger("Attack");
		isAttacking = true;
		Player player = col.GetComponent<Player> ();
		player.TakeDamage (dmg, gameObject);
	}

	IEnumerator Attacking()
	{
		yield return new WaitForSeconds (attackTime);
		isAttacking = false;
	}

	public void Respawn()
	{
		//TODO: Map.GetRandomRespawnPoint();
		//TODO: Map.Generate Enemies(int amount);
		//For now I just repsawn them at 1, 1, 1.
		isAlive = true;
        float x = Random.Range(-5.0f, 5.0f), y = Random.Range(-5.0f, 5.0f);
        Vector3 var = new Vector3(x, y, -1f);
        gameObject.transform.position = var;
	}
}
