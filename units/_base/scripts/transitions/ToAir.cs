using Godot;
using System;

public class ToAir : Transition
{
    public ToAir( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BaseUnit u = agent as BaseUnit;
        bool is_on_ground = u.IsOnFloor();
        return !is_on_ground;
    }
}