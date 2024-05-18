using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdMobInterstitial : MonoBehaviour
{
    //やること
    //1.インタースティシャル広告IDの入力
    //2.インタースティシャル起動設定　ShowAdMobInterstitial()を使う


    private InterstitialAd interstitialAd;//InterstitialAd型の変数interstitialAdを宣言　この中にインタースティシャル広告の情報が入る

    private string adUnitId;

    private void Start()
    {
        //AndroidとiOSで広告IDが違うのでプラットフォームで処理を分けます。
        // 参考
        //【Unity】AndroidとiOSで処理を分ける方法
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-9815471696720958/7969495838";//ここにAndroidのインタースティシャル広告IDを入力          
#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-3940256099942544/4411468910";//ここにiOSのインタースティシャル広告IDを入力
#else
        adUnitId = "unexpected_platform";
#endif

        //インタースティシャル 読み込み開始
        Debug.Log("Interstitial ad load start");
        LoadInterstitialAd();
    }

    //インタースティシャル広告を表示する関数
    //ボタンなどに割付けして使用
    public void ShowAdMobInterstitial()
    {
        //変数interstitialの中身が存在しており、広告の読み込みが完了していたら広告表示
        if (interstitialAd != null && interstitialAd.CanShowAd() == true)
        {
            //インタースティシャル広告 表示を実施
            interstitialAd.Show();
        }
        else
        {
            //インタースティシャル広告読み込み未完了
            Debug.Log("Interstitial ad not loaded");
            SceneManager.LoadScene("RankingScene", LoadSceneMode.Single);
        }
    }


    //インタースティシャル広告を読み込む関数 再読み込みにも使用
    private void LoadInterstitialAd()
    {
        //広告の再読み込みのための処理
        //interstitialAdの中身が入っていた場合処理
        if (interstitialAd != null)
        {
            //インタースティシャル広告は使い捨てなので一旦破棄
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        //リクエストを生成
        AdRequest request = new AdRequest();

        //広告のキーワードを追加
        //==================================================================
        // アプリに関連するキーワードを文字列で設定するとアプリと広告の関連性が高まります。
        // 結果、収益が上がる可能性があります。
        // 任意設定のため不要であれば消していただいて問題はありません。

        // Application.systemLanguageでOSの言語判別　
        // 返り値はSystemLanguage.言語
        // 端末の言語が日本語の時
        if (Application.systemLanguage == SystemLanguage.Japanese)
        {
            request.Keywords.Add("ゲーム");
            request.Keywords.Add("モバイルゲーム");
        }

        //端末の言語が日本語以外の時
        else
        {
            request.Keywords.Add("game");
            request.Keywords.Add("mobile games");
        }
        //==================================================================

        //広告をロード  その後、関数OnInterstitialAdLoadedを呼び出す
        InterstitialAd.Load(adUnitId, request, OnInterstitialAdLoaded);
    }


    // 広告のロードを実施した後に呼び出される関数
    private void OnInterstitialAdLoaded(InterstitialAd ad, LoadAdError error)
    {
        //変数errorに情報が入っていない、または、変数adに情報がはいっていなかったら実行
        if (error != null || ad == null)
        {
            //インタースティシャル 読み込み失敗
            Debug.LogError("Failed to load interstitial ad :" + error);//error:エラー内容 
            return;//この時点でこの関数の実行は終了
        }

        //インタースティシャル 読み込み完了
        Debug.Log("Interstitial ad loaded");

        //InterstitialAd.Load(~略~)関数を実行することにより、InterstitialAd型の変数adにInterstitialAdのインスタンスを生成する。
        //生成したInterstitialAd型のインスタンスを変数interstitialAdへ割り当て
        interstitialAd = ad;

        //広告の 表示・表示終了・表示失敗 の内容を登録
        RegisterEventHandlers(interstitialAd);
    }


    //広告の 表示・表示終了・表示失敗 の内容
    private void RegisterEventHandlers(InterstitialAd ad)
    {
        //インタースティシャル広告が表示された時に起動する内容
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };

        //インタースティシャル広告が表示終了 となった時に起動する内容
        ad.OnAdFullScreenContentClosed += () =>
        {
            //インタースティシャル広告 表示終了
            Debug.Log("Interstitial ad full screen content closed.");

            //インタースティシャル 再読み込み
            LoadInterstitialAd();

            SceneManager.LoadScene("RankingScene", LoadSceneMode.Single);
        };

        //インタースティシャル広告の表示失敗 となった時に起動する内容
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //エラー表示
            Debug.LogError("Interstitial ad failed to open full screen content with error : " + error);

            //インタースティシャル 再読み込み
            LoadInterstitialAd();
        };
    }
}
