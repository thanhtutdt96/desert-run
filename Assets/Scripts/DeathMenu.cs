using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenu : MonoBehaviour
{

	public TextMeshProUGUI scoreText;

	public Image background;

	private bool isDisplayed = false;
	// Display the death menu

	private float transition = 0f;
	// Use this for initialization
	void Start ()
	{
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isDisplayed)
			return;

		transition += Time.deltaTime;
		background.color = Color.Lerp (new Color (0, 0, 0, 0), Color.black, transition);
	}

	public void ToogleEndMenu (float score)
	{
		gameObject.SetActive (true);
		scoreText.SetText ("Score: " + (int)score);
		isDisplayed = true;
	}

	public void Restart ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void ToMenu ()
	{
		SceneManager.LoadScene ("Start");
	}
}
