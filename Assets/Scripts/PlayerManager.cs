using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PlayerManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip targetSE;
    public AudioClip whistleSE;

    bool playerdead = false;

    public GameObject RandomGenerator;

    public ScoreManager sManager;

    bool isZero = true;

    // Start is called before the first frame update

    void Start()
    {
        //音を鳴らすためにコンポーネントに追加
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ぶつかったときに動く
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target_u")|| collision.gameObject.CompareTag("Target_b")|| collision.gameObject.CompareTag("Target_k"))
        {
            //衝突相手を破壊
            Destroy(collision.gameObject);
            //スコアを足す
            sManager.SetScore();

            if (collision.gameObject.CompareTag("Target_u"))
            {
                sManager.SetPoint_u();
            }
            else if (collision.gameObject.CompareTag("Target_b"))
            {
                sManager.SetPoint_b();
            }
            else
            {
                sManager.SetPoint_k();
            }

            isZero = false;
            //SEを一回鳴らす
            audioSource.PlayOneShot(targetSE);
        }

        if (collision.gameObject.CompareTag("Virus") && !playerdead)
        {
            Destroy(RandomGenerator);
            //スコア0の場合ここで更新
            if (isZero)
            {
                PlayerPrefs.SetInt("SCORE", 0);
                PlayerPrefs.Save();
            }

            //シーン移動する前に２回以上呼ばれないようにする
            playerdead = true;

            //コルーチンを呼ぶ
            StartCoroutine(GoResult());
            audioSource.PlayOneShot(whistleSE);
        }
    }

    //コルーチン
    IEnumerator GoResult()
    {
        //3秒待機
        yield return new WaitForSeconds(3f);
        //Endシーンへ
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }
}
