using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BPMMoves : MonoBehaviour
{

    [SerializeField] private GameObject[] Objs;
    [SerializeField] private GameObject[] Lights;
    [SerializeField] private GameObject[] Toys;

    float Tempo = 1 / 2.2f;

    void Awake()
    {
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 800, sequencesCapacity: 200);
    }

    public void StartMusic()
    {
        //StartCoroutine(Loop());
        InvokeRepeating("Bounce", 0, Tempo);
    }


    IEnumerator Loop()
    {
        while (true)
        {
            Bounce();
            yield return new WaitForSeconds(Tempo);
        }
    }

    void Bounce()
    {
        for (int i = 0; i < Objs.Length; i++)
        {
            Vector3 initialScale = Objs[i].transform.localScale;
            Objs[i].transform.DOScale((initialScale * 1.05f), 0);
            Objs[i].transform.DOScale(initialScale, 0.4f);
        }

        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0);
            Lights[i].transform.DOScale(new Vector3 (1, 1, 1), 0.4f);
        }

        for (int i = 0; i < Toys.Length; i++)
        {
            Toys[i].transform.DOScale(new Vector3(0.7f, 0.7f, 1f), 0);
            Toys[i].transform.DOScale(new Vector3(0.6f, 0.6f, 1f), 0.4f);
        }
    }

    public void EndGame()
    {
        CancelInvoke();
    }
}
