using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPlayerMovement : MonoBehaviour
{
	public float vel;
	public Vector2 force;
	public Sprite fallingSprite;
	public Sprite aah;

	private bool hitRock = false;
	private Rigidbody2D rb2d;
	private Animator anim;
	private SpriteRenderer sprndr;

	void Start(){
		rb2d = transform.gameObject.GetComponent<Rigidbody2D>();
		anim = transform.gameObject.GetComponent<Animator>();
		sprndr = transform.gameObject.GetComponent<SpriteRenderer>();
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		if(!hitRock){
			MoveTotheLeft();
		}

		if(transform.position.x >= 0){
			rb2d.velocity = new Vector2(0, rb2d.velocity.y);
			rb2d.gravityScale = 1;
			sprndr.sprite = aah;
		}
	}

	void PlayerFall(){
		rb2d.AddForce(force);
	}

	void MoveTotheLeft(){
		transform.position += new Vector3(vel, 0, 0) * Time.deltaTime;
	}

	void OnTriggerEnter2D(){
		anim.enabled = false;
		sprndr.sprite = fallingSprite;
		rb2d.AddForce(force);
		rb2d.gravityScale = 1;
		hitRock = true;
	}
}
