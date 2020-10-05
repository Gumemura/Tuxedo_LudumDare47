using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
	public Vector2 hFrequence;
	public Vector2 hAmplitude;
	[Range(0, 10)]
	public int floatingChance;
	private float frequence;
	private float amplitude;

	private SpriteRenderer sprt;
	private bool horizontalMovement = false;
	// Start is called before the first frame update
	void Start()
	{
		floatingChance = GameObject.Find("GameControler").GetComponent<GameController>().dificultRating;

		sprt = transform.gameObject.GetComponent<SpriteRenderer>();
		if(transform.position.x > 0){
			sprt.flipX = true;
		}

		if(Random.Range(0, 11) <= floatingChance){
			horizontalMovement = true;
			frequence = Random.Range(hFrequence.x, hFrequence.y);
			amplitude = Random.Range(hAmplitude.x, hAmplitude.y);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(horizontalMovement){
			transform.position += new Vector3(amplitude * Mathf.Sin(frequence * Time.time), 0, 0) * Time.deltaTime;
		}
	}
}
