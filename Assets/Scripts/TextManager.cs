using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Text imagetext;
    public string imageName = "テスト";

    public EndScore score;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (score.canClick)//ドラムロール後に表示
        {
            imagetext.text = imageName;
            if (imageName.Length > 12)//文字数でサイズを変更
            {
                imagetext.fontSize = 60;
            }
            else if (imageName.Length < 8)
            {
                imagetext.fontSize = 100;
            }
            else
            {
                imagetext.fontSize = 80;
            }
        }
        
    }
}
