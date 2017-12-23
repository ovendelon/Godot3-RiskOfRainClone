using Godot;
using System;

public class BaseEnemy : BaseUnit
{

    public override void _Ready()
    {
        _collision_layer = 2;

        var state_spawn = new OneShotAnim( "spawn" );
        var state_chase = new EnemyChase();
        var state_idle = new EnemyIdle();

        var tr_spawn_finished = new FromOneShotAnim( state_idle , "spawn" );
        var tr_to_chase = new EnemyToChase( state_chase );
        var tr_to_idle = new EnemyToIdle( state_idle );

        state_spawn.AddTransition( tr_spawn_finished );
        state_chase.AddTransition( tr_to_idle );
        state_idle.AddTransition( tr_to_chase );

        _fsm = new FSM( this , state_spawn );

        // temp
        Attributes.RunSpeed = 10;  
    }

    public BasePlayer FindPlayer()
    {
        object[] players = GetTree().GetNodesInGroup( "player" );
        if ( players.Length > 0 )
            return (BasePlayer)players[0];
        return null;
    }
    
}
