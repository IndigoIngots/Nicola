using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private SEManager seManager;
    [SerializeField] private GameObject MovieObject;
    [SerializeField] private GameObject TitleObject;
    [SerializeField] private GameObject GalleryObject;
    [SerializeField] private TransitionManager transitionManager;


    [SerializeField] private float ChangeTime;

    [SerializeField] private AudioSource BGMOPIntro;
    [SerializeField] private AudioSource BGMTitleIntro;
    [SerializeField] private AudioSource BGMRoop;

    [SerializeField] private GameObject CreditPanel, CreditBlack;

    bool isStart = false, isTitle = false;

    public void OPBGMStart()
    {
        BGMOPIntro.Play();
        Invoke("StartLoopBGM", 187.3333f);
    }

    public void ChangeMtT()
    {
        transitionManager.FadeStart();
        Invoke("MovieToTitle", ChangeTime);
    }

    public void ChangeSkip()
    {
        transitionManager.FadeStart();
        Invoke("StartSkipBGM", ChangeTime * 0.2f);
        Invoke("MovieToTitle", ChangeTime);
    }

    void StartSkipBGM()
    {
        BGMTitleIntro.Play();
        Invoke("StartLoopBGM", 208f);
    }

    void MovieToTitle()
    {
        isTitle = true;
        MovieObject.SetActive(false);
        TitleObject.SetActive(true);
    }

    void StartLoopBGM()
    {
        //BGMRoop.Play();
    }

    public void PushGoToGallery()
    {
        seManager.page();
        transitionManager.BarChangeStart();
        Invoke("TitleToGallery", 0.5f);
    }

    void TitleToGallery()
    {
        TitleObject.SetActive(false);
        GalleryObject.SetActive(true);
    }

    public void PushGoToTitle()
    {
        seManager.page();
        transitionManager.BarChangeBack();
        Invoke("GalleryToTitle", 0.5f);
    }

    void GalleryToTitle()
    {
        TitleObject.SetActive(true);
        GalleryObject.SetActive(false);
    }

    bool isCredit = false;

    public void PushCredit()
    {
        if (isCredit == false)
        {
            isCredit = true;
            CreditPanel.transform.DOLocalMove(new Vector3(10, 625, 0), 1.5f).SetEase(Ease.OutBack);
            CreditBlack.SetActive(true);
        }
    }

    public void PushCreditBack()
    {
        if (isCredit == true)
        {
            isCredit = false;
            CreditPanel.transform.DOLocalMove(new Vector3(10, 1190, 0), 1.5f).SetEase(Ease.OutBack);
            CreditBlack.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isStart == false && isTitle == true && isCredit == false)
        {
            isStart = true;
            BGMOPIntro.DOFade(0, 0.5f);
            BGMTitleIntro.DOFade(0, 0.5f);
            BGMRoop.DOFade(0, 0.5f);
            transitionManager.AllFadeStart();
            Invoke("StartLoad", 1f);
        }
    }

    void StartLoad()
    {
        SceneManager.LoadScene("GameScene");
    }
}
