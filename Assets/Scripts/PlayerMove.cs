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
        //画面外に出ないように
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
        //Playerの移動
        transform.position += new Vector3(x * Time.deltaTime, 0, 0);
    }
    public void LPointerDown()
    {
        //左が押されたとき
        bLPush = true;
    }
    public void LPointerUP()
    {
        //左が離されたとき
        bLPush = false;
    }
    public void RPointerDown()
    {
        //右が押されたとき
        bRPush = true;
    }
    public void RPointerUP()
    {
        //右が離されたとき
        bRPush = false;
    }
}