using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GalleryManager : MonoBehaviour
{
    [SerializeField] private TitleManager titleManager;
    [SerializeField] private SEManager seManager;
    public CostumeData[] Datas;
    [SerializeField] private ButtonScript[] Buttons;
    [SerializeField] private Image StandImage;
    [SerializeField] private TextMeshProUGUI NumAndName, Description, getNum, releaseNum;
    int releaseTimes;

    void OnEnable()
    {
        NumAndName.text = "No.";
        getNum.text = "ïœëïÇµÇΩêî:";
        releaseTimes = 0;

        for (int i = 0; i < Datas.Length; i++)
        {
            Buttons[i].Data = Datas[i];
            if (DataHolder.DataNums[i] >= 0)
            {
                Buttons[i].Reflect();
                releaseTimes++;
            }
            else
            { 
                Buttons[i].ReflectbutLocked();
            }
        }
        releaseNum.text = "âï˙êî:" + releaseTimes.ToString() + "/28";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isChanging = false;

    public void ReflectIntroducePage(CostumeData ReceivedData)
    {
        if (isChanging == false)
        {
            isChanging = true;
            seManager.pop();
            StartCoroutine("ChangeNicora");
            StandImage.gameObject.transform.DORotate(new Vector3(0, -90, 0), 0f);
            StandImage.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.3f);
            StandImage.sprite = ReceivedData.graphic;
            NumAndName.text = "No." + ReceivedData.number + "  " + ReceivedData.name;
            Description.text = ReceivedData.desc;
            int GetsNum = ReceivedData.number - 1;
            getNum.text = "ïœëïÇµÇΩêî:" + DataHolder.DataNums[GetsNum];
        }
    }

    IEnumerator ChangeNicora()
    { 
        yield return new WaitForSeconds(0.3f);
        isChanging = false;
        yield return null;
    }
}
