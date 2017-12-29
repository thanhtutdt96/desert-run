using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

	private Transform lookAt;
	private Vector3 startOffset;
	private Vector3 moveVector;

	private float transition = 0.5f;
	private float animationDuration = 4f;
	private Vector3 animationOffset = new Vector3 (0, 12, -18);

	private Vector3 cameraLift = new Vector3 (0, 3, 3.2f);

	// Use this for initialization
	void Start ()
	{
		lookAt = GameObject.FindGameObjectWithTag ("Player").transform;
		startOffset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		moveVector = lookAt.position + startOffset;

		// moveVector.x = 0;
		moveVector.y = Mathf.Clamp (moveVector.y, 2, 4);

		if (transition > 1f) {
			transform.position = moveVector;
		} else {
			// Animation at the start of the game
			transform.position = Vector3.Lerp (moveVector + animationOffset, moveVector, transition);
			transition += Time.deltaTime * 1 / animationDuration;
			transform.LookAt (lookAt.position + cameraLift);
		}
	}
}
