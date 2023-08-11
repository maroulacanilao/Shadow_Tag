using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MusicChangeOnActivate : MonoBehaviour
{
    [SerializeField] private string musicId, sfxId;

    private void OnEnable()
    {
        if(!string.IsNullOrEmpty(musicId)) AudioManager.OnPlayBgm.Invoke(musicId);
        if(!string.IsNullOrEmpty(sfxId)) AudioManager.OnPlaySfx.Invoke(sfxId);
    }
}
