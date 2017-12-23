using Godot;
using System;

public class PlayerSkill3 : State
{
    float _dodgeSpeed = 12;

    public override void Enter( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        p.AnimPlayer.TransitionNodeSetCurrent( "skill-select" , 2 );
        p.AnimPlayer.OneshotNodeStart( "skills" );
        p.SkillTimers[2] = p.SkillCooldowns[2];
        p.SetCollisionLayerBit( 1, false );         // disable collision while dodging
        base.Enter( agent );
    }

    public override void Update( Godot.Object agent ) 
    {
        BasePlayer p = agent as BasePlayer;
        float f = p.Facing == eUnitFacing.LEFT ? -1 : 1;
        p.Velocity.z = _dodgeSpeed * f;
        base.Update( agent );
    }
    
    public override void Exit(Godot.Object agent)
    {
        BasePlayer p = agent as BasePlayer;
        p.SetCollisionLayerBit( 1, true );          // enable collision again
        base.Exit( agent );
    }
}