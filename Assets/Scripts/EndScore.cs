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

    public imageChanger imageChanger;
    bool firstDisplay = true;

    public GameObject image;
    
    float imageSize = 100f;
    public SizeChanger sizeChanger;

    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(drumrollSE);

        //�ۑ����Ă����X�R�A���Ăяo��
        getScore = (float)PlayerPrefs.GetInt("SCORE", 0);

        StartCoroutine(ScoreAnimation(0f, getScore, 2f));


    }

    private void Update()
    {
        if (canClick && imageChanger.downloaded && firstDisplay)
        {
            imageChanger.Display();
            firstDisplay = false;

        }
        if(!firstDisplay&!sizeChanger.isZero)
        {
            sizeChanger.Sizechanger(imageSize, imageSize);//�摜�����ւ���ɑ傫���\��

        }
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

        //�n�C�X�R�A��\��
        scoreManager.SetHighScore(getScore);

        //�{�^����������悤��
        canClick = true;
    }

}
