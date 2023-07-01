
[System.Serializable]
public abstract class UnitStateMachine
{
    public UnitState CurrentState { get; protected set; }

    public virtual void ChangeState(UnitState newState_)
    {
        CurrentState?.Exit();

        CurrentState = newState_;
        CurrentState?.Enter();
    }

    public abstract void Initialize();

    public virtual void StateUpdate()
    {
        CurrentState.LogicUpdate();
    }
    
    public virtual void StateFixedUpdate()
    {
        CurrentState.PhysicsUpdate();
    }
}
