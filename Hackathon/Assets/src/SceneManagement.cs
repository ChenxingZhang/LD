using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour {

	// Use this for initialization
	public Animator anim;
	void Awake()
	{
		DontDestroyOnLoad (this);
	}

	public void LoadGame()
	{
		//SceneManager.LoadScene ();
		StartCoroutine (LoadTheGame());
	}

	IEnumerator LoadTheGame()
	{
		Debug.Log ("Load The Game!!");
		anim.SetTrigger ("Fade");

		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene ("Game");
	}
}
