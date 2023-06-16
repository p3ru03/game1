using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip targetSE;
    public AudioClip whistleSE;

    bool playerdead = false;

    public GameObject RandomGenerator;

    public ScoreManager sManager;

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
        if (collision.gameObject.CompareTag("Target"))
        {
            //衝突相手を破壊
            Destroy(collision.gameObject);
            //スコアを足す
            sManager.SetScore();
            //SEを一回鳴らす
            audioSource.PlayOneShot(targetSE); 
        }

        if (collision.gameObject.CompareTag("Virus") && !playerdead)
        {
            Destroy(RandomGenerator);

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
