using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialPlayerMovement : MonoBehaviour
{
	public float initialVelocity;
	public Vector2 force;
	public Sprite fallingSprite;
	public Vector2 yLimit;
	public float inGameVelocity;
	public Sprite theEndSprite;

	private bool hitRock = false;
	private Rigidbody2D rb2d;
	private Animator anim;
	private SpriteRenderer sprndr;
	private bool gameIntro = true;
	private bool imAlive = true;
	private Transform eyes;
	private GameObject gameController;

	void MoveTotheLeft(){
		transform.position += new Vector3(initialVelocity, 0, 0) * Time.deltaTime;
	}

	void Start(){
		rb2d = transform.gameObject.GetComponent<Rigidbody2D>();
		anim = transform.gameObject.GetComponent<Animator>();
		sprndr = transform.gameObject.GetComponent<SpriteRenderer>();
		eyes = transform.GetChild(0);
		gameController = GameObject.Find("GameControler");
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(!hitRock){
			MoveTotheLeft();
		}

		if(transform.position.x >= 0 && gameIntro){
			gameIntro = false;
			rb2d.velocity = Vector2.zero;
			rb2d.gravityScale = 0;
			anim.enabled = true;
			anim.SetBool("startFall", true);
			gameController.GetComponent<GameController>().enabled = true;
			transform.GetChild(0).gameObject.SetActive(true);
		}

		if(!gameIntro && imAlive){
			Movement();
		}

		// if(!imAlive){
		// 	DeathAnimations();
		// }
	}

	void Death(){
		gameController.GetComponent<GameController>().gameRunning = false;
		imAlive = false;
		anim.enabled = false;
		sprndr.sprite = theEndSprite;
		transform.GetChild(0).gameObject.SetActive(false);
		Invoke("DeathAnimations", 1);
	}

	void DeathAnimations(){
		rb2d.AddForce(new Vector3(0, 800, 0));
		rb2d.gravityScale = 4;
		Invoke("ShowRestart", 2);
	}

	void ShowRestart(){
		GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
	}

	void Movement(){
		if(Input.GetAxis("Horizontal") != 0){
			transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Time.deltaTime * inGameVelocity;
			if(Input.GetAxis("Horizontal") > 0){
				eyes.localPosition = new Vector3(.1f, eyes.localPosition.y, 0);
			}else{
				eyes.localPosition = new Vector3(-.1f, eyes.localPosition.y, 0);
			}
		}else{
			eyes.localPosition = new Vector3(0, eyes.localPosition.y, 0);
		}

		if(Input.GetAxis("Vertical") != 0){
			if((Input.GetAxis("Vertical") > 0 && transform.position.y < yLimit.x) || (Input.GetAxis("Vertical") < 0 && transform.position.y > yLimit.y)){
				transform.position += new Vector3(0, Input.GetAxis("Vertical"), 0) * Time.deltaTime * inGameVelocity;
			}
			if(Input.GetAxis("Vertical") > 0){
				eyes.localPosition = new Vector3(eyes.localPosition.x, .1f, 0);
			}else{
				eyes.localPosition = new Vector3(eyes.localPosition.x, -.1f, 0);
			}
		}else{
			eyes.localPosition = new Vector3(eyes.localPosition.x, 0, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.name == "Rock"){
			anim.enabled = false;
			sprndr.sprite = fallingSprite;
			rb2d.AddForce(force);
			rb2d.gravityScale = 1;
			hitRock = true;
		}else{
			Death();
		}
	}
}
