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
        Attack( e );
        base.Enter( agent );
    }

    void Attack( BaseEnemy enemy )
    {
        var mele_area = (Area)enemy.FindNode( "MeleAttackArea" );
        foreach ( object obj in mele_area.GetOverlappingBodies() )
            if ( obj is BasePlayer )
            {
                BasePlayer player = obj as BasePlayer;
                player.TakeDamage( enemy.CalculateDamage(player) );
            }
    }

    public override void Update( Godot.Object agent )
    {
        BaseUnit u = agent as BaseUnit;        
        u.Velocity.y = -Const.Gravity;
        base.Update( agent );
    }

}