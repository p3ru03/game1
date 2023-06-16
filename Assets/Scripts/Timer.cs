using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    //������4�ɂ��Ȃ���3����J�E���g�_�E�����n�܂�Ȃ�
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
        //�J�E���g�_�E���I����
        else
        {
            //�J�E���g�_�E���pCanvas�������Ȃ�����
            gameObject.SetActive(false);
            //�������n�߂�
            randomgenerator.isStart = true;

            //�R���|�[�l���g���I���ɂ���gameBGM�𗬂��n�߂�
            _audiosource.enabled = true;
        }
    }
}
