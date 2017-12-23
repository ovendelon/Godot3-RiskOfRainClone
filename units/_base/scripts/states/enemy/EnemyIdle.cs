using Godot;
using System;

public class EnemyIdle : State
{
    public override void Enter( Godot.Object agent ) 
    {
        BaseEnemy e = agent as BaseEnemy;
        e.Velocity.z = 0;
        e.AnimPlayer.TransitionNodeSetCurrent( "state" , 0 );
        base.Enter( agent );
    }

    public override void Update( Godot.Object agent ) 
    {
        BaseEnemy e = agent as BaseEnemy;
        e.Velocity.y = -0.5f;
        base.Update( agent );
    }
}