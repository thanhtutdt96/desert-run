using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;

using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	private int character;
	private int map;

	public TextMeshProUGUI highScore;

	void Start ()
	{
		GameObject.FindGameObjectWithTag ("ButtonMute").SetActive (false);
		highScore.SetText ("<size=+5>H</size>ighscore: " + "<size=+5>" + (int)PlayerPrefs.GetFloat ("Highscore"));

		character = PlayerPrefs.GetInt ("CharacterSelected");
		map = PlayerPrefs.GetInt ("MapSelected");
	}

	public void ToGame ()
	{
		if (character % 2 == 0 && map % 2 == 0)
			SceneManager.LoadScene ("Max_Day");
		else if (character % 2 == 0 && map % 2 != 0)
			SceneManager.LoadScene ("Max_Night");
		else if (character % 2 != 0 && map % 2 == 0)
			SceneManager.LoadScene ("Unity-chan_Day");
		else if (character % 2 != 0 && map % 2 != 0)
			SceneManager.LoadScene ("Unity-chan_Night");
	}

	public void ToStart ()
	{
		SceneManager.LoadScene ("Start");
	}

	public void ToChooseCharacterMap ()
	{
		SceneManager.LoadScene ("ChooseCharacterMap");

	}

	public void ToSetting ()
	{
		SceneManager.LoadScene ("Setting");
	}

}
