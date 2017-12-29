using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
	private Transform player;

	public float distance;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	void Update ()
	{
		if (transform.position.z - player.position.z <= distance)
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (player.position.x, player.position.y + 1f, player.position.z), 10f * Time.deltaTime);
		
	}

}
