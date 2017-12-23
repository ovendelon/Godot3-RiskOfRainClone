using Godot;
using System;
using System.Collections.Generic;

public abstract class State : Reference
{
    List<Transition> _transitions = new List<Transition>();

    public State() { }
    public State( Transition[] transitions ) { foreach ( Transition t in transitions ) _transitions.Add( t ); }
    public virtual void Enter( Godot.Object agent ) {}
    public virtual void Update( Godot.Object agent ) {}
    public virtual void Exit( Godot.Object agent ) {}

    public void AddTransition( Transition t ) { _transitions.Add( t ); }
    public void AddTransition( Transition[] ts ) { foreach ( Transition t in ts ) _transitions.Add( t ); }
    
    public List<Transition> GetTransitions() { return _transitions; }
}