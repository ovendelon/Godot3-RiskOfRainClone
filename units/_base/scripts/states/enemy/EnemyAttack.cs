using Godot;
using System;

public class EnemyAttack : State
{
    BasePlayer _target;

    public override void Enter( Godot.Object agent ) 
    {
        BaseEnemy e = agent as BaseEnemy;
        e.AnimPlayer.OneshotNodeStart( "attack" );
        _target = e.FindPlayer();
        e.Velocity.z = 0;
        e.AttackTimer = e.AttackCooldown;
        base.Enter( agent );
    }

    public override void Update( Godot.Object agent )
    {
        BaseUnit u = agent as BaseUnit;        
        u.Velocity.y = -Const.Gravity;
        base.Update( agent );
    }

}