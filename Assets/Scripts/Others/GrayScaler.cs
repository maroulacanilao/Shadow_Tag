using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrayScaler : MonoBehaviour
{
    [SerializeField] private Material material;

    private void OnEnable()
    {
        Greyscale();
    }

    private void Greyscale()
    {
        var _sprites = FindObjectsOfType<SpriteRenderer>();
        foreach (var _sprite in _sprites)
        {
            _sprite.material = material;
        }
        
        var _tilemaps = FindObjectsOfType<TilemapRenderer>();
        foreach (var _tilemap in _tilemaps)
        {
            _tilemap.material = material;
        }
    }
}
