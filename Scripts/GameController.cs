using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameController : MonoBehaviour
{
	[HideInInspector]
	public float globalVelocity;
	[HideInInspector]
	public bool gameRunning = true;
	[HideInInspector]
	public int actualColor = 0;
	[HideInInspector]
	public int dificultRating = 0;

	[Header("Basics")]
	//Any transform above this limit will be destroyed
	public float yLimitToDestroy;
	//Velocity
	public float limitGlobalVelocity;
	public float globalAceleration;
	public GameObject pauseMenu;

	[Header("Coin")]
	public GameObject coinObj;
	public float energyLost;
	public float chanceSpawnCoin = 2;
	[HideInInspector]
	public bool slowTime = false;

	//Variables related to the background color change
	//Yes, is terible, i know. It was my first time dealing with Color, so give me a discount hehe
	//To do: convert this terible floats to integer
	[Header("Background color changer")]
	public Camera cam;
	public float colorSpeed; // How fast will the color change
	private Vector3 cBrown = new Vector3(0.2352941f, 0.1607843f, 0.1058824f);
	private Vector3 cGreen = new Vector3(0.1176471f, 0.2352941f, 0.1058824f);
	private Vector3 cPurple = new Vector3(0.2f, 0.1294118f, 0.1647059f);
	private Vector3[] backColors = new Vector3[3];
	private bool cR = false, cG = false, cB = false, callBackgroundChanger = false;

	[Header("Wall and sparkles")]
	//Wall instantiation
	public GameObject wall;
	private float wallYInstatiate = -5;
	private string wallTag = "Wall";

	//Sparkles instantiation
	public GameObject sparkles;
	private float sparklesYInstatiate = -40;
	private string sparklesTag = "Sparkles";

	[Header("Monsters")]
	public GameObject badEye;
	public GameObject flamingSkull;
	public GameObject plataform;
	public GameObject spikes;
	[Range(0, 10)]
	public int chanceSpawnMonster;
	[Range(0, 10)]
	public int chanceSpawnSpike;
	[Range(0, 10)]
	public int chanceSpawnPlataform;
	public int spaceBetweenMonsters = 4;

	public Vector2 spawnLimitX; //X limits to spawn monster
	public float spawnEnemyY; //Y to spawn monster
	public float monsterAcelerationLimit;
	public float monsterInitialAcceleration = 1.5f;
	private float monsterAcceleration;
	private string monsterTag = "Monster";
	private string badEyeTag = "BadEye";

	[Header("Time control")]
	//Time interval to change the background color
	public float timeChangeBack;
	private float tempTimerChangeBack;
	public float difficultModifier;
	private float tempDifficultModifier;

	public void StopEverything(){
		globalVelocity = 0;
	}

	public void QuitGame(){
		Application.Quit();
	}

	void PauseGame(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			pauseMenu.SetActive(!pauseMenu.activeSelf);

			Time.timeScale = Time.timeScale + 1 + (- 2 * Time.timeScale);
		}
	}

	void AcelerateObjectsMovement(){
		if(globalVelocity < limitGlobalVelocity){
			globalVelocity += globalAceleration;
		}else{
			globalVelocity = limitGlobalVelocity;
		}
	}

	void BreakObjectsMovement(){
		if(globalVelocity > .5f){
			globalVelocity -= globalAceleration;
		}
	}

	void Destroyer(Transform transf){
		if(transf.position.y >= yLimitToDestroy){
			Destroy(transf.gameObject);
			if(transf.gameObject.tag == wallTag){
				InstantiateObj(wall, wallYInstatiate);
			}else if(transf.gameObject.tag == sparklesTag){
				InstantiateObj(sparkles, sparklesYInstatiate);
			}
		}
	}

	void InstantiateObj(GameObject obj, float y, float x = 0){
		Instantiate(obj, new Vector3(x, y, 0), Quaternion.identity, this.transform);
	}

	void MonsterSpawn(){
		if(Random.Range(0, 10) < chanceSpawnMonster){
			float enemyX = Random.Range(spawnLimitX.x, spawnLimitX.y);

			if(!slowTime){
				InstantiateObj(flamingSkull, spawnEnemyY, Random.Range(spawnLimitX.x, spawnLimitX.y));
				if(dificultRating > 10){
					InstantiateObj(flamingSkull, spawnEnemyY - spaceBetweenMonsters, Random.Range(spawnLimitX.x, spawnLimitX.y));
				}

				if(dificultRating > 20){
					InstantiateObj(flamingSkull, spawnEnemyY - spaceBetweenMonsters * 2, Random.Range(spawnLimitX.x, spawnLimitX.y));
				}
			}
		}
	}

	void StructureSpawn(){
		if(Random.Range(0, 10) < chanceSpawnSpike){
			int enemyX;
			if(Random.Range(0, 2) == 0){
				enemyX = -6;
			}else{
				enemyX = 6;
			}
			InstantiateObj(spikes, spawnEnemyY, enemyX);
		}

		if(Random.Range(0, 10) < chanceSpawnPlataform){
			InstantiateObj(plataform, spawnEnemyY, Random.Range(spawnLimitX.x, spawnLimitX.y));
		}
	}

	void BadEyeSpawn(){
		int badEyeX;
		if(Random.Range(0,2) == 0){
			badEyeX = -1;
		}else{
			badEyeX = 1;
		}

		InstantiateObj(badEye, Random.Range(-3.5f, 3.5f), 10 * badEyeX);
	}

	void MovesObject(Transform transf, float plusAceleration = 1){
		if(transf.gameObject.tag == monsterTag){
			plusAceleration = monsterAcceleration;
		}else if(transf.gameObject.tag == badEyeTag){
			plusAceleration = 0;
		}
		transf.position += new Vector3(0, globalVelocity * plusAceleration, 0) * Time.deltaTime; 
	}

	void DificultIncrease(){
		dificultRating++;

		if(chanceSpawnMonster <= 10){
			chanceSpawnMonster++;
		}
		if(chanceSpawnSpike <= 10){
			chanceSpawnSpike++;
		}
		if(chanceSpawnPlataform <= 10){
			chanceSpawnPlataform++;
		}

		if(monsterAcceleration < monsterAcelerationLimit){
			monsterAcceleration += .1f;
		}
	}

	void TimeManager(){
		int seconds = (int)(Time.timeSinceLevelLoad);
		if(seconds >= tempTimerChangeBack){
			IndexColorSetter();
			callBackgroundChanger = true;
			tempTimerChangeBack += timeChangeBack;
		}

		if(seconds >= tempDifficultModifier){
			DificultIncrease();
			tempDifficultModifier += difficultModifier;
		}
	}

	void BackgroundChanger(int i){
		if(callBackgroundChanger){
			int inversor = 1;
			/*
			Background color:
			Brown R60 G41 B27 Hex 3C291B
			Green R31 G60 B28 Hex 1E3C1B
			Purple R51 G33 B42 Hex 33212A 
			*/
			float camR = cam.backgroundColor.r;
			float camG = cam.backgroundColor.g;
			float camB = cam.backgroundColor.b;


			if(!Mathf.Approximately(camR, backColors[i].x)){
				if(camR < backColors[i].x){
					inversor = 1;
				}else{
					inversor = -1;
				}
				camR += colorSpeed * inversor;
			}else{
				cR = true;
			}

			if(!Mathf.Approximately(camG, backColors[i].y)){
				if(camG < backColors[i].y){
					inversor = 1;
				}else{
					inversor = -1;
				}
				camG += colorSpeed * inversor;
			}else{
				cG = true;
			}

			if(!Mathf.Approximately(camB, backColors[i].z)){
				if(camB < backColors[i].z){
					inversor = 1;
				}else{
					inversor = -1;
				}
				camB += colorSpeed * inversor;
			}else{
				cB = true;
			}

			if(cR && cG && cB){
				cR = false;
				cG = false;
				cB = false;
				callBackgroundChanger = false;
			}else{
				cam.backgroundColor = new Color(camR, camG, camB, 1);
			}
		}
	}

	public static float ModifyEnergyBar(float modification){
		Transform energyBar = GameObject.Find("Energy").transform;
		float energyVariation = energyBar.localScale.y + modification * Time.deltaTime;
		energyVariation = Mathf.Clamp(energyVariation, 0, 27.0f);
		energyBar.localScale = new Vector3(energyBar.localScale.x, energyVariation, 0);
		return energyVariation;
	}

	void IndexColorSetter(){
		actualColor++;
		if(actualColor == backColors.Length){
			actualColor = 0;
		}
	}

	void SpawnCoin(){
		if(Random.Range(0,10) < chanceSpawnCoin){
			InstantiateObj(coinObj, spawnEnemyY + 2, Random.Range(spawnLimitX.x, spawnLimitX.y));
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		backColors[0] = cBrown;
		backColors[1] = cGreen;
		backColors[2] = cPurple;

		tempTimerChangeBack = timeChangeBack;
		tempDifficultModifier = difficultModifier;
		monsterAcceleration = monsterInitialAcceleration;

		InvokeRepeating("MonsterSpawn", 3.0f, 1.0f);
		InvokeRepeating("StructureSpawn", 3.0f, 2.0f);
		InvokeRepeating("BadEyeSpawn", difficultModifier * 3, 10.0f);
		InvokeRepeating("SpawnCoin", 3.0f, 1.0f);

	}

	void Update(){
		PauseGame();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(gameRunning){
			if(Input.GetKey(KeyCode.Space) && ModifyEnergyBar(energyLost) > 0){
				BreakObjectsMovement();
				slowTime = true;
			}else{
				slowTime = false;
				AcelerateObjectsMovement();
			}

			TimeManager();
			BackgroundChanger(actualColor);

			foreach (Transform child in transform){
				MovesObject(child);
				Destroyer(child);
			}
		}
	}
}
