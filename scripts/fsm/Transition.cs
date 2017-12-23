using Godot;
using System;

public abstract class Transition : Reference
{
    State _targetState;
    public Transition( State target_state ) { _targetState = target_state; }
    public abstract bool IsTriggered( Godot.Object agent );
    public virtual State GetTargetState() { return _targetState; }
    public virtual void OnTriggered( Godot.Object agent ) {}
}