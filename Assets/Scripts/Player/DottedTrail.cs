using System.Collections.Generic;
using UnityEngine;

public class DottedTrail : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private int dashLength = 5; // Adjust this to control the length of each dash
    [SerializeField] private int gapLength = 3;  // Adjust this to control the gap between dashes
    private int dashCount = 0;
    private int gapCount = 0;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.Clear(); // Clear any existing trail at the start
    }

    private void Update()
    {
        if(rb.velocity.magnitude < 0.1f) return;
        
        // Assuming your player's movement code updates its position here

        // Check if it's time to add a new dash or gap
        if (dashCount <= 0)
        {
            trailRenderer.emitting = false; // Turn off emission during gaps
            gapCount--;

            if (gapCount <= 0)
            {
                gapCount = gapLength;
                dashCount = dashLength;
                trailRenderer.emitting = true; // Turn on emission during dashes
            }
        }
        else
        {
            dashCount--;
        }
    }
}