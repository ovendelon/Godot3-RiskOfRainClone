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

        var st_ground = new PlayerGround();
        var st_air = new PlayerAir();

        var tr_jump = new PlayerJump( st_air );
        var tr_fall = new PlayerFall( st_air );
        var tr_land = new PlayerLand( st_ground );

        st_ground.AddTransition( tr_jump );
        st_ground.AddTransition( tr_fall );
        st_air.AddTransition( tr_land );

        _fsm = new FSM( this , st_ground );

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

    public void PlayAnimation( )
    {

    }

}
