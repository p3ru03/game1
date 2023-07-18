using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    public InputField inputField;
    public Text username;
    string savedname = "unknown";

    public PlayFabController pfContoroller;

    // Start is called before the first frame update
    void Start()
    {
        savedname = PlayerPrefs.GetString("username");
        username.text = "User Name:" + savedname;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rename()
    {
        //–¼‘O‚ð•Û‘¶
        PlayerPrefs.SetString("username", inputField.text);
        PlayerPrefs.Save();

        username.text = "User Name:" + inputField.text;
        pfContoroller.SetPlayerDisplayName(inputField.text);
    }
}
