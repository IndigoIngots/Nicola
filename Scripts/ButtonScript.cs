using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GalleryManager galleryManager;

    [System.NonSerialized] public CostumeData Data;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private Image charaStand;
    [SerializeField] private GameObject LockImage;
    bool isLocked = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reflect()
    {
        numberText.text = "No." + Data.number.ToString("D2");
        charaStand.sprite = Data.graphic;
        LockImage.SetActive(false);
        isLocked = false;
    }

    public void ReflectbutLocked()
    {
        numberText.text = "No." + Data.number.ToString("D2");
        charaStand.sprite = Data.graphic;
        LockImage.SetActive(true);
        isLocked = true;
    }

    public void PushButton()
    {
        if (isLocked == false) galleryManager.ReflectIntroducePage(Data);
    }
}
