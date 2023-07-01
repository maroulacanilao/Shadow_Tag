using System;
using System.Collections;
using System.Collections.Generic;
using CustomHelpers;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    private float defaultYPos;
    
    public float worldHeight { get; private set; }
    public float worldWidth { get; private set; }
        
    public float min_X => transform.position.x - (worldWidth/2f);
    public float max_X => transform.position.x + (worldWidth / 2f);
    public float min_Y => transform.position.y - (worldHeight / 2f);
    public float max_Y => transform.position.y + (worldHeight / 2f);

    public Vector3 playerPos => player.position;
    
    private void Awake()
    {
        defaultYPos = transform.position.y;
        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        float aspect = (float)Screen.width / Screen.height;
            
        worldHeight = gameObject.scene.GetFirstMainCameraInScene().orthographicSize * 2;
        
        worldWidth = worldHeight * aspect;
    }
    
    private void LateUpdate()
    {
        transform.position += Vector3.right * (speed * Time.deltaTime);
        PlayerBounds();
    }
    
    void PlayerBounds()
    {
        if (playerPos.x < max_X && playerPos.x > min_X && playerPos.y > min_Y) return;
        
        Debug.Log("player out of bounds");
    }
}
