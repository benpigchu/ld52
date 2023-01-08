using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameplayManager : MonoBehaviour
{
	static public GameplayManager Instance;

    public float timeLimit=120;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    private int score;
    private float timeRemain;
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
        timeRemain=timeLimit;
	}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeRemain-=Time.deltaTime;
        int timeRemainSeconds=Math.Max(0,Mathf.CeilToInt(timeRemain));
        timerText.text=$"{timeRemainSeconds/60}:{timeRemainSeconds%60:D2}";
    }

    public void AddScore(int value){
        score+=value;
        scoreText.text=$"{score}";
    }
}
