using Godot;
using System;

public interface IUnitDamageable
{
    bool TakeDamage( float amount ); // returns true if damage is fatal
    void Die( );
}

public interface IUnitPushable
{
    void PushBack( Vector3 force );
}

public interface IUnitHealable
{
    void Heal( float amount );
}

public interface IUnitDoesDamage
{
    float CalculateDamage( BaseUnit target ); // if negative -> damage is critical
}

public interface IUnitCollectsCoins
{
    void CollectCoins( int amount );
}