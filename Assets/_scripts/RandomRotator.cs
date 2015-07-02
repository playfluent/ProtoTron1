using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;
	public Rigidbody asteroid;
	// Use this for initialization
	void Start () {
		asteroid = GetComponent<Rigidbody> ();
		asteroid.angularVelocity = Random.insideUnitSphere * tumble;
	}

}
