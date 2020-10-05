using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		float rotationSpike;
		if(transform.position.x > 0){
			rotationSpike = 90;
		}else{
			rotationSpike = -90;
		}
		transform.Rotate(new Vector3(0, 0, rotationSpike));
	}
}
