using Godot;
using System;

// MESSAGE POOLING

public class messanger : Control
{
    MovingMessage[] _messages;
    int _next_index = 0;
    const int NUM_MESSAGES = 10;
    static Vector3 offset = new Vector3( 0, 1, 0 );

    public override void _Ready()
    {
        _messages = new MovingMessage[NUM_MESSAGES];
        for ( int i=0; i<NUM_MESSAGES; i++)
        {
            _messages[i] = new MovingMessage(  );
            AddChild( _messages[i] );
        }
    }

    public void Add( string msg, Color color, Vector3 position )
    {
        Vector2 pos = GetViewport().GetCamera().UnprojectPosition( position );
        _messages[_next_index].Start( msg, color, pos );
        _next_index = ( _next_index + 1 ) % _messages.Length;
    }

    public void EnemyDamage( Damage dmg, Vector3 position )
    {
        Color c = new Color( 1, 1, dmg.is_crit ? 0 : 1 );
        Add( string.Format( "{0}", (int)dmg.amount ) , c, position + offset );
    }

    public void PlayerDamage( Damage dmg, Vector3 position )
    {
        float gb = dmg.is_crit ? 0.5f : 0;
        Color c = new Color(  1, gb, gb );
        Add( string.Format( "{0}", (int)dmg.amount ) , c, position + offset );
    }

    public void PlayerHeal( float amount, Vector3 position )
    {
        Color c = new Color( 0, 1, 0 );
        Add( string.Format( "{0}", (int)amount ) , c, position + offset );
    }
}
