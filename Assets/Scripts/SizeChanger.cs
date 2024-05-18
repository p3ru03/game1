using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChanger : MonoBehaviour
{

    RectTransform rectTransform;

    public bool isZero = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Sizechanger(float w, float h)
    {
        rectTransform.sizeDelta = new Vector2(w, h);
    }

    public void Sizezero()
    {
        isZero = true;
        rectTransform.sizeDelta = new Vector2(0f, 0f);
    }
}
