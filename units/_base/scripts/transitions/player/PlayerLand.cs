using Godot;
using System;

public class PlayerLand : Transition
{
    public PlayerLand( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        bool is_on_ground = p.IsOnFloor();
        return is_on_ground;
    }

    public override void OnTriggered( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        p.AnimPlayer.TransitionNodeSetCurrent( "land-inplace-running" , Math.Abs( p.Velocity.z ) > 0.2 ? 1 : 0 );
        p.AnimPlayer.OneshotNodeStart( "land" );
    }    
}