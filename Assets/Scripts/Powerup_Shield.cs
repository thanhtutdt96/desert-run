using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Shield : MonoBehaviour
{
	public GameObject pickupEffect;
	private GameObject[] obstacles;

	public float duration;

	void Update ()
	{
		transform.Rotate (transform.position.x, transform.position.y, -4f);
		obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
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

		FindObjectOfType<FollowEffect> ().ShieldFollowOn ();

		// Powerup handler
		foreach (GameObject go in obstacles) {
			go.GetComponent<Collider> ().enabled = false;
		}

		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<Collider> ().enabled = false;

		yield return new WaitForSeconds (duration);
		// Return back to normal
		FindObjectOfType<FollowEffect> ().ShieldFollowOff ();

		foreach (GameObject go in obstacles) {
			go.GetComponent<Collider> ().enabled = true;
		}

		Destroy (gameObject);
	}
}
