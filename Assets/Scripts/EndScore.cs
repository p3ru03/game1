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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(drumrollSE);

        //�ۑ����Ă����X�R�A���Ăяo��
        getScore = (float)PlayerPrefs.GetInt("SCORE", 0);
        
        StartCoroutine(ScoreAnimation(0f, getScore, 2f));
    }

    // �X�R�A���A�j���[�V����������
    private IEnumerator ScoreAnimation(float startScore, float endScore, float duration)
    {
        // �J�n����
        float startTime = Time.time;

        // �I������
        float endTime = startTime + duration;

        do
        {
            // ���݂̎��Ԃ̊���
            float timeRate = (Time.time - startTime) / duration;

            // ���l���X�V
            float updateValue = (float)((endScore - startScore) * timeRate + startScore);

            // �e�L�X�g�̍X�V
            // �i"f0" �� "0" �́A�����_�ȉ��̌����w��j
            scoreText.text = "Score:" + updateValue.ToString("f0"); 

            // 1�t���[���҂�
            yield return null;

        } while (Time.time < endTime);

        //�h�������[�����X�g�b�v
        audioSource.Stop();
        audioSource.PlayOneShot(drumrollendSE);
        // �ŏI�I�Ȓ��n�̃X�R�A
        scoreText.text = "Score:" + endScore.ToString();

        //�X�R�A�𑗐M
        pfContoroller.SubmitScore();
        //�n�C�X�R�A��\��
        pfContoroller.GetPlayerStatistics();

    }

    /*void renewalHighScore()
    {
        if (highScore < getScore)
        {
            highScore = getScore;

            PlayerPrefs.SetFloat("HIGHSCORE", highScore);
            PlayerPrefs.Save();//�f�B�X�N�ւ̏�������
        }
    }*/
}
