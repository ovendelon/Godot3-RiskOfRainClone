using Godot;
using System;

public class PlayerAir : State
{
    float _airControlFactor = 0.5f;
    Vector3 _movement = new Vector3();

    public override void Enter( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        p.AnimPlayer.TransitionNodeSetCurrent( "movement" , 2 );
    }

    public override void Update( Godot.Object agent ) 
    {
        BasePlayer p = agent as BasePlayer;

        _movement.z = 0;
        if ( Input.IsKeyPressed( GD.KEY_LEFT ) )
        {
            _movement.z -= p.Attributes.RunSpeed * _airControlFactor;
            p.SetFacing( eUnitFacing.LEFT );
        }
        if ( Input.IsKeyPressed( GD.KEY_RIGHT ) )
        {
            _movement.z += p.Attributes.RunSpeed * _airControlFactor;
            p.SetFacing( eUnitFacing.RIGHT );            
        }
        p.Velocity.y -= Const.Gravity * p.GetPhysicsProcessDeltaTime();
        p.Velocity.z += _movement.z;
        if ( Math.Abs( p.Velocity.z ) > p.Attributes.RunSpeed )
            p.Velocity.z = p.Velocity.z / Math.Abs( p.Velocity.z ) * p.Attributes.RunSpeed;

        base.Update( agent );
    }
}