using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10f;
    public float jumpSpeed = 100f;
	public Image heart;
	public Text picupCount;

	private bool faceingright = true;
    private Rigidbody2D rigi;
	private CircleCollider2D circlecollider;
    private Animator anim; 
    private float movex = 0f;
    private bool jump = false;
	private bool dubbleJump = false;
	private bool niessen = false;
	private int collected = 0;
	private int life = 2;
	private int invulnerable = 0;

    // Use this for initialization
    void Start(){
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		circlecollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
		invulnerable -= 1;
		//Check if we are running either in the Unity editor or in a standalone build.
		#if UNITY_STANDALONE || UNITY_WEBPLAYER

        jump = Input.GetKeyDown(KeyCode.Space);
		niessen = Input.GetKeyDown(KeyCode.N);

		movex = Input.GetAxis("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(movex));
		rigi.velocity = new Vector2(movex * maxSpeed, rigi.velocity.y);
		//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		int screenMidd = Screen.width / 2;
		movex = Input.GetAxis("Mouse X");
		if (Input.touchCount > 0)
		{
			if(Input.touches[0].position.x > screenMidd){
				movex = 1;
			}else if(Input.touches[0].position.x < screenMidd){
				movex = -1;
			}
		}
		if (Input.touchCount == 2 && (Input.touches[0].phase.Equals(TouchPhase.Began) || Input.touches[1].phase.Equals(TouchPhase.Began))){
			jump = true;
		}
		if (Input.touchCount == 3){
			niessen = true;
		}
		anim.SetFloat("Speed", Mathf.Abs(movex));
		rigi.velocity = new Vector2(movex * maxSpeed, rigi.velocity.y);

		#endif //End of mobile platform dependendent compilation section started above with #elif

		if(niessen)
		{
			anim.SetTrigger("Niessen");
			niessen = false;
		}
		if(circlecollider.IsTouchingLayers() && jump)
		{
			Jump ();
		}
		else if(!circlecollider.IsTouchingLayers() && jump && dubbleJump)
		{
			DubbleJump ();
		}
		if (movex > 0 && !faceingright)
		{
			Flip();
		}else if (movex < 0 && faceingright)
		{
			Flip();
		}
		if (life <= 0) {
			anim.SetTrigger ("Dead");
			if (invulnerable <= 0) {
				Reset ();
			}
		}else if (transform.position.y < -50) {
			life = 0;
			Damage ();
		}
    }

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.CompareTag ("EnterWorld") && other is CircleCollider2D) {
			string levelName = other.gameObject.name;
			SceneManager.LoadScene ("Scene/" + levelName, LoadSceneMode.Single);
		}
		if (other.CompareTag ("PicupTee")) {
			collected += 1;
			picupCount.text = "Teeblätter "+collected;
			other.gameObject.SetActive (false);
		}
		if (other.CompareTag ("PicupKarte")) {
			collected += 1;
			picupCount.text = "Karten "+collected;
			other.gameObject.SetActive (false);
		}
		//Marienkaefer
		if (other is BoxCollider2D && other.CompareTag("Marienkaefer")) {
			Jump ();
			MarienkaeferController script = other.GetComponent<MarienkaeferController> (); 
			script.KillMarienkaefer ();
		}
		if (invulnerable <= 0) {
			if (other is CircleCollider2D && other.CompareTag("Marienkaefer")) {
				MarienkaeferController script = other.GetComponent<MarienkaeferController> (); 
				script.AttackMarienkaefer ();
				Damage ();
			}
		}
		//Karte
		if (other is BoxCollider2D && other.CompareTag("Karte")) {
			Jump ();
			KartenController script = other.GetComponent<KartenController> (); 
			script.KillKarte ();
		}
		if (invulnerable <= 0) {
			if (other is CircleCollider2D && other.CompareTag("Karte")) {
				Damage ();
			}
		}

	}

	void Jump(){
		anim.SetTrigger("Jump");
		rigi.velocity = new Vector2 (-rigi.velocity.x, jumpSpeed);
		jump = false;
		dubbleJump = true;
	}

	void DubbleJump(){
		anim.SetTrigger("Drehen");
		rigi.velocity = new Vector2(rigi.velocity.x, jumpSpeed+1);
		dubbleJump = false;
		jump = false;
	}
		
	void Damage() {
		invulnerable = 50;
		life -= 1;
		if (life > 0) {
			Jump ();
		}
		if (life <= 1) {
			heart.gameObject.SetActive(false);
		}
	}

	void Flip()
	{
		faceingright = !faceingright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Reset()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		/*life = 2;
		heart.gameObject.SetActive(true);
		rigi.velocity = new Vector2 (0, 0);
		transform.position = new Vector3 (-20, 3, 0);
		collected = 0;
		if (picupCount != null) {
			picupCount.text = "Teeblätter 0";
		}*/
	}

	public void SaveProgress(){
		Scene s = SceneManager.GetActiveScene();
		int oldScore = PlayerPrefs.GetInt (s.name);
		if (collected > oldScore) {
			PlayerPrefs.SetInt (s.name, collected);
			PlayerPrefs.Save ();
		}
	}


}
