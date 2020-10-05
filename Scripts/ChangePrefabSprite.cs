using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePrefabSprite : MonoBehaviour
{
	public Sprite[] sprites;

	//Start is called before the first frame update
	void Start()
	{
		int currentBackground = GameObject.Find("GameControler").GetComponent<GameController>().actualColor;

		transform.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currentBackground];
	}
}
