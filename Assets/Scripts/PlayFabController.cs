using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabController : MonoBehaviour
{
    //PlayFab��Ō��߂����
    const string STATISTICS_NAME = "HighScore";

    public EndScore endscore;
    public int highScore;

    public Text highscoreText;

    void Start()
    {
        Login();
    }

    void Update()
    {
       
    }

    public void Login()
    {
        //�J�X�^��ID�Ń��O�C��
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest { CustomId = "TestID", CreateAccount = true },
            result => Debug.Log("���O�C�������I"),
            error => Debug.Log("���O�C�����s"));
    }

    //�X�R�A�𑗐M
    public void SubmitScore()
    {
        //�قƂ�ǌ��܂蕶��
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate
                    {
                        StatisticName = STATISTICS_NAME,
                        Value = (int)endscore.getScore
                    }
                }
            },
            result =>
            {
                Debug.Log("�X�R�A���M");
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
    }
    //�X�R�A���擾
    public void GetPlayerStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            result =>
            {
                // ���v���̎擾�ɐ��������ꍇ�̏���
                foreach (var stat in result.Statistics)
                {
                    if (stat.StatisticName == STATISTICS_NAME)
                    {
                        highScore = stat.Value;
                        // �n�C�X�R�A��\������
                        highscoreText.text = "HighScore:" + highScore.ToString();
                    }
                }
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }


    //�����L���O���擾
    void RequestLeaderBoard()
    {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest
            {
                StatisticName = STATISTICS_NAME,
                StartPosition = 0,
                MaxResultsCount = 5
            },
            result =>
            {
                result.Leaderboard.ForEach(
                    x => Debug.Log(string.Format("{0}��:{1} �X�R�A{2}", x.Position + 1, x.DisplayName, x.StatValue))
                    );
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
    }

    //���O�̍X�V
    public void SetPlayerDisplayName(string displayName)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = displayName
            },
            result => {
                Debug.Log("Set display name was succeeded.");
    
            },
            error => {
                Debug.LogError(error.GenerateErrorReport());
            }
        );
    }
}