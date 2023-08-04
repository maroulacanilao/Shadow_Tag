using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity2D : MonoBehaviour
{
    private Vector3 lastPosition;
    public Vector3 currentVelocity { get; private set; }

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}
