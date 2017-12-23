using Godot;
using System;

public class PlayerMove : State
{
    Vector3 _movement = new Vector3();

    public PlayerMove() { }

    public override void Enter( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        AnimationPlayer ap = p.FindNode( "AnimationPlayer" ) as AnimationPlayer;
        ap.Play( "run-loop" );
    }

    public override void Update( Godot.Object agent ) 
    {
        BasePlayer p = agent as BasePlayer;

        p.Velocity.y = -0.5f;
        _movement.z = 0;
        if ( Input.IsKeyPressed( GD.KEY_LEFT ) )
        {
            _movement.z -= p.Attributes.RunSpeed;
            p.SetFacing( eUnitFacing.LEFT );
        }
        if ( Input.IsKeyPressed( GD.KEY_RIGHT ) )
        {
            _movement.z += p.Attributes.RunSpeed;
            p.SetFacing( eUnitFacing.RIGHT );            
        }

        p.Velocity.z = _movement.z;

        // AnimationPlayer ap = p.FindNode( "AnimationPlayer" ) as AnimationPlayer;
        // if ( Math.Abs( _movement.z ) > 0.1 )
        //     ap.Play( "run-loop" );
        // else
        //     ap.Play( "idle-loop" );

        base.Update( agent );
    }
}