using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

	private float score = 0.0f;
	private int currentGold;

	private int difficultyLevel = 3;
	private int maxDifficultyLevel = 10;
	private int scoreToNextLevel = 10;

	private bool isDead = false;

	public Text scoreText;
	public Text goldText;

	public DeathMenu deathMenu;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isDead)
			return;
		if (score >= scoreToNextLevel) {
			LevelUp ();
		}
		score += Time.deltaTime * difficultyLevel;
		scoreText.text = ((int)score).ToString ();
	}

	void LevelUp ()
	{
		if (difficultyLevel == maxDifficultyLevel)
			return;
		scoreToNextLevel *= 2;
		difficultyLevel++;
		if (gameObject.name == "Max")
			GetComponent<MaxController> ().SetSpeed (difficultyLevel);
		if (gameObject.name == "Unity-chan")
			GetComponent<UnityChanController> ().SetSpeed (difficultyLevel);
	}

	public void OnDeath ()
	{
		isDead = true;

		// Saving the high score
		PlayerPrefs.SetFloat ("Highscore", score);

		// Open death menu when player is dead
		deathMenu.ToogleEndMenu (score + currentGold);
	}

	public void AddGold (int goldToAdd)
	{
		currentGold += goldToAdd;
		goldText.text = "" + currentGold;
	}
}
