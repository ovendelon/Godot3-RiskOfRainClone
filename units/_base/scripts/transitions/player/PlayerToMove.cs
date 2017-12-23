using Godot;
using System;

public class PlayerToMove : Transition
{
    public PlayerToMove( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        bool is_on_ground = p.IsOnFloor();
        bool key_pressed = Input.IsKeyPressed( GD.KEY_LEFT ) || Input.IsKeyPressed( GD.KEY_RIGHT );

        bool has_velocity = Math.Abs( p.Velocity.z ) > 0.5;
        return is_on_ground && ( key_pressed || has_velocity );
    }
}