using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    //ここを4にしないと3からカウントダウンが始まらない
    float countdown = 4f;
    int count;
    public Text CountText;

    public RandomGenerator randomgenerator;

    private AudioSource audioSource;
    public AudioClip countdownSE;

    public GameObject gameBGM;
    AudioSource _audiosource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(countdownSE);

        _audiosource = gameBGM.GetComponent<AudioSource>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown >= 0)
        {
            countdown -= Time.deltaTime;
            count = (int)countdown;
            CountText.text = count.ToString();
        }
        //カウントダウン終了後
        else
        {
            //カウントダウン用Canvasを見えなくする
            gameObject.SetActive(false);
            //生成を始める
            randomgenerator.isStart = true;

            //コンポーネントをオンにしてgameBGMを流し始める
            _audiosource.enabled = true;
        }
    }
}
