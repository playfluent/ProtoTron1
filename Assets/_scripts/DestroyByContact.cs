﻿using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public GameObject pickup;
	public int scoreValue;

	private GameController gameController;
	//private PlayerController playerController;

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

		/* Reference for PlayerController script on Player
		GameObject playerControllerObject = GameObject.FindWithTag ("Player");
		if (playerControllerObject != null)
		{
			playerController = playerControllerObject.GetComponent <PlayerController>();
		}
		if (playerControllerObject == null)
		{
			Debug.Log ("Cannot find 'PlayerController' script");
		}*/
	}

	void OnTriggerEnter(Collider other)
	{


		//Boundary does not effect this script
		if (other.tag == "Boundary") 
			return;

		//Hazard and Boss don't collide
		if (other.tag == "Boss" && this.tag == "Hazard" || other.tag == "Hazard" && this.tag == "Boss")
			return;

		//Hazard and PickUp don't collide
		if (other.tag == "Pickup" && this.tag == "Hazard")
			return;

		//Lasers' always create explosions and add score
		if (other.tag == "Laser"){
			Instantiate (explosion, transform.position, transform.rotation);
			gameController.AddScore (scoreValue);

			//Randomly drop pickup
			int x = Random.Range(0,2);
			if(x == 1){
				Instantiate(pickup,transform.position,Quaternion.identity);
			}

			//Lasers only destroy Boss if health is 0
			if(this.tag == "Boss"){
				if(gameController.bossHealth > 0){
					//lasers take 1 health
					gameController.bossHealth--;
					return;
				}
			}
		}

		//Players die only when shields are down	
		if (other.tag == "Player") {
			if (gameController.shieldsUp == false)
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			else {
				gameController.shieldsUp = false;
				Destroy (gameObject);
				return;
			}
		}
		Destroy (other.gameObject);
		Destroy (gameObject);
	}

}
