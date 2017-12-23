using Godot;
using System;

public class EnemyTelegraph : State
{
    BasePlayer _target;

    public override void Enter( Godot.Object agent ) 
    {
        BaseEnemy e = agent as BaseEnemy;
        e.AnimPlayer.OneshotNodeStart( "telegraph" );
        _target = e.FindPlayer();
        e.Velocity.z = 0;
        base.Enter( agent );
    }

    public override void Update( Godot.Object agent )
    {
        BaseUnit u = agent as BaseUnit;        
        Vector3 diff = _target.GetTranslation() - u.GetTranslation();
        if ( diff.z < 0 )
            u.SetFacing( eUnitFacing.LEFT );
        if ( diff.z > 0 )
            u.SetFacing( eUnitFacing.RIGHT );
        u.Velocity.y = -Const.Gravity;
        base.Update( agent );
    }
}