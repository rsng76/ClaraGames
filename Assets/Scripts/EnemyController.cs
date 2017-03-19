using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	private Rigidbody2D rigi;
	private bool faceingleft = false;

	public int speed = 3;
	public int destroydelay = 3;

	// Use this for initialization
	void Start () {
		rigi = GetComponent<Rigidbody2D>();
	}

	public void KillEnemy(){
		transform.GetChild (0).gameObject.SetActive (false);
		transform.GetChild (1).gameObject.SetActive (false);
		transform.GetChild (2).gameObject.SetActive (false);
		speed = 0;
		rigi.velocity = new Vector3 (0, -10, 0);
		transform.localScale = new Vector3 (1, 0.3f, 1);
		Destroy (gameObject, destroydelay);
	}
	
	// Update is called once per frame
	void Update () {
		
		rigi.velocity = new Vector2(speed,rigi.velocity.y);
		if (speed > 0 && !faceingleft) {
			Flip ();
		} else if (speed < 0 && faceingleft) {
			Flip ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Spinne")) {
			speed = speed * -1;
		}
	}

	void Flip()
	{
		faceingleft = !faceingleft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
