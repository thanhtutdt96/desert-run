using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
	public float moveSpeed;
	public float jumpSpeed;

	private float verticalVerlocity;
	private float gravity = 12f;
	private Vector3 movement;

	private CharacterController controller;
	private Animator animator;

	private float animationDuration = 3f;
	private float startTime;

	private bool isDead = false;
	private bool isJumping = false;
	private bool controlLocked;


	void Start ()
	{
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> ();
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update ()
	{		
		if (isDead) {
			return;
		}

		if (Time.time - startTime < animationDuration) {
			controller.Move (new Vector3 (0, -0.5f, moveSpeed) * Time.deltaTime);
			return;
		}

		if (controller.isGrounded) {
			verticalVerlocity -= gravity * Time.deltaTime;
			// Player jump
			if (Input.GetAxis ("Vertical") > 0 && !controlLocked) {
				isJumping = true;

				FindObjectOfType<AudioManager> ().Play ("Jump");

				animator.SetTrigger ("Jump");
				StartCoroutine (LockControl ());
				controlLocked = true;

				verticalVerlocity = jumpSpeed;
			}
			// Player slide
			if (Input.GetAxis ("Vertical") < 0 && !controlLocked) {
				FindObjectOfType<AudioManager> ().Play ("Slide");

				animator.SetTrigger ("Slide");
				StartCoroutine (LockControl ());
				controlLocked = true;
				controllerCenterWhenSlide ();
			}
		} else {
			if (isJumping) {
				verticalVerlocity -= gravity * Time.deltaTime;

				// Jump down hole
				if (-11f < verticalVerlocity && verticalVerlocity < -10f)
					FindObjectOfType<AudioManager> ().Play ("Fall");
				if (verticalVerlocity < -11f) {
					Death ();
					FindObjectOfType<AudioManager> ().Play ("Fall");
				}
			} else {
				Death ();
				FindObjectOfType<AudioManager> ().Play ("Fall");

			}
			
		}
			
		//movement = Vector3.zero;
		movement.x = Input.GetAxis ("Horizontal") * moveSpeed;

		// Control by click to build on smartphone
		if (Input.GetMouseButton (0)) {
			if (Input.mousePosition.x > Screen.width / 2)
				movement.x = moveSpeed;
			else
				movement.x = -moveSpeed;
		}

		movement.y = verticalVerlocity;
		movement.z = moveSpeed;

		controller.Move (movement * Time.deltaTime);

	}
	void controllerCenterWhenSlide(){
		controller.center = new Vector3 (controller.center.x, 0.4f, controller.center.z);
		controller.height = 0.9f;
	}

	void controllerCenterDefault(){
		controller.center = new Vector3 (controller.center.x, 0.8f, controller.center.z);
		controller.height = 1.6f;
	}
	// Wait for animator to completed
	IEnumerator LockControl ()
	{
		yield return new WaitForSeconds (1f);
		controlLocked = false;
	}



	// Obstacle hit
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (hit.point.z > transform.position.z + controller.radius && hit.gameObject.tag == "Obstacle") {
			Death ();
			FindObjectOfType<AudioManager> ().Play ("HitObstacle");
		}
	}

	public void SetSpeed (float modifier)
	{
		moveSpeed = 6f + modifier;
	}

	private void Death ()
	{
		isDead = true;
		animator.SetTrigger ("Dead");
		GetComponent<Score> ().OnDeath ();
	}
		
}
