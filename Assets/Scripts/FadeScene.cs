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

        //�|�[�Y��ʂ���^�C�g���ɖ߂����Ƃ��ɃA�j���[�V�����𓮂���
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�����̓A�j���[�V������������������s����悤�ɂ��Ă���
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
