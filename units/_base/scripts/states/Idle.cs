using Godot;
using System;

public class Idle : State
{
    public override void Enter( Godot.Object agent ) 
    {
        BaseEnemy e = agent as BaseEnemy;
        e.Velocity.z = 0;
        AnimationPlayer ap = e.FindNode( "AnimationPlayer" ) as AnimationPlayer;
        ap.Play( "idle-loop" );
        base.Enter( agent );
    }
}