using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public Rigidbody mover;
	public float speed;

	void Start() 
	{
		mover = GetComponent<Rigidbody> ();

		if (mover.tag == "Hazard" || mover.tag == "Pickup") {
			mover.velocity = transform.right * -speed;
		}else{
			mover.velocity = transform.up * speed;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
