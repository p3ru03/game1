using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    // 生成オブジェクト
    [SerializeField, Header("敵")] GameObject virus;
    [SerializeField, Header("ターゲット１")] GameObject target1;

    [SerializeField, Header("敵の割合")] int vnumber;

    int random;

    int frame = 0;
    [SerializeField, Header("生成間隔")] int generateFrame;

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

        //これでポーズのときに生成を止める
        if (!isStart) return;

       

        //生成間隔を操作
        if (frame > generateFrame)
        {
            frame = 0;

            // 乱数で生成オブジェクトの種類と位置を決める
            random = Random.Range(1, 100);

            if (random <= vnumber)
            {
                Instantiate(virus, new Vector3(Random.Range(-2.3f, 2.3f), transform.position.y, 0), Quaternion.identity);
                //n秒おきに速度を上げる
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