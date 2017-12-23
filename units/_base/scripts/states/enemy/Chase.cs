using Godot;
using System;

public class Chase : State
{
    BasePlayer _target;

    public override void Enter( Godot.Object agent ) 
    {
        BaseEnemy e = agent as BaseEnemy;
        AnimationPlayer ap = e.FindNode( "AnimationPlayer" ) as AnimationPlayer;
        ap.Play( "walk-loop" );
        _target = e.FindPlayer();
        base.Enter( agent );
    }

    public override void Update( Godot.Object agent )
    {
        BaseUnit u = agent as BaseUnit;        
        Vector3 diff = _target.GetTranslation() - u.GetTranslation();
        if ( diff.z > 0 )
            u.SetFacing( eUnitFacing.LEFT );
        if ( diff.z < 0 )
            u.SetFacing( eUnitFacing.RIGHT );

        if ( diff.Length() > u.Attributes.RunSpeed )
            diff = diff.Normalized() * u.Attributes.RunSpeed;
        u.Velocity = diff;
        base.Update( agent );
    }
}