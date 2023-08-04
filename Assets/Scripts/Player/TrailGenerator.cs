using System;
using Managers;
using UnityEngine;

public class TrailGenerator : MonoBehaviour
{
    [SerializeField] private float trailTime = 20.0f;
    [SerializeField] private float dotInterval = 0.2f;
    [SerializeField] private float minDistance = 0.1f;

    private float timeSinceLastDot = 0f;
    private TrailRenderer trailRenderer;
    private Vector2 lastPosition;

    private void Awake()
    {
        GameManager.OnPlayerFeed.AddListener(Clear);
        trailRenderer = GetComponent<TrailRenderer>();
        lastPosition = transform.position;
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerFeed.RemoveListener(Clear);
    }

    private void FixedUpdate()
    {
        // Update the time since the last dot
        timeSinceLastDot += Time.deltaTime;

        // If enough time has passed, add a dot to the trail
        if (timeSinceLastDot >= dotInterval)
        {
            trailRenderer.AddPosition(transform.position);
            timeSinceLastDot = 0f; // Reset the timer
        }

        // Fade out the oldest points in the trail
        trailRenderer.time = trailTime;
    }

    private void Clear(int score_)
    {
        trailRenderer.Clear();
    }
}