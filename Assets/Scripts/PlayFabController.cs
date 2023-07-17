using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayFabController : MonoBehaviour
{
    //PlayFab��Ō��߂����
    const string STATISTICS_NAME = "HighScore";

    public EndScore endscore;
    public int highScore;

    public Text highscoreText;

    public Text first;
    public Text second;
    public Text third;
    public Text yours;

    string unknown = "unknown";

    void Start()
    {
        Login();
    }

    void Update()
    {

    }

    public void Login()
    {
        //�Q��ڈȍ~�̃��O�C��
        if (PlayerPrefs.HasKey("loginID"))
        {
            //�J�X�^��ID�Ń��O�C��
            PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest { CustomId = PlayerPrefs.GetString("loginID"), CreateAccount = true },
                result => Debug.Log("���O�C�������I"),
                error => Debug.Log("���O�C�����s"));
        }
        //���񃍃O�C��
        else
        {
            //Guid�̍\���̐��� �����̃p�^�[��������
            Guid guid = Guid.NewGuid();
            //�J�X�^��ID�Ń��O�C��
            PlayFabClientAPI.LoginWithCustomID(
                new LoginWithCustomIDRequest { CustomId = guid.ToString(), CreateAccount = true },
                result => Debug.Log("���O�C�������I"),
                error => Debug.Log("���O�C�����s"));

            //���O�C��ID��ۑ�
            PlayerPrefs.SetString("loginID", guid.ToString());
            PlayerPrefs.Save();
        }

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
    //�X�R�A���擾(�g���ĂȂ�)
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
    public void RequestLeaderBoard()
    {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest
            {
                StatisticName = STATISTICS_NAME,
                StartPosition = 0,
                MaxResultsCount = 3
            },
            result =>
            {
                for (var i = 0; i < result.Leaderboard.Count; i++)
                {
                    var x = result.Leaderboard[i];
                    if (x.DisplayName == null)
                    {
                        x.DisplayName = unknown;
                    }

                    if (i == 0)
                    {
                        first.text = x.Position + 1 + "��" + " " + x.DisplayName + "\n" + "�X�R�A:" + x.StatValue;
                    }
                    else if (i == 1)
                    {
                        second.text = x.Position + 1 + "��" + " " + x.DisplayName + "\n" + "�X�R�A:" + x.StatValue;
                    }
                    else if (i == 2)
                    {
                        third.text = x.Position + 1 + "��" + " " + x.DisplayName + "\n" + "�X�R�A:" + x.StatValue;
                    }
                }
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
    }

    /// <summary>
    /// �����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)���擾
    /// </summary>
    public void GetLeaderboardAroundPlayer()
    {
        //GetLeaderboardAroundPlayerRequest�̃C���X�^���X�𐶐�
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = STATISTICS_NAME, //�����L���O��(���v���)
            MaxResultsCount = 1                 //�������܂ߑO�㉽���擾���邩
        };

        //�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)���擾
        Debug.Log($"�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾�J�n");
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardAroundPlayerFailure);
    }

    //�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾����
    private void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log($"�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾�ɐ������܂���");

        //result.Leaderboard�Ɋe���ʂ̏��(PlayerLeaderboardEntry)�������Ă���
        yours.text = "";
        foreach (var entry in result.Leaderboard)
        {
            yours.text = entry.Position + 1 + "��" + " " + entry.DisplayName + "\n" + "�X�R�A:" + entry.StatValue;
        }
    }

    //�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾���s
    private void OnGetLeaderboardAroundPlayerFailure(PlayFabError error)
    {
        Debug.LogError($"�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾�Ɏ��s���܂���\n{error.GenerateErrorReport()}");
    }

    //���O�̍X�V
    public void SetPlayerDisplayName(string displayName)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = displayName
            },
            result =>
            {
                Debug.Log("Set display name was succeeded.");

            },
            error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            }
        );
    }
}