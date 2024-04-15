using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip Cymbal, Success, Miss;
    [SerializeField] private AudioClip Buzzer, Page, Pop;

    public void cymbal()
    {
        audioSource.PlayOneShot(Cymbal);
    }

    public void success()
    {
        audioSource.PlayOneShot(Success);
    }

    public void miss()
    {
        audioSource.PlayOneShot(Miss);
    }

    public void buzzer()
    {
        audioSource.PlayOneShot(Buzzer);
    }

    public void page()
    {
        audioSource.PlayOneShot(Page);
    }

    public void pop()
    {
        audioSource.PlayOneShot(Pop);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
