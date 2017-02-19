using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBulletControl : MonoBehaviour
{
	public ParticleSystem dustParticle;
	//public ParticleSystem blood;

	Vector3 dst;
	float speed;
	float dmg;
	int life;
	bool fire;

	public void Fire (Vector3 dir)
	{
		gameObject.transform.parent = null;
		speed = 0.2f;
		dmg = 1.2f;
		float x = dir.x + Random.Range (-0.5f, 0.5f);
		float y = dir.y + Random.Range (-0.5f, 0.5f);
		float z = dir.z + Random.Range (-0.5f, 0.5f);
		this.dst = new Vector3 (x, y, z);
		dir.Normalize ();

		float y_angle = Vector2.Angle (new Vector2 (dir [1], dir [2]), new Vector2 (1.0f, 1.0f));

		transform.Rotate (0.0f, 0f, y_angle);
		life = 100;
		fire = true;
	}

	private void Awake ()
	{
		fire = false;
	}

	void Update ()
	{
		if (life <= 0) {
			Destroy (this.gameObject);
		}
		if (fire) {
			float x = gameObject.transform.position.x + dst.x * speed;
			float y = gameObject.transform.position.y + dst.y * speed;
			float z = gameObject.transform.position.z + dst.z * speed;

			gameObject.transform.position = new Vector3 (x, y, z);
		}


		life--;
	}

	/// <summary>
	/// Explode this instance on collide with enemy;
	/// </summary>
	void Explode (Collider col)
	{

		if (col.CompareTag ("Environment")) {
			ParticleSystem dust =  Instantiate (dustParticle);
			dust.transform.position = gameObject.transform.position;
			Destroy (dust.gameObject, 1.04f);
		} 
		else if (col.CompareTag ("Enemy")) {
			Debug.Log ("Bullet on Enemy!");
			Enemy robot = col.GetComponent<Enemy> ();
			robot.TakeDamage (dmg);
		} 
//			else if (col.CompareTag ("")) {
//
//		}

		Destroy (this.gameObject);
	}

	private void OnTriggerEnter (Collider col)
	{
		if (col.GetComponent<Enemy> () != null)
			Debug.Log ("we hit something!");
		if (!col.CompareTag ("Player"))
			Explode (col);
	}
}
