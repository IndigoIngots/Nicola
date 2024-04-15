using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNoise : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;

    // Update is called once per frame
    void FixedUpdate()
    {
        int RandomNum0 = Random.Range(0, 100);

        if (RandomNum0 >= 80)
        {
            int RandomNum = Random.Range(0, sprites.Length);
            RandomNum0 = Random.Range(0, 100);
            image.sprite = sprites[RandomNum];
        }
        else
        {
            image.sprite = null;
        }
    }
}
