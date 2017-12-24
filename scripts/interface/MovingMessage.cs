using Godot;
using System;

public class MovingMessage : Label
{
    float _start_height;
    static float _distance      = 48;
    static float _duration      = 0.75f;

    Random _rnd = new Random();

    float _timer = 0.0f;

    Font _font;
    Font Font{
                get { 
                    if ( _font == null )
                        _font = GD.Load("res://fonts/s16.tres") as Font;
                    return _font;
                    }
            }       

    public MovingMessage()
    {
        Deactivate();
        AddFontOverride( "font", Font );
    }

    public void Activate( ) { Show(); SetProcess( true ); }
    public void Deactivate( ) { Hide(); SetProcess( false ); }

    public void Start( string text, Color color, Vector2 position )
    {
        SetText( text );
        SetPosition( position + new Vector2(  (float)_rnd.NextDouble() * 8, (float)_rnd.NextDouble() * 8 ) );
        _start_height = position.y + (float)_rnd.NextDouble() * 8;
        Set( "custom_colors/font_color", color );
        _timer = 0;
        Activate();
    }

    public override void _Process(float delta)
    {
        _timer += delta;
        if ( _timer >= _duration )
            Deactivate();

        Vector2 pos = GetPosition();
        pos.y = MathUtils.Bezier3( _start_height, _start_height - _distance, _start_height - _distance, _timer / _duration );
        SetPosition( pos );
    }   
}