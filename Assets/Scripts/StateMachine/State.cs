using Characters;
using UnityEngine;

public abstract class State 
{
    protected Character character;
    protected StateMachine.StateMachine stateMachine;

    protected State(Character character, StateMachine.StateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }

    protected void DisplayOnUI()
    {
        Debug.Log(this);
    }
}