using Godot;
using System;

public class EnemyDie : Transition
{
    public EnemyDie( State target_state ) : base( target_state )
    {
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BaseEnemy e = agent as BaseEnemy;
        return e.Attributes.Health <= 0;
    }
}