using UnityEngine;
using System.Collections;

public class ResetAllForTesting : MonoBehaviour {

	void Start ()
    {
        if (PlayerPrefs.GetInt("ResetSettings") == 0)
        {
            PlayerPrefs.SetFloat("CurVol", 1);
            PlayerPrefs.SetFloat("CurMusVol", 1);
            PlayerPrefs.SetFloat("CurEffVol", 1);
            PlayerPrefs.SetInt("TotalTime", 0);
            PlayerPrefs.SetInt("Punk", 0);
            PlayerPrefs.SetInt("Dino", 0);
            PlayerPrefs.SetInt("Mach", 0);
            PlayerPrefs.SetInt("Alian", 0);
            PlayerPrefs.SetInt("King", 0);
            PlayerPrefs.SetInt("Death", 0);
            PlayerPrefs.SetInt("HighestRound", 0);
            PlayerPrefs.SetInt("TotalKills", 0);
            PlayerPrefs.SetInt("Hat", 1);
            PlayerPrefs.SetInt("Character", 0);
            PlayerPrefs.SetInt("bandanna1", 0);
            PlayerPrefs.SetInt("bandanna2", 0);
            PlayerPrefs.SetInt("bandanna3", 0);
            PlayerPrefs.SetInt("ballcap", 0);
            PlayerPrefs.SetInt("beanie", 0);
            PlayerPrefs.SetInt("chief", 0);
            PlayerPrefs.SetInt("cowboy", 0);
            PlayerPrefs.SetInt("crown", 0);
            PlayerPrefs.SetInt("grad", 0);
            PlayerPrefs.SetInt("halo", 0);
            PlayerPrefs.SetInt("headphones", 0);
            PlayerPrefs.SetInt("helmet1", 0);
            PlayerPrefs.SetInt("helmet2", 0);
            PlayerPrefs.SetInt("santa", 0);
            PlayerPrefs.SetInt("sombrero", 0);
            PlayerPrefs.SetInt("sunhat", 0);
            PlayerPrefs.SetInt("tophat", 0);
            PlayerPrefs.SetInt("vizor", 0);
            PlayerPrefs.SetInt("wizard", 0);
            PlayerPrefs.SetInt("snow", 0);
            PlayerPrefs.SetInt("flower", 0);
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("Vibrate", 0);
            PlayerPrefs.SetInt("ResetSettings", 1);
        }
    }
	
	void Update ()
    {
	
	}
}
