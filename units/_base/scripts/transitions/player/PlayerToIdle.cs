using Godot;
using System;

public class PlayerToIdle : Transition
{
    public PlayerToIdle( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        bool is_on_ground = p.IsOnFloor();
        bool not_moving = Math.Abs( p.Velocity.z ) < 0.1;
        return is_on_ground && not_moving;
    }
}