using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBulletControl : MonoBehaviour {
	public Camera mainCam;

	Vector3 dst;
	float speed;
	float dmg;
	int life;
	public void Fire(Vector3 dst)
	{
		mainCam = FindObjectOfType<Camera> ();
		speed = 0.3f;
		dmg = 1.2f;
		float x = dst.x + Random.Range (-5f, 5f);
		float y = dst.y + Random.Range (-5f, 5f);
		float z = dst.z + Random.Range (-5f, 5f);
		this.dst = new Vector3(x, y ,z);

		transform.Rotate ();
		life = 299;
	}

	void Update () {
		if (life <= 0)
		{
			Destroy (this.gameObject);
		}

		float x = Mathf.Lerp (gameObject.transform.position.x, dst.x, speed);
		float y = Mathf.Lerp (gameObject.transform.position.y, dst.y, speed);
		float z = Mathf.Lerp (gameObject.transform.position.z, dst.z, speed);
		dst = dst * 1.1f;

		gameObject.transform.position = new Vector3 (x, y, z);
		life --;
	}

	/// <summary>
	/// Explode this instance on collide with enemy;
	/// </summary>
	void Explode()
	{
		//Enemy = collider.GetComponent<Enemy>();
		//Enemy.TakeDamage(dmg);
		//Destroy(this.gameObject);
	}
}
