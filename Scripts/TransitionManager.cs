using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject BlackPanel;

    [SerializeField] private GameObject BlackBar1, BlackBar2, BlackBar3;

    [SerializeField] private Image AllFade;

    [SerializeField] private float FadeTime, RemainTime, FadeTime2;

    void Start()
    {
        //FadeStart();
    }

    public void AllFadeStart()
    {
        DOTween.ToAlpha(
        () => AllFade.color,
        color => AllFade.color = color,
        1f, 0.5f) ;
    }

    public void FadeStart()
    {
        BlackPanel.transform.DOLocalMove(new Vector3(0, 1280, 0), 0f);
        BlackPanel.transform.DOLocalMoveY(0, FadeTime);
        Invoke("FadeEnd", RemainTime);
    }

    void FadeEnd()
    {
        BlackPanel.transform.DOLocalMoveY(-1280, FadeTime);
    }

    public void BarChangeStart()
    {
        StartCoroutine("BarMoveToGallery");
    }

    IEnumerator BarMoveToGallery()
    {
        BlackBar1.transform.DOLocalMoveX(1000, 0);
        BlackBar2.transform.DOLocalMoveX(1000, 0);
        BlackBar3.transform.DOLocalMoveX(1000, 0);

        BlackBar1.transform.DOLocalMoveX(0, FadeTime2);
        yield return new WaitForSeconds(0.1f);
        BlackBar2.transform.DOLocalMoveX(0, FadeTime2);
        yield return new WaitForSeconds(0.1f);
        BlackBar3.transform.DOLocalMoveX(0, FadeTime2);
        yield return new WaitForSeconds(0.4f);

        BlackBar1.transform.DOLocalMoveX(-1000, FadeTime2);
        yield return new WaitForSeconds(0.1f);
        BlackBar2.transform.DOLocalMoveX(-1000, FadeTime2);
        yield return new WaitForSeconds(0.1f);
        BlackBar3.transform.DOLocalMoveX(-1000, FadeTime2);
        yield return null;
    }

    public void BarChangeBack()
    {
        StartCoroutine("BarMoveToTitle");
    }

    IEnumerator BarMoveToTitle()
    {
        BlackBar1.transform.DOLocalMoveX(-1000, 0);
        BlackBar2.transform.DOLocalMoveX(-1000, 0);
        BlackBar3.transform.DOLocalMoveX(-1000, 0);

        BlackBar1.transform.DOLocalMoveX(0, FadeTime2);
        yield return new WaitForSeconds(0.1f);
        BlackBar2.transform.DOLocalMoveX(0, FadeTime2);
        yield return new WaitForSeconds(0.1f);
        BlackBar3.transform.DOLocalMoveX(0, FadeTime2);
        yield return new WaitForSeconds(0.4f);

        BlackBar1.transform.DOLocalMoveX(1000, FadeTime2);
        yield return new WaitForSeconds(0.1f);
        BlackBar2.transform.DOLocalMoveX(1000, FadeTime2);
        yield return new WaitForSeconds(0.1f);
        BlackBar3.transform.DOLocalMoveX(1000, FadeTime2);
        yield return null;
    }

}
