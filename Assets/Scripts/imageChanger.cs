using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class imageChanger : MonoBehaviour
{
    public Image image;

    TextAsset csvFile; // CSV�t�@�C��
    List<string> EN_list = new List<string>(); // CSV�̒��g�����郊�X�g;
    List<string> JN_list = new List<string>(); // ���{��p;


    int number = 0;//�\������摜�����߂鐔

    bool Loaded = false;
    public bool downloaded = false;

    public TextManager textManager;

    private int point_u;
    private int point_b;
    private int point_k;


    // Start is called before the first frame update
    void Start()
    {
        point_u = PlayerPrefs.GetInt("point_u", 0);
        point_b = PlayerPrefs.GetInt("point_b", 0);
        point_k = PlayerPrefs.GetInt("point_k", 0);

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    // �g�p���鏀�������������ɒʂ�
                }
                else
                {
                    Debug.LogError(String.Format("���ׂĂ�Firebase�ˑ��֌W�������ł��܂���ł���: {0}", dependencyStatus));
                }
            });


        Loadfile();
        number = UnityEngine.Random.Range(0, EN_list.Count);

        //number = 0;
        //Downloader();
    }

    // Update is called once per frame
    void Update()
    {
        if (Loaded & !downloaded)
        {
            //Downloader();
            downloaded = true;
        }

    }

    public void Downloader()
    {

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;


        for (int i = 0; i < EN_list.Count; i++)
        {
            StorageReference reference = storage.GetReference("images_v1/" + EN_list[i] + ".jpg");
            string filePath = Application.dataPath + "/Resources/" + EN_list[i] + ".jpg";

            reference.GetFileAsync(filePath).ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && !task.IsCanceled)
                {
                    //Debug.Log("downloaded");
                }
            });
        }

    }

    public void Display()
    {

        // ���\�[�X����Atexture2���擾
        Texture2D texture = Resources.Load(EN_list[number]) as Texture2D;

        while (texture == null)//�摜�\������Ȃ��G���[�΍�
        {
            number = UnityEngine.Random.Range(0, EN_list.Count);
            texture = Resources.Load(EN_list[number]) as Texture2D;
        }


        // texture2���g��Sprite������āA���f������
        image.sprite = Sprite.Create(texture,
                                     new Rect(0, 0, texture.width, texture.height),
                                     Vector2.zero);

        textManager.imageName = JN_list[number];


        Debug.Log(JN_list[number]);
    }

    public void Loadfile()
    {
        //if (point_u >= point_b)
        //{

        //}
        csvFile = Resources.Load("EN_merged_situation_sport") as TextAsset; // Resouces����CSV�ǂݍ���
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            EN_list.Add(line); //���X�g�ɒǉ�
        }

        csvFile = Resources.Load("merged_situation_sport") as TextAsset; // Resouces����CSV�ǂݍ���
        reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            JN_list.Add(line); //���X�g�ɒǉ�
        }


        Loaded = true;

        //number = UnityEngine.Random.Range(0, situation_sport.Count);

    }
}
