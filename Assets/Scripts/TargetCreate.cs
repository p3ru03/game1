using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCreate : MonoBehaviour
{
    public GameObject prefabTarget;
    
    private float fInterval = 2f;
    private float fTime = 0;
    void Update()
    {
        fTime += Time.deltaTime;

        if (fTime >= fInterval)//ˆê’è‚ÌüŠú‚Å¶¬
        {
            RandomTargetCreate();
            fTime = 0;
        }
    }

    private void RandomTargetCreate()
    {
        Vector3 TargetPosition = new Vector3(Random.Range(-2.3f, 2.3f), transform.position.y, transform.position.z);
        Instantiate(prefabTarget, TargetPosition, transform.rotation);
    }
}