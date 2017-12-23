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
        _fsm = new FSM( this , new Idle() );
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
