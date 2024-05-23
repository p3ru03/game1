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

    TextAsset csvFile; // CSVファイル
    List<string> EN_list = new List<string>(); // CSVの中身を入れるリスト;
    List<string> JN_list = new List<string>(); // 日本語用;


    int number = 0;//表示する画像を決める数

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
                    // 使用する準備が整った時に通る
                }
                else
                {
                    Debug.LogError(String.Format("すべてのFirebase依存関係を解決できませんでした: {0}", dependencyStatus));
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

        // リソースから、texture2を取得
        Texture2D texture = Resources.Load(EN_list[number]) as Texture2D;

        while (texture == null)//画像表示されないエラー対策
        {
            number = UnityEngine.Random.Range(0, EN_list.Count);
            texture = Resources.Load(EN_list[number]) as Texture2D;
        }


        // texture2を使いSpriteを作って、反映させる
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
        csvFile = Resources.Load("EN_merged_situation_sport") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            EN_list.Add(line); //リストに追加
        }

        csvFile = Resources.Load("merged_situation_sport") as TextAsset; // Resouces下のCSV読み込み
        reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            JN_list.Add(line); //リストに追加
        }


        Loaded = true;

        //number = UnityEngine.Random.Range(0, situation_sport.Count);

    }
}
