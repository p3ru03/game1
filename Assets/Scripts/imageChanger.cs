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
    List<string> situation_sport = new List<string>(); // CSV�̒��g�����郊�X�g;

    int number = 0;//�\������摜�����߂鐔

    bool Loaded = false;
    public bool downloaded = false;


    // Start is called before the first frame update
    void Start()
    {

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
        number = UnityEngine.Random.Range(0, situation_sport.Count);

        //number = 0;
        //Downloader();
    }

    // Update is called once per frame
    void Update()
    {
        if (Loaded &! downloaded)
        {
            //Downloader();
            downloaded = true;
        }

    }

    public void Downloader()
    {

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        

        for(int i = 0; i < situation_sport.Count; i++)
        {
            StorageReference reference = storage.GetReference("images_v1/" + situation_sport[i] + ".jpg");
            string filePath = Application.dataPath + "/Resources/" + situation_sport[i] + ".jpg";

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
        Texture2D texture = Resources.Load(situation_sport[number]) as Texture2D;

        // texture2���g��Sprite������āA���f������
        image.sprite = Sprite.Create(texture,
                                     new Rect(0, 0, texture.width, texture.height),
                                     Vector2.zero);

    }

    public void Loadfile()
    {
        situation_sport.Clear();
        csvFile = Resources.Load("EN_merged_situation_sport") as TextAsset; // Resouces����CSV�ǂݍ���
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            situation_sport.Add(line); //���X�g�ɒǉ�
        }

        Debug.Log("loaded");
        Loaded = true;
        //�w�肵�Ēl�����R�Ɏ��o����
        //Debug.Log(situation_sport[0]);
    }
}
