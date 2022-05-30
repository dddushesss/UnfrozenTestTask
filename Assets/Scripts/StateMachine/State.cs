using System.Collections.Generic;
using Characters;
using UnityEngine;

public abstract class State 
{
    protected Character character;
    private StateMachine.StateMachine stateMachine;
    private List<Character> characters;

    protected State(Character character, StateMachine.StateMachine stateMachine, List<Character> characters)
    {
        this.character = character;
        this.stateMachine = stateMachine;
        this.characters = characters;
    }

    public void SetNextCharacter()
    {
        character = characters[Random.Range(0, characters.Count)];
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