using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    int ClearTimes;
    int Combo;

    [SerializeField] private BPMMoves bpmMoves;

    [SerializeField] private Text ScoreText;

    [SerializeField] private Image FrontBlackPanel;
    [SerializeField] private GameObject stageCurtain;
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject Light0;
    [SerializeField] private GameObject Light1;

    [SerializeField] private MovieManager movieManager;

    //[SerializeField] private Text CountDown;

    bool isTimerEnd = true;
    bool isPlayEnd = true;

    int score;
    bool isClear = true;

    [SerializeField] private float SettingTimeFloat;
    private float timer;
    [SerializeField] Text timerText;

    [SerializeField] private SEManager seManager;

    [SerializeField] private SpriteRenderer image;
    [SerializeField] private CostumeData[] Datas;
    [SerializeField] private Sprite[] Costumes;

    [SerializeField] private Image[] ArrowImages;
    [SerializeField] private Sprite[] ArrowSprites;

    int arrowPlace;
    int Arrow1;
    int Arrow2;
    int Arrow3;
    int Arrow4;
    int Arrow5;

    [SerializeField] private Image UnMask;

    [SerializeField] private AudioSource BGM, BGMLoop;
    [SerializeField] private AudioClip IntroBGM;

    [SerializeField] private Canvas MovieCanvas;

    void TimeEnd()
    {
        isTimerEnd = true;
        timerText.text = "0.00";
    }

    void GameOver()
    {
        bpmMoves.EndGame();
        BGM.Stop();
        BGMLoop.Stop();
        timerText.text = "SHOW'S OVER!!";
        isPlayEnd = true;

        int CountReleaseNum = 0;
        for (int i = 0; i < Datas.Length; i++)
        {
            if (DataHolder.DataNums[i] > 0)
            {
                CountReleaseNum++;
            }
        }
        movieManager.ReleaseNum = CountReleaseNum;

        int highscore = PlayerPrefs.GetInt("SCORE", 0);
        if (highscore < score)
        {        
            PlayerPrefs.SetInt("SCORE", score);
            PlayerPrefs.Save();
        }

        MovieCanvas.gameObject.SetActive(true);
        Invoke("StartMovie", 1.0f);
    }

    void EndGame()
    { 
    
    }

    void StartMovie()
    {
        for (int i = 0; i < Datas.Length; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), DataHolder.DataNums[i]);
        }
        PlayerPrefs.Save();
        movieManager.score = score;
        movieManager.StartEnd();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CountFour");
    }

    IEnumerator CountFour()
    {
        yield return new WaitForSeconds(2.5f);
        BGM.Play();
        while (true)
        {
            if (BGM.time > 0)
            {
                CountFourStart();
                break;
            }
            yield return null;
        }
        yield return null;
    }

    void CountFourStart()
    {
        Invoke("StartLoopBGM", 350.9f);
        DOTween.ToAlpha(
        () => FrontBlackPanel.color,
        color => FrontBlackPanel.color = color,
        0f, 0.6f);

        Invoke("CountThree", 0.4f);
    }

    void StartLoopBGM()
    {
        BGMLoop.Play();
    }

    void CountThree()
    {
        stageCurtain.transform.DOMoveY(8, 0.4f);
        Camera.transform.DOMove(new Vector3(0, 0, -17f), 0.4f);
        Invoke("CountTwo", 0.4f);
    }

    void CountTwo()
    {
        image.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f, RotateMode.FastBeyond360);
        Invoke("CountOne", 0.5f);
    }

    void CountOne()
    {
        image.gameObject.transform.DOMoveY(-1f, 0.25f).SetEase(Ease.OutCubic);
        Invoke("CountOnePointFive", 0.25f);
        Invoke("GameStart", 0.5f);
    }

    void CountOnePointFive()
    {
        image.gameObject.transform.DOMoveY(-1.26f, 0.25f).SetEase(Ease.InCubic);
    }

    void GameStart()
    {
        bpmMoves.StartMusic();
        ClearTimes = 0;
        Camera.transform.DOMove(new Vector3(0, 0, -13f), 0f);
        Camera.transform.DOMove(new Vector3(0, 0, -16.4f), 0.3f);
        Light0.SetActive(false);
        Light1.SetActive(true);
        timer = SettingTimeFloat;
        score = 0;
        ScoreText.text = "Score:0000";
        isTimerEnd = false;
        isPlayEnd = false;
        NewQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClear == false && isTimerEnd == false)
        {       
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("f2");

            if (timer <= 0)
            {
                TimeEnd();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && isClear == false && isPlayEnd == false)
        {
            Judge(0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && isClear == false && isPlayEnd == false)
        {
            Judge(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && isClear == false && isPlayEnd == false)
        {
            Judge(2);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && isClear == false && isPlayEnd == false)
        {
            Judge(3);
        }
    }

    void Judge(int PushNum)
    {
        if (arrowPlace == 0)
        {
            if (PushNum == Arrow1)
            {
                ArrowImages[0].gameObject.SetActive(false);
                Correct();
            }
            else
            {
                Miss();
            }
        }
        else if (arrowPlace == 1)
        {
            if (PushNum == Arrow2)
            {
                ArrowImages[1].gameObject.SetActive(false);
                Correct();
            }
            else
            {
                Miss();
            }
        }
        else if (arrowPlace == 2)
        {
            if (PushNum == Arrow3)
            {
                ArrowImages[2].gameObject.SetActive(false);
                image.sprite = Costumes[1];
                Correct();
            }
            else
            {
                Miss();
            }
        }
        else if (arrowPlace == 3)
        {
            if (PushNum == Arrow4)
            {
                ArrowImages[3].gameObject.SetActive(false);
                Correct();
            }
            else
            {
                Miss();
            }
        }
        else if (arrowPlace == 4)
        {
            if (PushNum == Arrow5)
            {
                ArrowImages[4].gameObject.SetActive(false);
                Correct();
            }
            else
            {
                Miss();
            }
        }
    }

    void Correct()
    {
        if (arrowPlace != 4)
        {
            seManager.success();
            arrowPlace++;
        }
        else
        {
            Clear();
        }
    }

    void Miss()
    {
        Combo = 0;
        seManager.miss();
    }

    void Clear()
    {
        seManager.cymbal();
        int RandomNum = Random.Range(0, Datas.Length);
        image.sprite = Datas[RandomNum].graphic;
        score += 50;
        score += 5 * Combo;
        ScoreText.text = "Score:" + score.ToString("D4");
        isClear = true;

        DataHolder.DataNums[RandomNum] += 1;


        if (isTimerEnd == false)
        {
            Combo++;
            Invoke("FadeBlack", 0.4f);
            Invoke("NewQuestion", 1.0f);
        }
        else
        {
            GameOver();
        }
    }

    void FadeBlack()
    {
        UnMask.transform.DOScale(new Vector3(0, 0, 1), 0.5f).SetEase(Ease.OutQuart);
    }

    void NewQuestion()
    {
        isClear = false;
        arrowPlace = 0;

        image.sprite = Costumes[0];
        Arrow1 = Random.Range(0, 4);
        Arrow2 = Random.Range(0, 4);
        Arrow3 = Random.Range(0, 4);
        Arrow4 = Random.Range(0, 4);
        Arrow5 = Random.Range(0, 4);

        ArrowImages[0].gameObject.SetActive(true);
        ArrowImages[1].gameObject.SetActive(true);
        ArrowImages[2].gameObject.SetActive(true);
        ArrowImages[3].gameObject.SetActive(true);
        ArrowImages[4].gameObject.SetActive(true);

        ArrowImages[0].sprite = ArrowSprites[Arrow1];
        ArrowImages[1].sprite = ArrowSprites[Arrow2];
        ArrowImages[2].sprite = ArrowSprites[Arrow3];
        ArrowImages[3].sprite = ArrowSprites[Arrow4];
        ArrowImages[4].sprite = ArrowSprites[Arrow5];

        UnMask.transform.DOScale(new Vector3(16, 16, 1), 0.5f).SetEase(Ease.OutQuart);
    }
}
