using Godot;
using System;

public class EnemyStartTelegraph : Transition
{
    public EnemyStartTelegraph( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BaseEnemy e = agent as BaseEnemy;
        BasePlayer p = e.FindPlayer();

        float distance = ( p.GetTranslation() - e.GetTranslation() ).Length();
        bool is_in_range = distance < 1.5;
        bool cooldown_reset = e.AttackTimer == 0;

        return is_in_range && cooldown_reset;
    }
}