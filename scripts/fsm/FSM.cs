using Godot;
using System;
using System.Collections.Generic;

public class FSM : Reference
{
    State _currentState;
    Godot.Object _agent;
    List<Transition> _global_transitions = new List<Transition>();

    public FSM( Godot.Object agent, State initial_state )
    {
        _currentState = initial_state;
        _currentState.Enter( agent );
        _agent = agent;
    }

    public void AddTransition( Transition t ) { _global_transitions.Add( t ); }
    public void AddTransition( Transition[] ts ) { foreach ( Transition t in ts ) _global_transitions.Add( t ); }

    public void SetState( State new_state )
    {
        _currentState.Exit( _agent );
        _currentState = new_state;
        _currentState.Enter( _agent );
    }

    public void Update(  )
    {
        // Try global transitions first
        Transition triggered_transition = null;
        foreach ( Transition t in _global_transitions )
            if ( t.IsTriggered( _agent ) )
            {
                triggered_transition = t;
                break;
            }

        // If none fires try state transitions
        if ( triggered_transition == null )
            foreach ( Transition t in _currentState.GetTransitions() )
                if ( t.IsTriggered( _agent ) )
                {
                    triggered_transition = t;
                    break;
                }

        // If there is a transition fired - change the state
        if ( triggered_transition != null )
        {
            triggered_transition.OnTriggered( _agent );
            SetState( triggered_transition.GetTargetState() );
        }

        _currentState.Update( _agent );
    }
}