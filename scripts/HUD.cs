using Godot;
using System;
using System.Collections.Generic;

public class HUD : Control
{

    TextureProgress _healthbar_progress;
    Label           _healthbar_label;

    TextureProgress _experiencebar_progress;
    Label           _level_label;

    Label           _coins_label;

    Label[]         _skill_labels;

    //Dictionary<ItemID,Label> _items = new Dictionary<ItemID,Label>();

    public override void _Ready()
    {
        _healthbar_progress = (TextureProgress)FindNode( "healthbar_progress" );
        _healthbar_label = (Label)FindNode( "healthbar_label" );

        _experiencebar_progress = (TextureProgress)FindNode( "experiencebar" );

        _level_label = (Label)FindNode( "level_label" );

        _coins_label = (Label)GetNode( "coins_label" );

        Label l1 = (Label)FindNode( "skill1_label" );
        Label l2 = (Label)FindNode( "skill2_label" );
        Label l3 = (Label)FindNode( "skill3_label" );
        Label l4 = (Label)FindNode( "skill4_label" );
        _skill_labels = new Label[4]{ l1, l2, l3, l4 };
        foreach ( Label l in _skill_labels ) 
            l.Hide();

        BasePlayer player = (BasePlayer)GetTree().GetNodesInGroup( "player" )[0]; // probably should add a check for this

        UnitAttributes a = player.Attributes;
        OnHealthChanged( a.Health, a.MaxHealth );
        OnExperienceUp( a.Experience, a.NextLevelExperience );
        OnLevelUp( a.Level );
        OnCoinsChanged( a.Coins );

        player.LevelChangedEvent += OnLevelUp;
        player.ExperienceChangedEvent += OnExperienceUp;
        player.HealthChangedEvent += OnHealthChanged;
        player.CoinsChangedEvent += OnCoinsChanged;
        player.SkillCooldownChangedEvent += OnSkillCooldownChanged;
        //player.ItemCollectedEvent += OnItemCollectedEvent;
    }

    void OnExperienceUp( float current_exp , float all_exp )
    {
        _experiencebar_progress.SetValue( current_exp / all_exp );
    }

    void OnLevelUp( int new_level )
    {
        _level_label.SetText( String.Format( "{0}", new_level ) );
    }

    void OnHealthChanged( float current_health , float max_health )
    {
        _healthbar_label.SetText( String.Format( "{0} / {1}", (int)current_health, (int)max_health ) );
        _healthbar_progress.SetValue( current_health / max_health );
    }

    void OnCoinsChanged( int coins )
    {
        _coins_label.SetText( String.Format( "${0}", coins ) );
    }

    void OnSkillCooldownChanged( int index, float time, float duration )
    {
        Label l = _skill_labels[index];
        
        if ( time < 0.1 )
        {
            l.Hide();
            return;
        }

        l.Show();
        l.SetText( String.Format( "{0}", (int)time ) );
    }

    // void OnItemCollectedEvent( item it )
    // {
    //     ItemID item_id = it.GetItemID();

    //     if ( _items.ContainsKey( item_id ) )
    //     {
    //         Label l = _items[item_id];
    //         int n;
    //         int.TryParse( l.GetText() , out n );
    //         n++;
    //         l.SetText( n.ToString() );
    //         l.Show();
    //     }
    //     else
    //     {
    //         GridContainer cont = (GridContainer)GetNode( "item_container" );
    //         TextureRect tr = new TextureRect();
    //         tr.SetTexture( it.GetIcon() );
    //         cont.AddChild( tr );
    //         Label l = new Label( );
    //         l.AddFontOverride( "font", (Font)GD.Load("res://fonts/s16.tres") );
    //         l.SetText( "1" );
    //         l.Hide();
    //         tr.AddChild( l );
    //         _items.Add( item_id, l );
    //     }
    // }
}
