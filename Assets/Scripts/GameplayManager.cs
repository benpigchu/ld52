using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public enum GameView
{
	Title,
	Playing,
	Result,
}

public class GameplayManager : MonoBehaviour
{
	static public GameplayManager Instance;

	public float timeLimit = 120;

	public TextMeshProUGUI timerText;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI resultText;

	public GameObject PlayingUILayer;
	public GameObject ResultUILayer;
	public GameObject TitleUILayer;
	public GameObject Playfield;

	public GameView initialView = GameView.Title;
	public GameView currentView = GameView.Title;
	private int score;
	private float timeRemain;

	private string resultTemplate;
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		timeRemain = timeLimit;
		resultTemplate = resultText.text;
	}

	// Start is called before the first frame update
	void Start()
	{
		SetGameView(initialView);
	}

	// Update is called once per frame
	void Update()
	{
		if (currentView == GameView.Playing)
		{
			timeRemain -= Time.deltaTime;
			int timeRemainSeconds = Math.Max(0, Mathf.CeilToInt(timeRemain));
			timerText.text = $"{timeRemainSeconds / 60}:{timeRemainSeconds % 60:D2}";
			if (timeRemain < 0)
			{
                resultText.text=string.Format(resultTemplate,score);
				SetGameView(GameView.Result);
			}
		}
		else
		{
			if (Keyboard.current.spaceKey.IsPressed())
			{
				timeRemain = timeLimit;
				score = 0;
				scoreText.text = $"{score}";
				SetGameView(GameView.Playing);
				CropManager.Instance.Reset();
			}
		}
	}

	void SetGameView(GameView view)
	{
		currentView = view;
		PlayingUILayer.SetActive(view == GameView.Playing);
		ResultUILayer.SetActive(view == GameView.Result);
		TitleUILayer.SetActive(view == GameView.Title);
		Playfield.SetActive(view != GameView.Title);
	}

	public void AddScore(int value)
	{
		score += value;
		scoreText.text = $"{score}";
	}
}
