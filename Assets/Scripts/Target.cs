using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    [SerializeField, Header("�������x")] public float velocity0;
    // ��ʉ��[��y���W
    public float bottomBoundary = -5f; 
    private void Start()
    {
        
    }
    void Update()
    {
        //���ɗ����Ă���
        transform.position -= new Vector3(0, velocity0 * Time.deltaTime, 0);
        if (transform.position.y < bottomBoundary)
        {
            // ��ʉ��[�������ɍs������G�I�u�W�F�N�g��j�󂷂�
            Destroy(gameObject);
        }
    }  
}