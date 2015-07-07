using UnityEngine;
using System.Collections;

public class PickupTrigger : MonoBehaviour {

	public int scoreValue;
	public int power;

	private GameController gameController;
	AudioSource pickupSound;


	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		pickupSound = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			pickupSound.Play ();

			//if (pickupSound.time > .4)
			//	pickupSound.Stop ();

			PowerUp(power);
			gameController.AddScore (scoreValue);
			Destroy (gameObject);
		}
	}

	//assigns power of pickup 
	//*should convert this to string later
	void PowerUp(int power){

		//1. Add Shields
		if (power == 1) {
			//If shields are down add shields
			if (gameController.shieldsUp == false){
				gameController.shieldsUp = true;
			}
		}

		//2. Double Score
		if (power == 2){
			gameController.AddScore(1000);
		}
	}

}
