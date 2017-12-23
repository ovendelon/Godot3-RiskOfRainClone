using Godot;
using System;

public class EnemyInterruptTelegraph : Transition
{
    public EnemyInterruptTelegraph( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BaseEnemy e = agent as BaseEnemy;
        BasePlayer p = e.FindPlayer();

        float distance = ( p.GetTranslation() - e.GetTranslation() ).Length();
        bool is_out_of_range = distance > 2.5;
        return is_out_of_range;
    }
}