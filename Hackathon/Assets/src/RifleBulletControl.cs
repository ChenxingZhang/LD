using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBulletControl : MonoBehaviour {
	public Camera mainCam;

	Vector3 dst;
	float speed;
	float dmg;
	int life;
    bool fire;
	public void Fire(Vector3 dst)
	{
        gameObject.transform.parent = null;
		mainCam = FindObjectOfType<Camera> ();
		speed = 0.05f;
		dmg = 1.2f;
		float x = dst.x + Random.Range (-2f, 2f);
		float y = dst.y + Random.Range (-2f, 2f);
		float z = dst.z + Random.Range (-2f, 2f);
		this.dst = new Vector3(x, y ,z);

        float y_angle = Vector2.Angle(new Vector2(dst[1],dst[2]),new Vector2(1.0f,1.0f));

		//transform.Rotate (0.0f,0f, y_angle);
		life = 299;
        fire = true;
	}
    private void Awake()
    {
        fire = false;
    }
    void Update () {
		if (life <= 0)
		{
			Destroy (this.gameObject);
		}
        if (fire) {
            float x = Mathf.Lerp(gameObject.transform.position.x, dst.x, speed);
            float y = Mathf.Lerp(gameObject.transform.position.y, dst.y, speed);
            float z = Mathf.Lerp(gameObject.transform.position.z, dst.z, speed);
            dst = dst * 1.1f;
            gameObject.transform.position = new Vector3(x, y, z);
        }


		life --;
	}

	/// <summary>
	/// Explode this instance on collide with enemy;
	/// </summary>
	void Explode(Collider col)
	{
		Enemy robot = col.GetComponent<Enemy>();
		robot.TakeDamage(dmg);
		Destroy(this.gameObject);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Enemy>() != null)
            Debug.Log("we hit something!");
        Explode(collision.collider);
    }
}
