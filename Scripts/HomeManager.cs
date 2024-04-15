using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private AudioSource GameBGM;

    [SerializeField] private Image BlackFade;
    [SerializeField] private GameObject SkipButton;

    [SerializeField] private TitleManager titleManager;
    [SerializeField] private GameObject BlackPanel;
    [SerializeField] private GameObject BlackUpBar, BlackDownBar;

    [System.Serializable]
    public class OpeningText
    {
        [TextArea] public string StoryText;
        public float WaitTime;
    }
    [SerializeField] List<OpeningText> OpeningTexts = new List<OpeningText>();

    [SerializeField] private Image Chara;
    [SerializeField] private Sprite[] Nicorairrusts;

    [SerializeField] private GameObject ClickText;
    [SerializeField] private Text StoryText;

    [SerializeField] private SEManager seManager;
    [SerializeField] private float BuzzerTime;

    bool isFirst = false;

    // Start is called before the first frame update
    void Awake()
    {
        DOTween.ToAlpha(
        () => BlackFade.color,
        color => BlackFade.color = color,
        0f, 1f);
        Invoke("falseFade", 1.0f);

        int Data = PlayerPrefs.GetInt("SCORE", 0);
        if (Data == 0)
        {
            SkipButton.SetActive(false);
        }
        else
        {
            SkipButton.SetActive(true);
        }
    }

    void falseFade()
    {
        BlackFade.gameObject.SetActive(false);
    }

    public void PushStartMovie()
    {
        if (isFirst == false)
        {
            isFirst = true;
            StartMovie();
        }
    }

    void StartMovie()
    {
        SkipButton.SetActive(false);
        seManager.buzzer();
        ClickText.transform.DOLocalMoveY(300, BuzzerTime).SetEase(Ease.Linear);
        StartCoroutine("Movie");
    }

    float Tempo = 1 / 1.5f;
    Vector3 NicolaPos;

    IEnumerator Movie()
    {
        yield return new WaitForSeconds(4.5f);
        seManager.Stop();
        titleManager.OPBGMStart();

        while (true)
        {
            if (GameBGM.time > 0)
            {
                StartCoroutine("MovieContinue");
                break;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator MovieContinue()
    { 
        Chara.gameObject.SetActive(true);
        InvokeRepeating("NicolaMove", 0f, Tempo);

        for (int i = 0; i < OpeningTexts.Count; i++)
        {
            StoryText.text = "";
            StoryText.DOText(OpeningTexts[i].StoryText, 1f);

            if (i == 2) Chara.sprite = Nicorairrusts[1];
            if (i == 7) Chara.sprite = Nicorairrusts[2];
            yield return new WaitForSeconds(OpeningTexts[i].WaitTime);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        //BlackUpBar.transform.DOMoveY(210, 2f);
        //BlackDownBar.transform.DOMoveY(210, 2f);

        titleManager.ChangeMtT();
        CancelInvoke("NicolaMove");
    }

    int MoveNum = 0;

    void NicolaMove()
    {
        if (MoveNum == 0)
        {
            Chara.gameObject.transform.Rotate(new Vector3(0, 0, 5));
            MoveNum = 1;
        }
        else if (MoveNum == 1)
        {
            Chara.gameObject.transform.Rotate(new Vector3(0, 0, -5));
            MoveNum = 0;
        }
    }

    public void PushSkipMovie()
    {
        SkipButton.SetActive(false);
        StartCoroutine("Skip");
    }

    IEnumerator Skip()
    {
        titleManager.ChangeSkip();
        yield return new WaitForSeconds(1.0f);

        isFirst = true;
        yield return null;
    }
}
