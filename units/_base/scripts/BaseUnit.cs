using Godot;
using System;

public enum eUnitFacing { LEFT, RIGHT };

public struct UnitAttributes
{
    public int      Level;
    public float    RunSpeed;
    public float    JumpHeight;
    public float    MaxHealth;
    public float    Health;
    public float    Damage;
    public float    CritChance;
    public float    Experience;
    public float    NextLevelExperience;
    public int      Coins;

    public void LevelUp()
    {
        Level++;
        MaxHealth = MaxHealth + MaxHealth * 0.1f;
        Health = MaxHealth;
        Damage = Damage + Damage * 0.1f;
        CritChance = CritChance + CritChance * 0.1f;
        Experience = 0.0f;
        NextLevelExperience = NextLevelExperience + NextLevelExperience * 0.1f;
    }
}

public class BaseUnit : KinematicBody, IUnitDoesDamage, IUnitDamageable
{
    public Vector3 Velocity;
    public UnitAttributes  Attributes;
    public AnimationTreePlayer AnimPlayer;

    protected FSM _fsm;
    protected int _collision_layer;

    eUnitFacing _facing = eUnitFacing.RIGHT;
    public eUnitFacing Facing { get { return _facing; } }
    Vector3 _face_left = new Vector3( 0, (float)Const.Pi, 0 );
    Vector3 _face_right = new Vector3();

    public Action<float,float>      HealthChangedEvent;
    public Action                   OwnDeathEvent;

    public override void _Ready()
    {
        AnimPlayer = FindNode( "main_anim_player" ) as AnimationTreePlayer;
        AnimPlayer.SetActive( true );
    }

    public override void _Process(float delta)
    {
        // ENSURE X == 0
        var pos = GetTranslation();
        pos.x = 0;
        SetTranslation( pos );
    }

    public override void _PhysicsProcess(float delta)
    {
        if ( _fsm != null ) _fsm.Update( );
        MoveAndSlide( Velocity , Const.FloorNormal );
    }

    public void SetFacing( eUnitFacing f )
    {
        if ( _facing == f )
            return;
        SetRotation( f == eUnitFacing.LEFT ? _face_left : _face_right );        
        _facing = f;
    }   

    public Damage CalculateDamage( BaseUnit target )
    {
        Damage dmg = new Damage();
        dmg.is_crit = MathUtils.GetRandom() <= Attributes.CritChance;
        dmg.amount = Attributes.Damage * MathUtils.GetRandom( 0.8f , 1.0f );
        if ( dmg.is_crit ) 
            dmg.amount *= MathUtils.GetRandom( 1.4f , 1.6f );
        // foreach ( rrItem item in Items )
        //     if ( item is IModifyDamage )
        //         di.Damage = ((IModifyDamage)item).ModifyDamage( target, di.Damage );
        return dmg;
    }

    public bool TakeDamage( Damage dmg )
    {
        Attributes.Health -= dmg.amount;
        if ( HealthChangedEvent != null ) HealthChangedEvent( Attributes.Health, Attributes.MaxHealth );
        if ( Attributes.Health <= 0 )
        {
            Die();
            if ( OwnDeathEvent != null ) OwnDeathEvent();
            return true;
        }
        return false;
    }

    public void Die( )
    {
        
    }

}
