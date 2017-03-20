using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartenController : MonoBehaviour {

	private Rigidbody2D rigi;
	private bool faceingleft = false;

	public int speed = 3;
	public int destroydelay = 2;

	// Use this for initialization
	void Start () {
		rigi = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		rigi.velocity = new Vector2(speed, rigi.velocity.y);
		if (speed > 0 && !faceingleft) {
			Flip ();
		} else if (speed < 0 && faceingleft) {
			Flip ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("KartenBorder")||other.gameObject.CompareTag ("Karte")||other.gameObject.CompareTag ("Picup")){
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

	public void KillKarte(){
		speed = 0;
		BoxCollider2D bcl = GetComponent<BoxCollider2D> ();
		bcl.enabled = false;
		CircleCollider2D ccl = GetComponent<CircleCollider2D> ();
		ccl.enabled = false;
		CapsuleCollider2D cacl = GetComponent<CapsuleCollider2D> ();
		cacl.enabled = false;
		Vector3 theScale = transform.localScale;
		theScale.y = 0.2f;
		transform.localScale = theScale;
		Destroy (gameObject, destroydelay);
	}
}
