using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text textScore;
    int iScore = 0;
    public void SetScore()
    {
        iScore += 10;
        textScore.text = "Score:" + iScore.ToString();

        //�X�R�A��ۑ����Ȃ��Ɖ��ĂȂ��Ȃ�
        PlayerPrefs.SetInt("SCORE", iScore);
        PlayerPrefs.Save();
    }

}