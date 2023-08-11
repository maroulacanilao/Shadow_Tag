using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablerOnActivate : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDisable;
    [SerializeField] private bool willEnable = true;

    private void OnEnable()
    {
        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(willEnable);
        }
    }
}
