using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    private string _SceneName;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //ポーズ画面からタイトルに戻ったときにアニメーションを動かす
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ここはアニメーションが完了したら実行するようにしておく
    public void LoadSceneEvent()
    {

        SceneManager.LoadScene(_SceneName, LoadSceneMode.Single);

    }

    public void LoadScene(string sceneName)
    {
        animator.enabled = true;
        _SceneName = sceneName;

    }
}
