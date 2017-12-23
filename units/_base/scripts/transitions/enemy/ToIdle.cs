using Godot;
using System;

public class ToIdle : Transition
{
    public ToIdle( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered(Godot.Object agent)
    {
        BaseEnemy e = agent as BaseEnemy;
        BasePlayer p = e.FindPlayer();

        float distance = ( p.GetTranslation() - e.GetTranslation() ).Length();

        return distance <= 1;
    }
}