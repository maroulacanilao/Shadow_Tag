using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject cam1;
    [SerializeField] private GameObject cam2;

    private void Awake()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }
    
    public void Switch()
    {
        cam1.SetActive(!cam1.activeSelf);
        cam2.SetActive(!cam1.activeSelf);
    }
}
