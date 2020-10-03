using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste_Prep : MonoBehaviour
{
	// public Sprite newSprite;
	// public SpriteRenderer prefab;

    // Start is called before the first frame update
    void Start()
    {
        //prefab.sprite = newSprite;
    }

    [HideInInspector]
	public int clicks = 0;
    public GameObject objeto;
    void Update(){
    	if(Input.GetMouseButtonDown(0)){
    		Instantiate(objeto, new Vector3(clicks, 0, 0), Quaternion.identity, this.transform);
    		clicks++;
    	}
    }


}
