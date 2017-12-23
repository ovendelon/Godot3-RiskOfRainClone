using Godot;
using System;

public class PlayerGround : State
{
    Vector3 _movement = new Vector3();

    public override void Enter( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        p.AnimPlayer.TransitionNodeSetCurrent( "movement" , 1 );
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
        p.AnimPlayer.TransitionNodeSetCurrent( "movement" , Math.Abs( _movement.z ) > 0 ? 1 : 0 );

        base.Update( agent );
    }
}