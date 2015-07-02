using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
	public AudioClip[] asteroidExplosion;
	private AudioSource bang;

	// Use this for initialization
	void Awake ()
	{
		bang = GetComponent<AudioSource> ();
		int ran = Random.Range (0, 3);
		bang.clip = asteroidExplosion [ran];
		bang.Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

