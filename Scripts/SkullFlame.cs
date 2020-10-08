using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFlame : MonoBehaviour
{
	public float alhpaLost = 1;
	private Color colorA;
	// Start is called before the first frame update
	void Start()
	{
		transform.localScale = transform.parent.localScale;
		transform.GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<SpriteRenderer>().sprite;
		transform.GetComponent<SpriteRenderer>().flipX = transform.parent.GetComponent<SpriteRenderer>().flipX;
		colorA = transform.GetComponent<SpriteRenderer>().color;
		transform.position = transform.parent.position;
		transform.localScale = transform.parent.localScale;
	}

	// Update is called once per frame
	void Update()
	{
		transform.localPosition -= new Vector3(0, 5, 0) * Time.deltaTime; 
		colorA = new Color(colorA.r, colorA.g, colorA.b, colorA.a - alhpaLost);
		transform.GetComponent<SpriteRenderer>().color = colorA;

		if(colorA.a < 0){
			Destroy(this.gameObject);
		}
	}
}
