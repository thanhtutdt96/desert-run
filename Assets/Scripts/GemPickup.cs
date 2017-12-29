using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickup : MonoBehaviour
{
	public int goldValue;

	public GameObject pickupEffect;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (transform.position.x, transform.position.y, -4);
	}

	private void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			FindObjectOfType<Score> ().AddGold (goldValue);
			gameObject.SetActive (false);
			Instantiate (pickupEffect, transform.position, transform.rotation);
			FindObjectOfType<AudioManager> ().Play ("Gem");
		}
	}
}
