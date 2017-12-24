using Godot;
using System;

public class EnemyHealthbar : TextureProgress
{
    BaseEnemy _owner;
    float _visible_duration = 3;
    float _timer = 0;

    float _target_value = 1;
    TextureProgress _cover;
    const int HEIGHT_OFFSET = 128;

    public override void _Ready()
    {
        _owner = GetParent() as BaseEnemy;
        _cover = GetNode( "cover" ) as TextureProgress;

        _owner.HealthChangedEvent += UpdateValue;
        _owner.OwnDeathEvent += QueueFree;
        SetValue( 1 );
        Hide();
    }

    public override void _Process(float delta)
    {
        _timer += delta;
        if ( _timer > _visible_duration )
        {
            Hide();
            SetProcess( false );   
        }
        Reposition();
        SetValue( MathUtils.Lerp( GetValue(), _target_value, delta * 5 ) );           
    }

    public void UpdateValue( float current, float max )
    {
        Reposition();
        Show();
        SetProcess( true );
        float value = current / max;
        _cover.SetValue( value );
        _timer = 0;
        _target_value = value;
    }

    void Reposition()
    {
        Vector2 pos = GetViewport().GetCamera().UnprojectPosition( _owner.GetTranslation() );
        pos.y -= HEIGHT_OFFSET;
        pos.x -= GetSize().x / 2;
        SetPosition( pos );        
    }
}
