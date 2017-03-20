using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevelScript : MonoBehaviour {

	private Animator anim;
	// Use this for initialization
	void Start () {
		int picups = PlayerPrefs.GetInt (name);
		anim = GetComponentInParent<Animator>();
		TextMesh progress = GetComponentInChildren<TextMesh> ();
		if (progress != null) {
			progress.text = "" + picups;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		anim.SetTrigger ("Wackeln");
	}
}
