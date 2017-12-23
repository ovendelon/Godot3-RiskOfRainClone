using Godot;
using System;

public class Idle : State
{
    public override void Enter( Godot.Object agent ) 
    {
        BaseUnit u = agent as BaseUnit;
        u.Velocity.z = 0;
        AnimationPlayer ap = u.FindNode( "AnimationPlayer" ) as AnimationPlayer;
        ap.Play( "idle-loop" );
        base.Enter( agent );
    }

    public override void Update( Godot.Object agent ) 
    {
        BaseUnit u = agent as BaseUnit;
        u.Velocity.y = -0.5f;
        base.Update( agent );
    }
}