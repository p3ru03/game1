using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Virus : MonoBehaviour
{
    // ��ʉ��[��y���W
    public float bottomBoundary = -5f;

    [SerializeField, Header("�������x")] public float velocity0;
    [SerializeField, Header("���݂̑��x")] public float velocity;

    //RandomGenerator�̃X�N���v�g�ő��x��ς���
    [SerializeField, Header("�����x")] public float accel;
    [SerializeField, Header("n�b���Ƃɉ���")] public int n;

    SceneChanger changer;

    private void Start()
    {
       
    }
    void Update()
    {
        //���ɗ����Ă���
        //�������Ȃ��痎���Ȃ��悤��RandomGenerator�ő��x�ύX
        transform.position -= new Vector3(0, velocity * Time.deltaTime, 0);
        if (transform.position.y < bottomBoundary)
        {
            // ��ʉ��[�������ɍs������G�I�u�W�F�N�g��j�󂷂�
            Destroy(gameObject);
        }
    }  
}
