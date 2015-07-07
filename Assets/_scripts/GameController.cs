using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//Enemy Wave Variables
	public GameObject hazard;
	public Vector3 spawnValue;
	public int hazardCount;
	public float startWait;
	public float spawnWait;
	public float waveWait;
	public GameObject miniBoss;
	public float bossHealth;

	//UI Variables
	public int score;
	public Text scoreText;
	public Image shieldIndicator;
	public bool shieldsUp;
	public Slider beatWave;

	private float beatWaveValue;
	private BossController bossTracker;

	//Initialization
	void Start () {
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves());
		shieldsUp = true;
		beatWave.handleRect.gameObject.SetActive (false);
		StartCoroutine ("UpdateBeat");
	}

	void Update(){
		ShieldUpdate ();
	}

	//Spawns hazard waves
	IEnumerator SpawnWaves (){
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosistion = new Vector3 (spawnValue.x, Random.Range (-spawnValue.y, spawnValue.y), spawnValue.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosistion, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

	//Controls the beat meter UI
	IEnumerator UpdateBeat (){
		beatWaveValue = 0;
		for(int i = 0; i <= 4; i++){
			while (beatWaveValue < i) {
				beatWave.value = beatWaveValue;
				beatWaveValue += Time.deltaTime;
				yield return null;
			}
			beatWave.handleRect.gameObject.SetActive (true);
			yield return null;
			beatWaveValue += Time.deltaTime;
			yield return null;
			beatWave.handleRect.gameObject.SetActive (false);
		}
		StartCoroutine("UpdateBeat");
	}

	//allow other scripts to update the score variable tracked by the game controller
	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	//provide feedback on shield status
	public void ShieldUpdate(){
		if (shieldsUp == false)
			shieldIndicator.gameObject.SetActive (false);
		else
			shieldIndicator.gameObject.SetActive (true);
	}

	//function to be called whenever the score changes
	void UpdateScore () {
		scoreText.text = "Score: " + score;

		//Spawn Boss at score
		if (score == 3) {
			MiniBoss();
		}
	}

	void MiniBoss(){
		Instantiate (miniBoss, new Vector3 (11, 0, 0), Quaternion.Euler (0, 270, 270));
		bossHealth = miniBoss.GetComponent<BossController>().health;
	}


	
}
