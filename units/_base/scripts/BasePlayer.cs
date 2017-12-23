using Godot;
using System;

public class BasePlayer : BaseUnit, IUnitCollectsCoins
{


    public float[] SkillTimers = new float[4] { 0, 0, 0, 0 };
    public float[] SkillCooldowns = { 0.5f, 3, 4, 8 };
    public Action<int,float,float>  SkillCooldownChangedEvent;

    public Action<int>              CoinsChangedEvent;

    public override void _Ready()
    {
        _collision_layer = 1;
        AddToGroup( "player" );

        var st_idle = new Idle();
        var st_move = new PlayerMove();
        var st_air = new PlayerMidAir();

        var tr_to_idle = new PlayerToIdle( st_idle );
        var tr_to_move = new PlayerToMove( st_move );
        var tr_to_air = new ToAir( st_air );
        var tr_jump = new PlayerJump( st_air );

        st_idle.AddTransition( tr_to_move );
        st_idle.AddTransition( tr_to_air );
        st_idle.AddTransition( tr_jump );
        
        st_move.AddTransition( tr_to_idle );
        st_move.AddTransition( tr_to_air );
        st_move.AddTransition( tr_jump );

        st_air.AddTransition( tr_to_idle );
        st_air.AddTransition( tr_to_move );

        _fsm = new FSM( this , st_idle );

        Attributes.RunSpeed = 8;
        Attributes.JumpHeight = 4;
    }

    public override void _Process(float delta)
    {
        for ( int i=0; i<SkillTimers.Length; i++ )
        {
            if ( SkillTimers[i] > 0 )
            {
                SkillTimers[i] = (float)Math.Max( 0.0, SkillTimers[i] - delta );
                if ( SkillCooldownChangedEvent != null ) SkillCooldownChangedEvent( i, SkillTimers[i], SkillCooldowns[i] );
            }
        }
    }

    public void CollectCoins( int amount )
    {
        Attributes.Coins += amount;
        if ( CoinsChangedEvent != null ) CoinsChangedEvent( Attributes.Coins );
    }

}
