using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
	private GameObject[] characterList;
	private GameObject character;
	private int index;
	private Animator animator;

	public Text characterName;

	void Start ()
	{
		characterName.text = "Max";
		characterList = new GameObject[transform.childCount];

		// Fill the array with models
		for (int i = 0; i < transform.childCount; i++) {
			characterList [i] = transform.GetChild (i).gameObject;
		}

		// Toogle off their renderer
		foreach (GameObject go in characterList) {
			go.SetActive (false);
		}

		if (characterList [0]) {
			characterList [0].SetActive (true);
			animator = characterList [0].GetComponent<Animator> ();
		}

	}

	void Update ()
	{	
		runAndStop ();

		transform.Rotate (new Vector3 (0f, 0.5f, 0f));
	}

	public void ToogleLeft ()
	{
		FindObjectOfType<AudioManager> ().Play ("Click2");

		characterList [index].SetActive (false);

		index--;
		if (index < 0)
			index = characterList.Length - 1;
		
		characterList [index].SetActive (true);
		animator = characterList [index].GetComponent<Animator> ();

		if (index % 2 == 0) {
			characterName.text = "Max";
		} else {
			characterName.text = "Unity-chan";
		}
	}

	public void ToogleRight ()
	{
		FindObjectOfType<AudioManager> ().Play ("Click2");

		characterList [index].SetActive (false);

		index++;
		if (index == characterList.Length)
			index = 0;

		characterList [index].SetActive (true);	
		animator = characterList [index].GetComponent<Animator> ();

		if (index % 2 == 0)
			characterName.text = "Max";
		else
			characterName.text = "Unity-chan";
	}

	public void HomeButton ()
	{
		FindObjectOfType<AudioManager> ().Play ("Click");
		FindObjectOfType<AudioManager> ().Pause ("Background");
		PlayerPrefs.SetInt ("CharacterSelected", index);
		SceneManager.LoadScene ("Start");
	}

	void runAndStop ()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit = new RaycastHit ();        
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			character = transform.GetChild (index).gameObject;
			
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject == character) {
					animator.SetTrigger ("Run");
					FindObjectOfType<AudioManager> ().Play ("Run");

				} else {
					Debug.Log ("Click outside");
				}
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			animator.SetTrigger ("Idle");
			FindObjectOfType<AudioManager> ().Stop ("Run");
		}
	}
}
