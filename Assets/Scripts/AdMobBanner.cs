using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdMobBanner : MonoBehaviour
{
    //��邱��
    //1.�o�i�[�L��ID�����
    //2.�o�i�[�̕\���ʒu�@(����\���ʒu�͉��ɂȂ��Ă��܂��B)
    //3.�o�i�[�\���̃^�C�~���O (���� �N������ɂȂ��Ă��܂��B)

    private BannerView bannerView;//BannerView�^�̕ϐ�bannerView��錾�@���̒��Ƀo�i�[�L���̏�񂪓���


    //�V�[���ǂݍ��ݎ�����o�i�[��\������
    //�ŏ�����o�i�[��\���������Ȃ��ꍇ�͂��̊֐��������Ă��������B
    private void Start()
    {
        RequestBanner();//�A�_�v�e�B�u�o�i�[��\������֐� �Ăяo��
    }

    //�{�^�����Ɋ���t���Ďg�p
    //�o�i�[��\������֐�
    public void BannerStart()
    {
        RequestBanner();//�A�_�v�e�B�u�o�i�[��\������֐� �Ăяo��       
    }

    //�{�^�����Ɋ���t���Ďg�p
    //�o�i�[���폜����֐�
    public void BannerDestroy()
    {
        bannerView.Destroy();//�o�i�[�폜
    }

    //�A�_�v�e�B�u�o�i�[��\������֐�
    private void RequestBanner()
    {
        //Android��iOS�ōL��ID���Ⴄ�̂Ńv���b�g�t�H�[���ŏ����𕪂��܂��B
        // �Q�l
        //�yUnity�zAndroid��iOS�ŏ����𕪂�����@
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9815471696720958/7349800305";//������Android�̃o�i�[ID�����

#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";//������iOS�̃o�i�[ID�����

#else
        string adUnitId = "unexpected_platform";
#endif

        // �V�����L����\������O�Ƀo�i�[���폜
        if (bannerView != null)//�����ϐ�bannerView�̒��Ƀo�i�[�̏�񂪓����Ă�����
        {
            bannerView.Destroy();//�o�i�[�폜
        }

        //���݂̉�ʂ̌����������擾���o�i�[�T�C�Y������
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);


        //�o�i�[�𐶐� new BannerView(�o�i�[ID,�o�i�[�T�C�Y,�o�i�[�\���ʒu)
        bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);//�o�i�[�\���ʒu��
                                                                               //��ʏ�ɕ\������ꍇ�FAdPosition.Top
                                                                               //��ʉ��ɕ\������ꍇ�FAdPosition.Bottom


        //BannerView�^�̕ϐ� bannerView�̊e���� �Ɋ֐���o�^
        bannerView.OnBannerAdLoaded += OnBannerAdLoaded;//bannerView�̏�Ԃ� �o�i�[�\������ �ƂȂ������ɋN������֐�(�֐���OnBannerAdLoaded)��o�^
        bannerView.OnBannerAdLoadFailed += OnBannerAdLoadFailed;//bannerView�̏�Ԃ� �o�i�[�ǂݍ��ݎ��s �ƂȂ������ɋN������֐�(�֐���OnBannerAdLoadFailed)��o�^


        //���N�G�X�g�𐶐�
        AdRequest adRequest = new AdRequest();

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
            adRequest.Keywords.Add("�Q�[��");
            adRequest.Keywords.Add("���o�C���Q�[��");
        }

        //�[���̌��ꂪ���{��ȊO�̎�
        else
        {
            adRequest.Keywords.Add("game");
            adRequest.Keywords.Add("mobile games");
        }
        //==================================================================


        //�L���\��
        bannerView.LoadAd(adRequest);
    }


    #region Banner callback handlers

    //�o�i�[�\������ �ƂȂ������ɋN������֐�
    private void OnBannerAdLoaded()
    {
        Debug.Log("�o�i�[�\������");
    }

    //�o�i�[�ǂݍ��ݎ��s �ƂȂ������ɋN������֐�
    private void OnBannerAdLoadFailed(LoadAdError error)
    {
        Debug.Log("�o�i�[�ǂݍ��ݎ��s" + error);//error:�G���[���e 
    }

    #endregion
}
