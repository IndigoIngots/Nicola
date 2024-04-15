using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using DG.Tweening;
using unityroom.Api;

public class MovieManager : MonoBehaviour
{
    [SerializeField] private GameObject GameSceneObj, GameSceneCanvas;

    [System.NonSerialized] public int score, ReleaseNum;
    [SerializeField] private Text StoryText, ScoreText;

    [SerializeField] private GameObject curtain;
    [SerializeField] private GameObject nicola;
    [SerializeField] private GameObject messageWindow;

    [SerializeField] private GameObject ResultPanel;

    [SerializeField] private Image BlackFade;

    [SerializeField] private AudioSource ResultBGM, ResultLoop;

    bool isStart = false;
    bool isComplete = false;
    Tween SendText;

    //SEを鳴らさない文字
    private static readonly string[] INVALID_CHARS = {
  " ", "　", "!", "?", "！", "？", ".", ",", "、", "。", "…"
};
    //SEを鳴らすためのやつ
    [SerializeField]
    private AudioSource _audioSource = default;
    [SerializeField] AudioClip ClicSE;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isStart == true && isComplete == false)
        {
            CompleteText();
        }
    }

    void EndMessage()
    {
        Invoke("MakeComplete", 0.1f);
    }

    void MakeComplete()
    {
        isComplete = true;
    }

    void CompleteText()
    {
        SendText.Complete();
        Debug.Log("CompleteText");
    }

    public void StartEnd()
    {
        curtain.transform.DOLocalMove(new Vector3(0, 0, 0), 2.5f).SetEase(Ease.OutBack);

        Invoke("StartMovie", 3.5f);
    }

    void StartMovie()
    {
        GameSceneCanvas.SetActive(false);
        GameSceneObj.SetActive(false);

        messageWindow.transform.DOLocalMove(new Vector3(0, -200, 0), 0.5f);
        nicola.transform.DOLocalMove(new Vector3(320, -20, 0), 1.0f);

        if (score < 1000)
        {
            StartNormalEnd();
        }
        else
        {
            StartTrueEnd();
        }
    }


    [System.Serializable]
    public class NormalEndText
    {
        [TextArea] public string normalEndText;
    }
    [SerializeField] List<NormalEndText> NormalEndTexts = new List<NormalEndText>();

    void StartNormalEnd()
    {
        StartCoroutine("NormalTalk");
    }

    IEnumerator NormalTalk()
    {
        yield return new WaitForSeconds(1.0f);
        isStart = true;

        for (int i = 0; i < NormalEndTexts.Count; i++)
        {
            StoryText.text = "";
            var beforeText = StoryText.text;

            SendText = StoryText.DOText(NormalEndTexts[i].normalEndText, 1f)
                .SetEase(Ease.Linear)
                  .OnUpdate(() =>
                  {//更新される度に実行される(※テキストが変更された時ではない)
                   //現在のテキストを取得、変化していなければ処理しない
                      var currentText = StoryText.text;
                      if (beforeText == currentText)
                      {
                          return;
                      }

                      //新たに追加された文字を取得
                      var newChar = currentText[currentText.Length - 1].ToString();

                      //SEを鳴らさないやつでなければ鳴らす
                      if (!INVALID_CHARS.Contains(newChar))
                      {
                          _audioSource.PlayOneShot(ClicSE);

                      }
                      //次のチェック用にテキスト更新
                      beforeText = currentText;
                  })
                  .OnComplete(EndMessage);

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && isStart == true && isComplete == true);
            yield return null;
        }

        ShowResult();
        yield return null;
    }


    [System.Serializable]
    public class TrueEndText
    {
        [TextArea] public string trueEndText;
    }
    [SerializeField] List<TrueEndText> TrueEndTexts = new List<TrueEndText>();

    void StartTrueEnd()
    {
        StartCoroutine("TrueTalk");
    }

    IEnumerator TrueTalk()
    {
        yield return new WaitForSeconds(1.0f);
        isStart = true;

        for (int i = 0; i < TrueEndTexts.Count; i++)
        {
            StoryText.text = "";
            var beforeText = StoryText.text;

            SendText = StoryText.DOText(TrueEndTexts[i].trueEndText, 1f)
                .SetEase(Ease.Linear)
                  .OnUpdate(() =>
                  {//更新される度に実行される(※テキストが変更された時ではない)
                   //現在のテキストを取得、変化していなければ処理しない
                      var currentText = StoryText.text;
                      if (beforeText == currentText)
                      {
                          return;
                      }

                      //新たに追加された文字を取得
                      var newChar = currentText[currentText.Length - 1].ToString();

                      //SEを鳴らさないやつでなければ鳴らす
                      if (!INVALID_CHARS.Contains(newChar))
                      {
                          _audioSource.PlayOneShot(ClicSE);

                      }
                      //次のチェック用にテキスト更新
                      beforeText = currentText;
                  })
                  .OnComplete(EndMessage);

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && isStart == true && isComplete == true);
            yield return null;
        }

        ShowResult();
        yield return null;
    }

    void ShowResult()
    {

        UnityroomApiClient.Instance.SendScore(1, score, ScoreboardWriteMode.HighScoreDesc);
        UnityroomApiClient.Instance.SendScore(2, ReleaseNum, ScoreboardWriteMode.HighScoreDesc);
        ScoreText.text = "Score:" + score.ToString("D4");

        StartCoroutine("CheckResultBGM");
    }

    IEnumerator CheckResultBGM()
    {
        yield return new WaitForSeconds(0.1f);
        ResultBGM.Play();
        while (true)
        {
            if (ResultBGM.time > 0)
            {
                Invoke("StartResultLoop", 202f);
                nicola.transform.DOLocalMove(new Vector3(320, -550, 0), 0.5f);
                messageWindow.transform.DOLocalMove(new Vector3(0, -730, 0), 0.5f);

                ResultPanel.transform.DOLocalMove(new Vector3(10, 625, 0), 2.5f).SetEase(Ease.OutBack);
                break;
            }
            yield return null;
        }
        yield return null;
    }

    void StartResultLoop()
    {
        ResultLoop.Play();
    }

    public void PushTweet()
    {
        naichilab.UnityRoomTweet.Tweet("changecircus", "ニコラは早着替えショーをこなして、今日も日銭を稼いだ！。 Score:" + score, "unityroom", "unity1week", "#WonderMagicaNicola");
    }

    public void PushRetry()
    {
        DOTween.ToAlpha(
        () => BlackFade.color,
        color => BlackFade.color = color,
        1f, 1f);
        ResultBGM.DOFade(0, 1.0f);
        Invoke("LoadRetry", 1.5f);
    }

    void LoadRetry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PushTitle()
    {
        DOTween.ToAlpha(
        () => BlackFade.color,
        color => BlackFade.color = color,
        1f, 1f);
        ResultBGM.DOFade(0, 1.0f);
        Invoke("LoadTitle", 1.5f);
    }

    void LoadTitle()
    {
        SceneManager.LoadScene("Home");
    }
}
