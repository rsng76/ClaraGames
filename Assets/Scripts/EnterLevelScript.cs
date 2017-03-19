using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevelScript : MonoBehaviour {

	private Animator anim;
	private 
	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		anim.SetTrigger ("Wackeln");
	}
}
