using Godot;
using System;

public class PlayerFall : Transition
{
    public PlayerFall( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        bool is_on_ground = p.IsOnFloor();
        return !is_on_ground;
    }
}