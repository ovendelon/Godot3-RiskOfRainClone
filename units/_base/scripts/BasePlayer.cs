using Godot;
using System;

public class BasePlayer : BaseUnit, IUnitCollectsCoins
{

    public float[] SkillTimers = new float[4] { 0, 0, 0, 0 };
    public float[] SkillCooldowns = { 0.5f, 3, 4, 8 };
    public Action<int,float,float>  SkillCooldownChangedEvent;

    public Action<float,float>      ExperienceChangedEvent;
    public Action<int>              LevelChangedEvent;
    public Action<int>              CoinsChangedEvent;
    public Action                   HitEvent;
    public Action                   DeathEvent;
    public Action                   ShootEvent;
    public Action                   CritEvent; 

    public override void _Ready()
    {
        _collision_layer = 1;
        ((Area)FindNode( "MeleAttackArea" )).SetCollisionMaskBit( 2, true );  // monitor only enemies
        AddToGroup( "player" );

        var st_ground = new PlayerGround();
        var st_air = new PlayerAir();
        var st_skill1 = new PlayerSkill1();
        var st_skill3 = new PlayerSkill3();

        var tr_jump = new PlayerJump( st_air );
        var tr_fall = new PlayerFall( st_air );
        var tr_land = new PlayerLand( st_ground );
        var tr_skill1 = new PlayerActivateSkill( st_skill1, 0, GD.KEY_Z );
        var tr_skill3 = new PlayerActivateSkill( st_skill3, 2, GD.KEY_C );

        st_ground.AddTransition( tr_jump );
        st_ground.AddTransition( tr_fall );
        st_ground.AddTransition( tr_skill1 );
        st_ground.AddTransition( tr_skill3 );
        st_air.AddTransition( tr_land );
        st_air.AddTransition( tr_skill1 );
        st_air.AddTransition( tr_skill3 );

        st_skill1.AddTransition( new FromOneShotAnim( st_ground, "skills" ) );
        st_skill3.AddTransition( new FromOneShotAnim( st_ground, "skills" ) );

        _fsm = new FSM( this , st_ground );

       // Attributes
        Attributes.Level = 0;
        Attributes.RunSpeed = 8;
        Attributes.JumpHeight = 4;
        Attributes.MaxHealth = 100;
        Attributes.Health = Attributes.MaxHealth;
        Attributes.Damage = 10;
        Attributes.CritChance = 0.1f;
        Attributes.Experience = 0.0f;
        Attributes.NextLevelExperience = 100;
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

    public void PlayAnimation( )
    {

    }

}
