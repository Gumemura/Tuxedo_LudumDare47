using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
	//BACKGROUND CHANGER
		public SpriteRenderer[] backgrounds;
		public float backgroundSpeed;

		[HideInInspector]
		public int actualBack = -1;
		private int nextBack = 0;

		private Color tempColor;

		void ResetBackgrounds(){
			initialTimer = timeToChangeBack;
			for(int i = 0; i < backgrounds.Length; i++){
				tempColor = backgrounds[i].color;
				if(i == 0){
					tempColor.a = 1;
				}else{
					tempColor.a = 0;
				}
				backgrounds[i].color = tempColor;
			}
		}

		void ChangeBackIndex(){
			actualBack++;
			nextBack++;

			if(actualBack == backgrounds.Length){
				actualBack = 0;
				nextBack = 1;				
			}else if(actualBack == backgrounds.Length - 1){
				nextBack = 0;
			}
		}

		void ChangeBackground(){
			tempColor = backgrounds[actualBack].color;
			if(tempColor.a > 0){
				tempColor.a -= backgroundSpeed;
			}
			backgrounds[actualBack].color = tempColor;

			tempColor = backgrounds[nextBack].color;
			if(tempColor.a < 1){
				tempColor.a += backgroundSpeed;
			}else{
				changeBack = false;
			}
			backgrounds[nextBack].color = tempColor;
		}

		public float timeToChangeBack = 10;

		private float timer;
		private float initialTimer;
		private bool changeBack = false;
		void BackgroundTimer(){
		    timer += Time.deltaTime;
			int seconds = (int)timer % 60;

			if(initialTimer < seconds){
				initialTimer += timeToChangeBack;
				changeBack = true;
				ChangeBackIndex();
			}

			if(changeBack){
				ChangeBackground();
			}
		}

	void Start(){
		ResetBackgrounds();
	}

	void Update(){
		BackgroundTimer();
	}
}
