using Godot;
using System;

public struct Damage { public float amount; public bool is_crit; }
public interface IUnitDoesDamage
{
    Damage CalculateDamage( BaseUnit target ); // if negative -> damage is critical
}

public interface IUnitDamageable
{
    bool TakeDamage( Damage dmg ); // returns true if damage is fatal
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

public interface IUnitCollectsCoins
{
    void CollectCoins( int amount );
}