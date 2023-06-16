using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Virus : MonoBehaviour
{
    // 画面下端のy座標
    public float bottomBoundary = -5f;

    [SerializeField, Header("初期速度")] public float velocity0;
    [SerializeField, Header("現在の速度")] public float velocity;

    //RandomGeneratorのスクリプトで速度を変える
    [SerializeField, Header("加速度")] public float accel;
    [SerializeField, Header("n秒ごとに加速")] public int n;

    SceneChanger changer;

    private void Start()
    {
       
    }
    void Update()
    {
        //下に落ちていく
        //加速しながら落ちないようにRandomGeneratorで速度変更
        transform.position -= new Vector3(0, velocity * Time.deltaTime, 0);
        if (transform.position.y < bottomBoundary)
        {
            // 画面下端よりも下に行ったら敵オブジェクトを破壊する
            Destroy(gameObject);
        }
    }  
}
