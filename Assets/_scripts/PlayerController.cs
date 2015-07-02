using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float yMin, yMax, xMin, xMax;
}

public class PlayerController : MonoBehaviour 
{
	public Rigidbody Player;
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate = 0.5F;
	private float nextFire = 0.0F;
	AudioSource shotsound;

	void Start()
	{
		Player = GetComponent<Rigidbody> ();
		shotsound = GetComponent<AudioSource> ();
	}


	void Update() {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			shotsound.Play ();
		}

		if (shotsound.time > .4){
			shotsound.Stop ();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

		//Player.AddForce(movement * speed * Time.deltaTime);
		Player.velocity = (movement * speed * Time.deltaTime);
		Player.position = new Vector3
			(
				Mathf.Clamp (Player.position.x, boundary.xMin, boundary.xMax),
				Mathf.Clamp (Player.position.y, boundary.yMin, boundary.yMax),
				0.0f
			);

		Player.rotation = Quaternion.Euler (0.0f, 90, Player.velocity.y * tilt);
	}
	
}
