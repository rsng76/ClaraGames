using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarienkaeferController : MonoBehaviour {

	private Rigidbody2D rigi;
	private bool faceingleft = false;
	private Animator anim; 
	private SpriteRenderer spriteR;

	public int speed = 3;
	public int destroydelay = 2;

	// Use this for initialization
	void Start () {
		rigi = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriteR = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat("Speed", Mathf.Abs(rigi.velocity.x));
		rigi.velocity = new Vector2(speed, rigi.velocity.y);
		if (speed > 0 && !faceingleft) {
			Flip ();
		} else if (speed < 0 && faceingleft) {
			Flip ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("MarienkaeferBorder")||other.gameObject.CompareTag ("Marienkaefer")||other.gameObject.CompareTag ("Picup")) {
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

	public void KillMarienkaefer(){
		speed = 0;
		BoxCollider2D bcl = GetComponent<BoxCollider2D> ();
		bcl.enabled = false;
		CircleCollider2D ccl = GetComponent<CircleCollider2D> ();
		ccl.enabled = false;
		CapsuleCollider2D cacl = GetComponent<CapsuleCollider2D> ();
		cacl.enabled = false;
		spriteR.sprite = Resources.Load("Extras_12", typeof(Sprite)) as Sprite;;
		Destroy (gameObject, destroydelay);
	}

	public void AttackMarienkaefer (){
		anim.SetTrigger ("Angriff");
	}
}
