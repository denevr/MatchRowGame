using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	public static GUIManager instance;

	public GameObject highScoreText;
	public GameObject scoreText;
	public GameObject moveCountText;
	public GameObject levelText;
	private int score;
	private int moveCounter;
	private int level;

	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
			scoreText.transform.gameObject.GetComponentInChildren<TextMesh>().text = "Score: " + score.ToString();
		}
	}

	public int Level
	{
		get
		{
			return level;
		}
		set
		{
			level = value;
			levelText.transform.gameObject.GetComponentInChildren<TextMesh>().text = "Level: " + level.ToString();
		}
	}

	public int MoveCounter
	{
		get
		{
			return moveCounter;
		}
		set
		{
			moveCounter = value;
			if (moveCounter <= 0)
			{
				moveCounter = 0;
				SceneManager.LoadScene("Animation", LoadSceneMode.Single);
				onGameEnd();
			}
			moveCountText.transform.gameObject.GetComponentInChildren<TextMesh>().text = "Moves Left: " + moveCounter.ToString();
		}
	}

	void Awake()
	{
		instance = GetComponent<GUIManager>();
		moveCounter = 20;
		moveCountText.transform.gameObject.GetComponentInChildren<TextMesh>().text = "Moves Left: " + moveCounter.ToString();
	}

	void Start()
    {
		highScoreText.transform.gameObject.GetComponentInChildren<TextMesh>().text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
	}

	void Update()
    {
        
    }

	public void onGameEnd()
	{
		GameManager.instance.gameOver = true;

		if (score > PlayerPrefs.GetInt("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore", score);
		}
	}
}
