using Godot;
using System;

public class PlayerSkill1 : State
{

    public override void Enter( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        p.AnimPlayer.TransitionNodeSetCurrent( "skill-select" , 0 );
        p.AnimPlayer.OneshotNodeStart( "skills" );
        p.SkillTimers[0] = p.SkillCooldowns[0];
        p.Velocity.z = 0;
        Shoot( p );
        base.Enter( agent );
    }

    void Shoot( BasePlayer player )
    {
        if ( player.ShootEvent != null ) player.ShootEvent();

        RayCast shoot_cast = player.GetNode( "ShootCast" ) as RayCast;
        shoot_cast.ForceRaycastUpdate();
        if ( shoot_cast.IsColliding() )
        {
            CollisionObject target = shoot_cast.GetCollider() as CollisionObject;
            if ( target is BaseEnemy )
            {
                HitEnemy( player , target as BaseEnemy );
            }
            else
            {
                HitEnvironment( player, target );
            }
        }        
    }

    public void HitEnemy( BasePlayer player, BaseEnemy enemy )
    {
        if ( enemy is IUnitDamageable )
        {
            float dmg = player.CalculateDamage( enemy );                    // return negative damage if it's fatal; probably stupid hack to avoid struct for damage
            bool crit = dmg < 0;
            dmg = Math.Abs( dmg );
            if ( crit && player.CritEvent != null ) 
                player.CritEvent();
            if ( ((IUnitDamageable)enemy).TakeDamage( dmg ) )               // return true if damage is fatal
                if ( player.DeathEvent != null ) player.DeathEvent();
            if ( player.HitEvent != null ) player.HitEvent();
        }

        if ( enemy is IUnitPushable )
            ((IUnitPushable)enemy).PushBack( player.GetTransform().basis.z * 100 );
    }

    public void HitEnvironment( BasePlayer player, CollisionObject body )
    {

    }

    public override void Update( Godot.Object agent ) 
    {
        BasePlayer p = agent as BasePlayer;
        base.Update( agent );
    }
}