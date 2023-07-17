using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabController : MonoBehaviour
{
    //PlayFab上で決めたやつ
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
        //カスタムIDでログイン
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest { CustomId = "TestID", CreateAccount = true },
            result => Debug.Log("ログイン成功！"),
            error => Debug.Log("ログイン失敗"));
    }

    //スコアを送信
    public void SubmitScore()
    {
        //ほとんど決まり文句
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
                Debug.Log("スコア送信");
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
    }
    //スコアを取得
    public void GetPlayerStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            result =>
            {
                // 統計情報の取得に成功した場合の処理
                foreach (var stat in result.Statistics)
                {
                    if (stat.StatisticName == STATISTICS_NAME)
                    {
                        highScore = stat.Value;
                        // ハイスコアを表示する
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


    //ランキングを取得
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
                    x => Debug.Log(string.Format("{0}位:{1} スコア{2}", x.Position + 1, x.DisplayName, x.StatValue))
                    );
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
    }

    //名前の更新
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