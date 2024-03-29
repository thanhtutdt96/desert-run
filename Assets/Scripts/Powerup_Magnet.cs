﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Magnet : MonoBehaviour
{
	public GameObject pickupEffect;
	private GameObject[] gems;

	public float duration;

	void Update ()
	{
		transform.Rotate (transform.position.x, transform.position.y, -4f);
		gems = GameObject.FindGameObjectsWithTag ("Gem");

	}

	void OnTriggerEnter (Collider other)
	{		
		if (other.tag == "Player") {
			FindObjectOfType<AudioManager> ().Play ("Powerup");
			StartCoroutine (Pickup (other));
		}
	}

	IEnumerator Pickup (Collider other)
	{		
		GameObject pickup = Instantiate (pickupEffect, transform.position, transform.rotation);
		Destroy (pickup, 2f);
	
		FindObjectOfType<FollowEffect> ().MagnetFollowOn ();
		// Powerup handler
		foreach (GameObject go in gems) {
			go.GetComponent<Magnet> ().enabled = true;

		}

		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<Collider> ().enabled = false;

		yield return new WaitForSeconds (duration);
		// Return back to normal
		FindObjectOfType<FollowEffect> ().MagnetFollowOff ();

		foreach (GameObject go in gems) {
			go.GetComponent<Magnet> ().enabled = false;
		}

		Destroy (gameObject);
	}
}
