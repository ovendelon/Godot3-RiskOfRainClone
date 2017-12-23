using Godot;
using System;

public class BaseEnemy : BaseUnit, IUnitPushable
{

    public float AttackTimer;
    public float AttackCooldown = 3.0f;

    public float AttackDistance = 2;
    public float ChaseDistance = 20;  

    public override void _Ready()
    {
        _collision_layer = 2;
        ((Area)FindNode( "MeleAttackArea" )).SetCollisionMaskBit( 1, true );    // monitor only player

        var state_spawn = new OneShotAnim( "spawn" );
        var state_chase = new EnemyChase();
        var state_idle = new EnemyIdle();
        var state_telegraph = new EnemyTelegraph();
        var state_attack = new EnemyAttack();

        var tr_spawn_finished = new FromOneShotAnim( state_idle , "spawn" );
        var tr_to_chase = new EnemyToChase( state_chase );
        var tr_to_idle = new EnemyToIdle( state_idle );
        var tr_start_telegraph = new EnemyStartTelegraph( state_telegraph );
        var tr_telegraph_finished = new FromOneShotAnim( state_attack , "telegraph" );
        var tr_telegraph_interrupt = new EnemyInterruptTelegraph( state_idle );
        var tr_attack_finished = new FromOneShotAnim( state_idle , "attack" );

        state_spawn.AddTransition( tr_spawn_finished );
        state_chase.AddTransition( tr_to_idle );
        state_chase.AddTransition( tr_start_telegraph );
        state_idle.AddTransition( tr_to_chase );
        state_idle.AddTransition( tr_start_telegraph );
        state_telegraph.AddTransition( tr_telegraph_finished );
        state_telegraph.AddTransition( tr_telegraph_interrupt );
        state_attack.AddTransition( tr_attack_finished );

        _fsm = new FSM( this , state_spawn );
        _fsm.AddTransition( new EnemyDie( new EnemyDead() ) );

        // temp
        Attributes.RunSpeed = 10;
        Attributes.MaxHealth = Attributes.Health = 20;
        Attributes.Damage = 10;
        Attributes.CritChance = 0.1f;
    }

    public BasePlayer FindPlayer()
    {
        object[] players = GetTree().GetNodesInGroup( "player" );
        if ( players.Length > 0 )
            return (BasePlayer)players[0];
        return null;
    }
    
    public override void _Process(float delta)
    {
        AttackTimer = Math.Max( 0, AttackTimer - delta ); 
    }

    public void PushBack( Vector3 force )
    {
        Velocity += force;
    }
}
