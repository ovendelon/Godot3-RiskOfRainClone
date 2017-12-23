using Godot;
using System;

public class FromOneShotAnim : Transition
{

    string _anim_id;

    public FromOneShotAnim( State target_state , string anim_id ) : base( target_state )
    {
        _anim_id = anim_id;
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BaseUnit u = agent as BaseUnit;
        bool is_active = u.AnimPlayer.OneshotNodeIsActive( _anim_id );
        return !is_active;
    }
}