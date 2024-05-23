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

    private int Point_u = 0;
    private int Point_b = 0;
    private int Point_k = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SetScore()
    {
        iScore += 10;
        textScore.text = "Score:" + iScore.ToString();

        //�X�R�A��ۑ����Ȃ��Ɖ��ĂȂ��Ȃ�
        PlayerPrefs.SetInt("SCORE", iScore);
        PlayerPrefs.Save();
    }

    public void SetPoint_u()
    {
        Point_u += 1;

        //�X�R�A��ۑ����Ȃ��Ɖ��ĂȂ��Ȃ�
        PlayerPrefs.SetInt("point_u", Point_u);
        PlayerPrefs.Save();
    }

    public void SetPoint_b()
    {
        Point_b += 1;

        //�X�R�A��ۑ����Ȃ��Ɖ��ĂȂ��Ȃ�
        PlayerPrefs.SetInt("point_b", Point_b);
        PlayerPrefs.Save();
    }

    public void SetPoint_k()
    {
        Point_k += 1;

        //�X�R�A��ۑ����Ȃ��Ɖ��ĂȂ��Ȃ�
        PlayerPrefs.SetInt("point_k", Point_k);
        PlayerPrefs.Save();
    }

    public void SetHighScore(float gScore)
    {
        int hScore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        //�n�C�X�R�A���X�V������
        if (hScore < gScore)
        {
            //�X�R�A�𑗐M
            pfContoroller.SubmitScore();
            audioSource.PlayOneShot(highscoreSE);
            hScore = (int)gScore;
            PlayerPrefs.SetInt("HIGHSCORE", hScore);
            PlayerPrefs.Save();
        }
        //�n�C�X�R�A��\��
        highScore.text = "HighScore:" + hScore.ToString();

    }
}