using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ChangePrefabSprite : MonoBehaviour
{
	public Sprite sprite00;
	public Sprite sprite01;
	public Sprite sprite02;

	// Start is called before the first frame update
	void Start()
	{
		int a = GameObject.Find("Teste01").GetComponent<Teste_Prep>().clicks;
		if(a == 0){
			transform.gameObject.GetComponent<SpriteRenderer>().sprite = sprite00;
		}else if(a == 1){
			transform.gameObject.GetComponent<SpriteRenderer>().sprite = sprite01;
		}else if(a == 2){
			transform.gameObject.GetComponent<SpriteRenderer>().sprite = sprite02;
		}
	}
}
