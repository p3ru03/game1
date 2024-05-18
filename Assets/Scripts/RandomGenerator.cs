using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    // 生成オブジェクト
    [SerializeField, Header("敵")] GameObject virus;
    [SerializeField, Header("運動")] GameObject Undo;
    [SerializeField, Header("勉強")] GameObject Benkyo;
    [SerializeField, Header("環境")] GameObject Kankyo;

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

            if (random <= 25)
            {
                Instantiate(virus, new Vector3(Random.Range(-2.3f, 2.3f), transform.position.y, 0), Quaternion.identity);
                //n秒おきに速度を上げる
                if (Mathf.Floor(time) % vScript.n == 0)
                {
                    vScript.velocity += vScript.accel;
                }

            }
            else if (random <= 50)
            {
                Instantiate(Undo, new Vector3(Random.Range(-2.3f, 2.3f), transform.position.y, 0), Quaternion.identity);
            }
            else if (random <= 75)
            {
                Instantiate(Benkyo, new Vector3(Random.Range(-2.3f, 2.3f), transform.position.y, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(Kankyo, new Vector3(Random.Range(-2.3f, 2.3f), transform.position.y, 0), Quaternion.identity);
            }
        }

    }
}