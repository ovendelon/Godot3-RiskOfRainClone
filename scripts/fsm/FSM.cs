using Godot;
using System;

public class FSM : Reference
{
    State _currentState;
    Godot.Object _agent;

    public FSM( Godot.Object agent, State initial_state )
    {
        _currentState = initial_state;
        _currentState.Enter( agent );
        _agent = agent;
    }

    public void SetState( State new_state )
    {
        _currentState.Exit( _agent );
        _currentState = new_state;
        _currentState.Enter( _agent );
    }

    public void Update(  )
    {
        Transition triggered_transition = null;

        foreach ( Transition t in _currentState.GetTransitions() )
            if ( t.IsTriggered( _agent ) )
            {
                triggered_transition = t;
                break;
            }

        if ( triggered_transition != null )
        {
            triggered_transition.OnTriggered( _agent );
            SetState( triggered_transition.GetTargetState() );
        }

        _currentState.Update( _agent );
    }
}