using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIButtons : MonoBehaviour
{
	public GameObject credits;
	public GameObject mainMenu;


	public void StartGame(){
		SceneManager.LoadScene(1);
	}

	public void LoadCredits(){
		mainMenu.SetActive(!mainMenu.activeSelf);
		credits.SetActive(!credits.activeSelf);
	}

	public void Bye(){
		Application.Quit();
	}
}
