using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Text imagetext;
    public string imageName = "�e�X�g";

    public EndScore score;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (score.canClick)//�h�������[����ɕ\��
        {
            imagetext.text = imageName;
            if (imageName.Length > 12)//�������ŃT�C�Y��ύX
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
