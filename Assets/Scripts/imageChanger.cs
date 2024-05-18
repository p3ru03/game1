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
    List<string> situation_sport = new List<string>(); // CSVの中身を入れるリスト;

    int number = 0;//表示する画像を決める数

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
                // 使用する準備が整った時に通る
            }
            else
            {
                Debug.LogError(String.Format("すべてのFirebase依存関係を解決できませんでした: {0}", dependencyStatus));
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

        // リソースから、texture2を取得
        Texture2D texture = Resources.Load(situation_sport[number]) as Texture2D;

        // texture2を使いSpriteを作って、反映させる
        image.sprite = Sprite.Create(texture,
                                     new Rect(0, 0, texture.width, texture.height),
                                     Vector2.zero);

    }

    public void Loadfile()
    {
        situation_sport.Clear();
        csvFile = Resources.Load("EN_merged_situation_sport") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            situation_sport.Add(line); //リストに追加
        }

        Debug.Log("loaded");
        Loaded = true;
        //指定して値を自由に取り出せる
        //Debug.Log(situation_sport[0]);
    }
}
