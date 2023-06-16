using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    [SerializeField, Header("初期速度")] public float velocity0;
    // 画面下端のy座標
    public float bottomBoundary = -5f; 
    private void Start()
    {
        
    }
    void Update()
    {
        //下に落ちていく
        transform.position -= new Vector3(0, velocity0 * Time.deltaTime, 0);
        if (transform.position.y < bottomBoundary)
        {
            // 画面下端よりも下に行ったら敵オブジェクトを破壊する
            Destroy(gameObject);
        }
    }  
}