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
        //����炷���߂ɃR���|�[�l���g�ɒǉ�
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�Ԃ������Ƃ��ɓ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target_u")|| collision.gameObject.CompareTag("Target_b")|| collision.gameObject.CompareTag("Target_k"))
        {
            //�Փˑ����j��
            Destroy(collision.gameObject);
            //�X�R�A�𑫂�
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
            //SE�����炷
            audioSource.PlayOneShot(targetSE);
        }

        if (collision.gameObject.CompareTag("Virus") && !playerdead)
        {
            Destroy(RandomGenerator);
            //�X�R�A0�̏ꍇ�����ōX�V
            if (isZero)
            {
                PlayerPrefs.SetInt("SCORE", 0);
                PlayerPrefs.Save();
            }

            //�V�[���ړ�����O�ɂQ��ȏ�Ă΂�Ȃ��悤�ɂ���
            playerdead = true;

            //�R���[�`�����Ă�
            StartCoroutine(GoResult());
            audioSource.PlayOneShot(whistleSE);
        }
    }

    //�R���[�`��
    IEnumerator GoResult()
    {
        //3�b�ҋ@
        yield return new WaitForSeconds(3f);
        //End�V�[����
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }
}
