using Godot;
using System;

public class EnemyDead : State
{
    float _timer;
    float _duration = 3.0f;

    public override void Enter( Godot.Object agent ) 
    {
        BaseEnemy e = agent as BaseEnemy;
        e.AnimPlayer.TransitionNodeSetCurrent( "state" , 2 );
        e.Velocity.z = 0;
        e.SetCollisionLayerBit( 2, false );
        base.Enter( agent );
    }

    public override void Update( Godot.Object agent )
    {
        BaseEnemy e = agent as BaseEnemy;
        e.Velocity.y = -Const.Gravity;
        _timer += e.GetProcessDeltaTime();
        if ( _timer >= _duration )
            e.QueueFree();            
        base.Update( agent );
    }

}