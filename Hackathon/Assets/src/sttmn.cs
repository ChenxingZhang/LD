using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sttmn : MonoBehaviour {
	public Image img;
	public Text text;
	float r, g, b;
	int rd, gd, bd;
	// Use this for initialization
	void Awake()
	{
		//play sound
		rd = 1;
		gd = 1;
		bd = 1;
	}

	void Update()
	{
		changeColor ();

	}

	void changeColor()
	{
		if (img.color.r * 255f > 250f) {
			rd = -1;
		} else if (img.color.r * 255f < 5f) {
			rd = 1;
		} else {
			//rd = Random.Range (-1, 1);
		}

		if(img.color.g * 255f > 250f)
		{
			gd = -1;
		}else if(img.color.g * 255f < 5f)
		{
			gd = 1;
		} else {
			//gd = Random.Range (-1, 1);
		}

		if(img.color.b * 255f > 250f)
		{
			bd = -1;
		}else if(img.color.b * 255f < 5f)
		{
			bd = 1;
		} else {
			//bd = Random.Range (-1, 1);
		}




		r = img.color.r * 255f + Random.Range (0, 5) * rd;
		g = img.color.g * 255f + Random.Range (0, 5) * gd;
		b = Random.Range (0, 5) + img.color.b * 255f * bd;

		img.color = new Color (r/255,g/255,b/255);
		text.color = new Color (r/255,g/255,b/255);
	}
}
