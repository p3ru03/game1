using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public float fSpeed;
    bool bLPush = false;
    bool bRPush = false;
    void Update()
    {
        //��ʊO�ɏo�Ȃ��悤��
        if (bLPush && transform.position.x >= -2.3f)
        {
            Move(-fSpeed);
        }
        else if (bRPush && transform.position.x <= 2.3f)
        {
            Move(fSpeed);
        }
    }
    private void Move(float x)
    {
        //Player�̈ړ�
        transform.position += new Vector3(x * Time.deltaTime, 0, 0);
    }
    public void LPointerDown()
    {
        //���������ꂽ�Ƃ�
        bLPush = true;
    }
    public void LPointerUP()
    {
        //���������ꂽ�Ƃ�
        bLPush = false;
    }
    public void RPointerDown()
    {
        //�E�������ꂽ�Ƃ�
        bRPush = true;
    }
    public void RPointerUP()
    {
        //�E�������ꂽ�Ƃ�
        bRPush = false;
    }
}