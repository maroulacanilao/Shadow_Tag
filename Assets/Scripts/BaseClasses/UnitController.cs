using NaughtyAttributes;
using UnityEngine;

public abstract class UnitController : MonoBehaviour
{
    [field: Header("Components")]
    
    [field: SerializeField] [field: Foldout("Components")] 
    public Rigidbody2D rb { get; protected set; }
    
    [field: SerializeField] [field: Foldout("Components")] 
    public Animator animator { get; protected set; }
    
    [field: SerializeField] [field: Foldout("Components")] 
    public SpriteRenderer spriteRenderer { get; protected set; }
    
    [field: SerializeField] [field: Foldout("Components")] 
    public MovementCollisionDetector collisionDetector { get; private set; }
    
    [field: SerializeField] [field: Foldout("Components")] 
    public AnimationEventInvoker animationEventInvoker { get; private set; }

    protected UnitStateMachine StateMachine;
    
    protected virtual void Update()
    {
        if(Time.timeScale <= 0) return;
        StateMachine.StateUpdate();
    }
    
    protected virtual void FixedUpdate()
    {
        if(Time.timeScale <= 0) return;
        StateMachine.StateFixedUpdate();
    }
}
