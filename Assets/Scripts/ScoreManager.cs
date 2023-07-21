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

    private AudioSource audioSource;
    public AudioClip highscoreSE;

    public PlayFabController pfContoroller;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SetScore()
    {
        iScore += 10;
        textScore.text = "Score:" + iScore.ToString();

        //スコアを保存しないと壊れてなくなる
        PlayerPrefs.SetInt("SCORE", iScore);
        PlayerPrefs.Save();
    }

    public void SetHighScore(float gScore)
    {
        int hScore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        //ハイスコアを更新したら
        if (hScore < gScore)
        {
            //スコアを送信
            pfContoroller.SubmitScore();
            audioSource.PlayOneShot(highscoreSE);
            hScore = (int)gScore;
            PlayerPrefs.SetInt("HIGHSCORE", hScore);
            PlayerPrefs.Save();
        }
        //ハイスコアを表示
        highScore.text = "HighScore:" + hScore.ToString();

    }
}