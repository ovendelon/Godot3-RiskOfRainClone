using Godot;
using System;

public class PlayerJump : Transition
{
    public PlayerJump( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        //bool is_on_ground = p.IsOnFloor();
        bool key_pressed = Input.IsKeyPressed( GD.KEY_SPACE );
        return key_pressed;
    }

    public override void OnTriggered( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        float y = (float)Math.Sqrt( 0 - 2f * -Const.Gravity * p.Attributes.JumpHeight );
        p.Velocity.y = y;
    }
}

