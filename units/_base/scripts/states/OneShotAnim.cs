using Godot;
using System;

public class OneShotAnim : State
{
    string _anim_id;

    public OneShotAnim( string anim_id )
    {
        _anim_id = anim_id;
    }

    public override void Enter( Godot.Object agent ) 
    {
        BaseUnit u = agent as BaseUnit;
        u.AnimPlayer.OneshotNodeStart( _anim_id );
        base.Enter( agent );
    }
}