using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;




public class UpdateStats : MonoBehaviour {

	public int sessionPoints;
	public int sessionRound;
	public int Highestround;
    public int highScore;
    public int enemieskilled;
	public int sessionkills;
	public Text totalKills;
	public Text highestRound;
    public Text highestScore;
    public int timeplayed;
    private Text timeplyd;
    public TimeSpan timeSpan;

    void Start ()
	{
		sessionRound = 0;
        sessionPoints = 0;

    }

	void Awake()
	{

	}

	void Update ()
	{
        if (sessionPoints > highScore)
        {
            highScore = sessionPoints;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void PausePress()
    {
        totalKills = GameObject.Find("TKText").GetComponent<Text>();
        highestRound = GameObject.Find("HRText").GetComponent<Text>();
        highestScore = GameObject.Find("HSText").GetComponent<Text>();
        enemieskilled = PlayerPrefs.GetInt("TotalKills");
        Highestround = PlayerPrefs.GetInt("HighestRound");
        highScore = PlayerPrefs.GetInt("HighScore");
        totalKills.text = "" + enemieskilled.ToString();
        highestRound.text = "" + Highestround.ToString();
        highestScore.text = "" + highScore.ToString();
    }

    public void OpeningPress()
    {
        totalKills = GameObject.Find("KillText").GetComponent<Text>();
        highestRound = GameObject.Find("HighText").GetComponent<Text>();
        highestScore = GameObject.Find("HSText").GetComponent<Text>();
        timeplayed = PlayerPrefs.GetInt("TotalTime");
        timeSpan = TimeSpan.FromSeconds(timeplayed);
        timeplyd = GameObject.Find("TimeText").GetComponent<Text>();
        string timeText = string.Format("{0:D2} Days\n{1:D2} Hours\n{2:D2} Minutes", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        timeplyd.text = "" + timeText.ToString();
        Highestround = PlayerPrefs.GetInt("HighestRound");
        highScore = PlayerPrefs.GetInt("HighScore");
        enemieskilled = PlayerPrefs.GetInt("TotalKills");
        totalKills.text = "" + enemieskilled.ToString();
        highestRound.text = "" + Highestround.ToString();
        highestScore.text = "" + highScore.ToString();
    }

	public void enemykill()
	{
		enemieskilled += 1;
	}

    public void roundsplayed()
	{
		if (sessionRound > Highestround)
		{
            Highestround = sessionRound;
            PlayerPrefs.SetInt("HighestRound", Highestround);
        } 

		else 
		{
            return;
		}
	}
}
