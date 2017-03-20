using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player")){
			PlayerController pc = other.gameObject.GetComponent<PlayerController> ();
			pc.SaveProgress ();
		}
		Scene s = SceneManager.GetActiveScene();
		if ("Level_3_Tee_trinken".Equals (s.name)) {
			SceneManager.LoadScene ("Scene/Tee_trinken", LoadSceneMode.Single);
		}else{
			SceneManager.LoadScene ("Scene/"+name, LoadSceneMode.Single);
		}
	}
}
