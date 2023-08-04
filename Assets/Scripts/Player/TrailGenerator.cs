using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class TrailGenerator : MonoBehaviour
{
    [SerializeField] private float trailTime = 20.0f;
    [SerializeField] private float minDistance = 0.1f;
    [SerializeField] private int dashSegments = 1;
    [SerializeField] private int gapSegments = 10;

    private TrailRenderer trailRenderer;
    private Vector2 lastPosition;
    private float dash_Timer;
    private float wait_Timer;
    private int currentSegment = 0;
    private bool isDashing = true;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        lastPosition = transform.position;
        trailRenderer.time = trailTime;
    }

    private void FixedUpdate()
    {
        float distanceMoved = Vector2.Distance(transform.position, lastPosition);

        if (distanceMoved < minDistance) return;

        if (isDashing)
        {
            trailRenderer.emitting = true;
            trailRenderer.AddPosition(transform.position);

            lastPosition = transform.position;
            currentSegment++;

            if (currentSegment < dashSegments) return;
            
            currentSegment = 0;
            isDashing = false;
        }
        else
        {
            currentSegment++;
            trailRenderer.emitting = false;
            
            if (currentSegment < gapSegments) return;
            
            currentSegment = 0;
            isDashing = true;
        }
    }
}