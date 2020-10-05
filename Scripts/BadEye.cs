using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEye : MonoBehaviour
{
	public float firstStopX = -6.8f;
	public float stopTime = 2;
	public float velocity = 1;
	public float acceleration = 1;

	private int seconds;
	private bool isLeft = false;
	private int whenStoped;
	private float yVelocity;
	private float xVelocity;

	// Start is called before the first frame update
	void Start()
	{
		isLeft = (transform.position.x < 0);

		if(!isLeft){
			transform.gameObject.GetComponent<SpriteRenderer>().flipX = true;
			transform.gameObject.GetComponent<Collider2D>().offset *= new Vector2(-1, 1);
			firstStopX *= -1;
		}

		yVelocity = Random.Range(.0f, .2f);
		xVelocity = 1.0f - yVelocity;
	}

	// Update is called once per frame
	void Update()
	{
		seconds = (int)Time.time;

		if((transform.position.x < firstStopX && isLeft) || (transform.position.x > firstStopX && !isLeft)){
			MoveBadEye(isLeft);
			whenStoped = seconds;
		}else{
			if(whenStoped + stopTime < seconds){
				MoveBadEye(isLeft, acceleration);
			}
		}

		DestroyBadEye();
	}

	void MoveBadEye(bool direction, float acele = 1){
		int inversor = -1;
		if(direction){
			inversor = 1;
		}  
		transform.position += new Vector3(xVelocity, yVelocity, 0) * inversor * velocity * acele * Time.deltaTime;
	}

	void DestroyBadEye(){
		if(transform.position.x > 15 || transform.position.x < -15){
			Destroy(this.gameObject);
		}
	}
}
