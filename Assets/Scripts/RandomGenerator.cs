using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    // �����I�u�W�F�N�g
    [SerializeField, Header("�G")] GameObject virus;
    [SerializeField, Header("�^�[�Q�b�g�P")] GameObject target1;

    [SerializeField, Header("�G�̊���")] int vnumber;

    int random;

    int frame = 0;
    [SerializeField, Header("�����Ԋu")] int generateFrame;

    public bool isStart = false;

    float time;
    public Virus vScript;

    void Start()
    {
        vScript.velocity = vScript.velocity0;
    }

    void Update()
    {
        ++frame;

        time += Time.deltaTime;

        //����Ń|�[�Y�̂Ƃ��ɐ������~�߂�
        if (!isStart) return;

       

        //�����Ԋu�𑀍�
        if (frame > generateFrame)
        {
            frame = 0;

            // �����Ő����I�u�W�F�N�g�̎�ނƈʒu�����߂�
            random = Random.Range(1, 100);

            if (random <= vnumber)
            {
                Instantiate(virus, new Vector3(Random.Range(-2.3f, 2.3f), transform.position.y, 0), Quaternion.identity);
                //n�b�����ɑ��x���グ��
                if (Mathf.Floor(time) % vScript.n == 0)
                {
                    vScript.velocity += vScript.accel;
                }

            }
            else
            {
                Instantiate(target1, new Vector3(Random.Range(-2.3f, 2.3f), transform.position.y, 0), Quaternion.identity);
            }

        }

    }
}