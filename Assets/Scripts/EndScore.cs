using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    public Text scoreText;
    public float getScore;

    private AudioSource audioSource;
    public AudioClip drumrollSE;
    public AudioClip drumrollendSE;

    public PlayFabController pfContoroller;

    public ScoreManager scoreManager;

    public bool canClick = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(drumrollSE);

        //保存していたスコアを呼び出す
        getScore = (float)PlayerPrefs.GetInt("SCORE", 0);

        StartCoroutine(ScoreAnimation(0f, getScore, 2f));
    }

    // スコアをアニメーションさせる
    private IEnumerator ScoreAnimation(float startScore, float endScore, float duration)
    {
        // 開始時間
        float startTime = Time.time;

        // 終了時間
        float endTime = startTime + duration;

        do
        {
            // 現在の時間の割合
            float timeRate = (Time.time - startTime) / duration;

            // 数値を更新
            float updateValue = (float)((endScore - startScore) * timeRate + startScore);

            // テキストの更新
            // （"f0" の "0" は、小数点以下の桁数指定）
            scoreText.text = "Score:" + updateValue.ToString("f0");

            // 1フレーム待つ
            yield return null;

        } while (Time.time < endTime);

        //ドラムロールをストップ
        audioSource.Stop();
        audioSource.PlayOneShot(drumrollendSE);
        // 最終的な着地のスコア
        scoreText.text = "Score:" + endScore.ToString();

        //ハイスコアを表示
        scoreManager.SetHighScore(getScore);

        //ボタンを押せるように
        canClick = true;
    }

}
