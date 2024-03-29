using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
	private GameObject[] mapList;
	private int index;

	public Text mapName;

	void Start ()
	{
		mapName.text = "Day";

		mapList = new GameObject[transform.childCount];

		// Fill the array with models
		for (int i = 0; i < transform.childCount; i++) {
			mapList [i] = transform.GetChild (i).gameObject;
		}

		// Toogle off their renderer
		foreach (GameObject go in mapList) {
			go.SetActive (false);
		}

		if (mapList [0])
			mapList [0].SetActive (true);
	}

	public void ToogleLeft ()
	{
		FindObjectOfType<AudioManager> ().Play ("Click2");

		mapList [index].SetActive (false);

		index--;
		if (index < 0)
			index = mapList.Length - 1;

		mapList [index].SetActive (true);	

		if (index % 2 == 0)
			mapName.text = "Day";
		else
			mapName.text = "Night";
	}

	public void ToogleRight ()
	{
		FindObjectOfType<AudioManager> ().Play ("Click2");

		mapList [index].SetActive (false);

		index++;
		if (index == mapList.Length)
			index = 0;

		mapList [index].SetActive (true);	

		if (index % 2 == 0)
			mapName.text = "Day";
		else
			mapName.text = "Night";
	}

	public void HomeButton ()
	{
		PlayerPrefs.SetInt ("MapSelected", index);
	}
		
}
