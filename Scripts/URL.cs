using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URL : MonoBehaviour
{
    [SerializeField] private string URLName;
    public void Push()
    {
        Application.OpenURL(URLName);
    }
}
