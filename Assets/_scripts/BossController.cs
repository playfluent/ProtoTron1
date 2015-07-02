using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {
	
	public Rigidbody miniBoss;
	public float speed;
	public float health;

	public float yMax;
	public float yMin;

	private Vector3 bossVelocity;

	void Awake() 
	{
		miniBoss = GetComponent<Rigidbody> ();
		bossVelocity = new Vector3 (0, -speed * Time.deltaTime, 0);
	}

	// Update is called once per frame
	void Update () {

		//move the boss up and down
		if(miniBoss.position.y < yMin){
			bossVelocity.y = - bossVelocity.y;
			miniBoss.position = new Vector3(miniBoss.position.x, yMin, miniBoss.position.z);
		} else if ( miniBoss.position.y > yMax){
			bossVelocity.y = - bossVelocity.y;
			miniBoss.position = new Vector3(miniBoss.position.x, yMax, miniBoss.position.z);
		}
		miniBoss.velocity = bossVelocity;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Laser") {
			health = health - 1;
		}
	}
}
