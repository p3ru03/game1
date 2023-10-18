using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip buttonSE;

    public FadeScene fScene;

    public RandomGenerator randomGenerator;

    public Canvas pauseCanvas;

    public Canvas RenameCanvas;

    public NameManager nameManager;

    public InputField inputField;

    public EndScore endscore;

    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }


    public void PressStart()
    {
        audioSource.PlayOneShot(buttonSE);

        fScene.LoadScene("SampleScene");
    }

    public void PressRename()
    {
        audioSource.PlayOneShot(buttonSE);

        //名前入力画面を前面に出す
        RenameCanvas.sortingOrder = 100;
    }
    public void PressOK()
    {
        //文字数制限
        if (inputField.text.Length >= 3 && inputField.text.Length <= 25)
        {
            audioSource.PlayOneShot(buttonSE);

            nameManager.Rename();

            //名前入力画面を後面に戻す
            RenameCanvas.sortingOrder = -11;
        }

    }

    public void PressRetry()
    {
        //endシーンならドラムロールが終わってからシーン遷移できるように
        if (endscore == null || endscore.canClick)
        {
            audioSource.PlayOneShot(buttonSE);

            fScene.LoadScene("SampleScene");
        }

    }

    public void PressTitle()
    {
        if (endscore == null || endscore.canClick)
        {
            audioSource.PlayOneShot(buttonSE);

            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
    }

    public void PressRanking()
    {
        if (endscore == null || endscore.canClick)
        {
            //音が鳴ってからシーン遷移させるため
            int count = 1000;
            audioSource.PlayOneShot(buttonSE);
            for (int i = 0; i < count; i++)
            {
                Debug.Log("wait");
            }
            SceneManager.LoadScene("RankingScene", LoadSceneMode.Single);
        }

    }

    public void PressRestart()
    {
        audioSource.PlayOneShot(buttonSE);

        //ポーズ画面の描画順を戻す
        pauseCanvas.sortingOrder = -20;
        //生成開始
        randomGenerator.isStart = true;
        //落下開始
        Time.timeScale = 1;
    }
    public void PressPause()
    {
        audioSource.PlayOneShot(buttonSE);

        //生成を止める
        randomGenerator.isStart = false;
        //落ちてくるのを止める
        Time.timeScale = 0;
        //ポーズ画面を前面に出す
        pauseCanvas.sortingOrder = 10;
    }
}
