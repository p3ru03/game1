using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobReward : MonoBehaviour
{
    //��邱��
    //1.�����[�h�L��ID�̓���
    //2.GetReward�֐��ɕ�V���e�����
    //3.�����[�h�N���ݒ�@ShowAdMobReward()���g��

    private RewardedAd rewardedAd;//RewardedAd�^�̕ϐ� rewardedAd��錾 ���̒��Ƀ����[�h�L���̏�񂪓���

    private string adUnitId;

    private void Start()
    {
        //Android��iOS�ōL��ID���Ⴄ�̂Ńv���b�g�t�H�[���ŏ����𕪂��܂��B
        // �Q�l
        //�yUnity�zAndroid��iOS�ŏ����𕪂�����@
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-9815471696720958/9026678201";//������Android�̃����[�h�L��ID�����
#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-3940256099942544/1712485313";//������iOS�̃����[�h�L��ID�����
#else
        adUnitId = "unexpected_platform";
#endif

        //�����[�h �ǂݍ��݊J�n
        Debug.Log("Rewarded ad load start");

        LoadRewardedAd();//�����[�h�L���ǂݍ���
    }

    //�����[�h�L����\������֐�
    //�{�^���Ɋ��t�����Ďg�p
    public void ShowAdMobReward()
    {
        //�ϐ�rewardedAd�̒��g�����݂��Ă���A�L���̓ǂݍ��݂��������Ă�����L���\��
        if (rewardedAd != null && rewardedAd.CanShowAd() == true)
        {
            //�����[�h�L�� �\�������{�@��V�̎󂯎��̊֐�GetReward�������ɐݒ�
            rewardedAd.Show(GetReward);
        }
        else
        {
            //�����[�h�L���ǂݍ��ݖ�����
            Debug.Log("Rewarded ad not loaded");
        }
    }

    //��V�󂯎�菈��
    private void GetReward(Reward reward)
    {
        //��V�󂯎��
        Debug.Log("GetReward");

        //�����ɕ�V�̏���������
    }


    //�����[�h�L����ǂݍ��ފ֐� �ēǂݍ��݂ɂ��g�p
    public void LoadRewardedAd()
    {
        //�L���̍ēǂݍ��݂̂��߂̏���
        //rewardedAd�̒��g�������Ă����ꍇ����
        if (rewardedAd != null)
        {
            //�����[�h�L���͎g���̂ĂȂ̂ň�U�j��
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        //���N�G�X�g�𐶐�
        AdRequest request = new AdRequest();

        //�L���̃L�[���[�h��ǉ�
        //===================================================================
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

        //�L�������[�h  ���̌�A�֐�OnRewardedAdLoaded���Ăяo��
        RewardedAd.Load(adUnitId, request, OnRewardedAdLoaded);
    }


    // �L���̃��[�h�����{������ɌĂяo�����֐�
    private void OnRewardedAdLoaded(RewardedAd ad, LoadAdError error)
    {
        //�ϐ�error�ɏ�񂪓����Ă���@�܂��́A�ϐ�ad�ɏ�񂪂͂����Ă��Ȃ���������s
        if (error != null || ad == null)
        {
            //�����[�h �ǂݍ��ݎ��s
            Debug.LogError("Failed to load reward ad : " + error);//error:�G���[���e 
            return;//���̎��_�ł��̊֐��̎��s�͏I��
        }

        //�����[�h �ǂݍ��݊���
        Debug.Log("Reward ad loaded");

        //RewardedAd.Load(~��~)�֐������s���邱�Ƃɂ��ARewardedAd�^�̕ϐ�ad��RewardedAd�̃C���X�^���X�𐶐�����B
        //��������RewardedAd�^�̃C���X�^���X��ϐ�rewardedAd�֊��蓖��
        rewardedAd = ad;

        //�L���� �\���E�\���I���E�\�����s �̓��e��o�^
        RegisterEventHandlers(rewardedAd);
    }


    //�L���� �\���E�\���I���E�\�����s �̓��e
    private void RegisterEventHandlers(RewardedAd ad)
    {
        //�����[�h�L�����\�����ꂽ���ɋN��������e
        ad.OnAdFullScreenContentOpened += () =>
        {
            //�����[�h�L�� �\��
            Debug.Log("Rewarded ad full screen content opened.");
        };

        //�����[�h�L�����\���I�� �ƂȂ������ɋN��������e
        ad.OnAdFullScreenContentClosed += () =>
        {
            //�����[�h�L�� �\���I��
            Debug.Log("Rewarded ad full screen content closed.");

            //�����[�h �ēǂݍ���
            LoadRewardedAd();
        };

        //�����[�h�L���̕\�����s �ƂȂ������ɋN��������e
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //�G���[�\��
            Debug.LogError("Rewarded ad failed to open full screen content with error : " + error);

            //�����[�h �ēǂݍ���
            LoadRewardedAd();
        };
    }
}
