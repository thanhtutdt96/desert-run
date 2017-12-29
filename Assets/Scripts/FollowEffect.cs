using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEffect : MonoBehaviour {

	private GameObject[] characterList;

	void Start () {
		characterList = new GameObject[transform.childCount];

		// Fill the array with models
		for (int i = 0; i < transform.childCount; i++) {
			characterList [i] = transform.GetChild (i).gameObject;
		}

		// Toogle off their renderer
		foreach (GameObject go in characterList) {
			go.SetActive (false);
		}
	}
	
	public void ShieldFollowOn()
	{
		characterList [0].SetActive (true);
	}

	public void ShieldFollowOff()
	{
		characterList [0].SetActive (false);
	}

	public void DoubleFollowOn()
	{
		characterList [1].SetActive (true);
	}

	public void DoubleFollowOff()
	{
		characterList [1].SetActive (false);
	}

	public void MagnetFollowOn()
	{
		characterList [2].SetActive (true);
	}

	public void MagnetFollowOff()
	{
		characterList [2].SetActive (false);
	}
}
