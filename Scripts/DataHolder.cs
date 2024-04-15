using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static int[] DataNums;

    void Awake()
    {
        DataNums = new int[28];
        for (int i = 0; i < DataNums.Length; i++)
        {
            DataNums[i] = PlayerPrefs.GetInt(i.ToString(), 0);
        }
    }
}
