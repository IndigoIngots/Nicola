using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMouseMove : MonoBehaviour
{
    [SerializeField] private GalleryManager galleryManager;
    [SerializeField] private Image Chara1;
    [SerializeField] private Image Chara2;

    [SerializeField] private GameObject Layer1;
    [SerializeField] private GameObject Layer2;
    [SerializeField] private GameObject Layer3;

    [SerializeField] private float Num1, Num2, Num3;

    void OnEnable()
    {
        int RandomNum = Random.Range(0, galleryManager.Datas.Length);
        Chara1.sprite = galleryManager.Datas[RandomNum].graphic;
        Chara2.sprite = galleryManager.Datas[RandomNum].graphic;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 correct1 = new Vector3(mousePos.x / 480, mousePos.y / 270, 0);

        Layer1.transform.localPosition = correct1 * Num1;
        Layer2.transform.localPosition = correct1 * Num2;
        Layer3.transform.localPosition = correct1 * Num3;
    }
}
