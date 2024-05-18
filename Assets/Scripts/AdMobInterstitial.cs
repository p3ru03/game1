using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdMobInterstitial : MonoBehaviour
{
    //��邱��
    //1.�C���^�[�X�e�B�V�����L��ID�̓���
    //2.�C���^�[�X�e�B�V�����N���ݒ�@ShowAdMobInterstitial()���g��


    private InterstitialAd interstitialAd;//InterstitialAd�^�̕ϐ�interstitialAd��錾�@���̒��ɃC���^�[�X�e�B�V�����L���̏�񂪓���

    private string adUnitId;

    private void Start()
    {
        //Android��iOS�ōL��ID���Ⴄ�̂Ńv���b�g�t�H�[���ŏ����𕪂��܂��B
        // �Q�l
        //�yUnity�zAndroid��iOS�ŏ����𕪂�����@
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-9815471696720958/7969495838";//������Android�̃C���^�[�X�e�B�V�����L��ID�����          
#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-3940256099942544/4411468910";//������iOS�̃C���^�[�X�e�B�V�����L��ID�����
#else
        adUnitId = "unexpected_platform";
#endif

        //�C���^�[�X�e�B�V���� �ǂݍ��݊J�n
        Debug.Log("Interstitial ad load start");
        LoadInterstitialAd();
    }

    //�C���^�[�X�e�B�V�����L����\������֐�
    //�{�^���ȂǂɊ��t�����Ďg�p
    public void ShowAdMobInterstitial()
    {
        //�ϐ�interstitial�̒��g�����݂��Ă���A�L���̓ǂݍ��݂��������Ă�����L���\��
        if (interstitialAd != null && interstitialAd.CanShowAd() == true)
        {
            //�C���^�[�X�e�B�V�����L�� �\�������{
            interstitialAd.Show();
        }
        else
        {
            //�C���^�[�X�e�B�V�����L���ǂݍ��ݖ�����
            Debug.Log("Interstitial ad not loaded");
            SceneManager.LoadScene("RankingScene", LoadSceneMode.Single);
        }
    }


    //�C���^�[�X�e�B�V�����L����ǂݍ��ފ֐� �ēǂݍ��݂ɂ��g�p
    private void LoadInterstitialAd()
    {
        //�L���̍ēǂݍ��݂̂��߂̏���
        //interstitialAd�̒��g�������Ă����ꍇ����
        if (interstitialAd != null)
        {
            //�C���^�[�X�e�B�V�����L���͎g���̂ĂȂ̂ň�U�j��
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        //���N�G�X�g�𐶐�
        AdRequest request = new AdRequest();

        //�L���̃L�[���[�h��ǉ�
        //==================================================================
        // �A�v���Ɋ֘A����L�[���[�h�𕶎���Őݒ肷��ƃA�v���ƍL���̊֘A�������܂�܂��B
        // ���ʁA���v���オ��\��������܂��B
        // �C�Ӑݒ�̂��ߕs�v�ł���Ώ����Ă��������Ė��͂���܂���B

        // Application.systemLanguage��OS�̌��ꔻ�ʁ@
        // �Ԃ�l��SystemLanguage.����
        // �[���̌��ꂪ���{��̎�
        if (Application.systemLanguage == SystemLanguage.Japanese)
        {
            request.Keywords.Add("�Q�[��");
            request.Keywords.Add("���o�C���Q�[��");
        }

        //�[���̌��ꂪ���{��ȊO�̎�
        else
        {
            request.Keywords.Add("game");
            request.Keywords.Add("mobile games");
        }
        //==================================================================

        //�L�������[�h  ���̌�A�֐�OnInterstitialAdLoaded���Ăяo��
        InterstitialAd.Load(adUnitId, request, OnInterstitialAdLoaded);
    }


    // �L���̃��[�h�����{������ɌĂяo�����֐�
    private void OnInterstitialAdLoaded(InterstitialAd ad, LoadAdError error)
    {
        //�ϐ�error�ɏ�񂪓����Ă��Ȃ��A�܂��́A�ϐ�ad�ɏ�񂪂͂����Ă��Ȃ���������s
        if (error != null || ad == null)
        {
            //�C���^�[�X�e�B�V���� �ǂݍ��ݎ��s
            Debug.LogError("Failed to load interstitial ad :" + error);//error:�G���[���e 
            return;//���̎��_�ł��̊֐��̎��s�͏I��
        }

        //�C���^�[�X�e�B�V���� �ǂݍ��݊���
        Debug.Log("Interstitial ad loaded");

        //InterstitialAd.Load(~��~)�֐������s���邱�Ƃɂ��AInterstitialAd�^�̕ϐ�ad��InterstitialAd�̃C���X�^���X�𐶐�����B
        //��������InterstitialAd�^�̃C���X�^���X��ϐ�interstitialAd�֊��蓖��
        interstitialAd = ad;

        //�L���� �\���E�\���I���E�\�����s �̓��e��o�^
        RegisterEventHandlers(interstitialAd);
    }


    //�L���� �\���E�\���I���E�\�����s �̓��e
    private void RegisterEventHandlers(InterstitialAd ad)
    {
        //�C���^�[�X�e�B�V�����L�����\�����ꂽ���ɋN��������e
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };

        //�C���^�[�X�e�B�V�����L�����\���I�� �ƂȂ������ɋN��������e
        ad.OnAdFullScreenContentClosed += () =>
        {
            //�C���^�[�X�e�B�V�����L�� �\���I��
            Debug.Log("Interstitial ad full screen content closed.");

            //�C���^�[�X�e�B�V���� �ēǂݍ���
            LoadInterstitialAd();

            SceneManager.LoadScene("RankingScene", LoadSceneMode.Single);
        };

        //�C���^�[�X�e�B�V�����L���̕\�����s �ƂȂ������ɋN��������e
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //�G���[�\��
            Debug.LogError("Interstitial ad failed to open full screen content with error : " + error);

            //�C���^�[�X�e�B�V���� �ēǂݍ���
            LoadInterstitialAd();
        };
    }
}
