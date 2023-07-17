using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayFabController : MonoBehaviour
{
    //PlayFab上で決めたやつ
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
        //２回目以降のログイン
        if (PlayerPrefs.HasKey("loginID"))
        {
            //カスタムIDでログイン
            PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest { CustomId = PlayerPrefs.GetString("loginID"), CreateAccount = true },
                result => Debug.Log("ログイン成功！"),
                error => Debug.Log("ログイン失敗"));
        }
        //初回ログイン
        else
        {
            //Guidの構造体生成 無数のパターンを持つ
            Guid guid = Guid.NewGuid();
            //カスタムIDでログイン
            PlayFabClientAPI.LoginWithCustomID(
                new LoginWithCustomIDRequest { CustomId = guid.ToString(), CreateAccount = true },
                result => Debug.Log("ログイン成功！"),
                error => Debug.Log("ログイン失敗"));

            //ログインIDを保存
            PlayerPrefs.SetString("loginID", guid.ToString());
            PlayerPrefs.Save();
        }

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
    //スコアを取得(使ってない)
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
                        first.text = x.Position + 1 + "位" + " " + x.DisplayName + "\n" + "スコア:" + x.StatValue;
                    }
                    else if (i == 1)
                    {
                        second.text = x.Position + 1 + "位" + " " + x.DisplayName + "\n" + "スコア:" + x.StatValue;
                    }
                    else if (i == 2)
                    {
                        third.text = x.Position + 1 + "位" + " " + x.DisplayName + "\n" + "スコア:" + x.StatValue;
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
    /// 自分の順位周辺のランキング(リーダーボード)を取得
    /// </summary>
    public void GetLeaderboardAroundPlayer()
    {
        //GetLeaderboardAroundPlayerRequestのインスタンスを生成
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = STATISTICS_NAME, //ランキング名(統計情報名)
            MaxResultsCount = 1                 //自分を含め前後何件取得するか
        };

        //自分の順位周辺のランキング(リーダーボード)を取得
        Debug.Log($"自分の順位周辺のランキング(リーダーボード)の取得開始");
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardAroundPlayerFailure);
    }

    //自分の順位周辺のランキング(リーダーボード)の取得成功
    private void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log($"自分の順位周辺のランキング(リーダーボード)の取得に成功しました");

        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        yours.text = "";
        foreach (var entry in result.Leaderboard)
        {
            yours.text = entry.Position + 1 + "位" + " " + entry.DisplayName + "\n" + "スコア:" + entry.StatValue;
        }
    }

    //自分の順位周辺のランキング(リーダーボード)の取得失敗
    private void OnGetLeaderboardAroundPlayerFailure(PlayFabError error)
    {
        Debug.LogError($"自分の順位周辺のランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
    }

    //名前の更新
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