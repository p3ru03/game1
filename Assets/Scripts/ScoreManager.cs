using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    public Text textScore;
    public Text highScore;
    int iScore = 0;
    public void SetScore()
    {
        iScore += 10;
        textScore.text = "Score:" + iScore.ToString();

        //ƒXƒRƒA‚ð•Û‘¶‚µ‚È‚¢‚Æ‰ó‚ê‚Ä‚È‚­‚È‚é
        PlayerPrefs.SetInt("SCORE", iScore);
        PlayerPrefs.Save();
    }

    public void SetHighScore(float gScore)
    {
        int hScore = Math.Max(PlayerPrefs.GetInt("HIGHSCORE",0), (int)gScore);
        PlayerPrefs.SetInt("HIGHSCORE", hScore);
        PlayerPrefs.Save();
        highScore.text = "HighScore:" + hScore.ToString();
    }
}