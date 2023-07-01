using CustomHelpers;
using NaughtyAttributes;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 5f;
    [CurveRange(0, 0, 1, 1, EColor.Yellow)]
    [SerializeField] private AnimationCurve curve;

    private int currentWaypointIndex;
    private void Start()
    {
        currentWaypointIndex = UnityEngine.Random.Range(0, waypoints.Length);
    }
    
    void Update()
    {
        var _pos = transform.position;
        Vector3 _direction = waypoints[currentWaypointIndex].position - _pos;
        _direction.Normalize();
        
        float _currentSpeed = speed * curve.Evaluate(Vector3.Distance(_pos, waypoints[currentWaypointIndex].position));
        
        transform.AddPosition(_direction * (_currentSpeed * Time.deltaTime));
        
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) >= 0.25f) return;
        
        currentWaypointIndex++;

        if (currentWaypointIndex < waypoints.Length) return;
        
        currentWaypointIndex = 0;
    }
}
