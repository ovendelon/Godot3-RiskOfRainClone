using Godot;
using System;

public class PlayerActivateSkill : Transition
{
    int _skill_index;
    int _skill_key;

    public PlayerActivateSkill( State target_state , int skill_index, int key ) : base( target_state )
    {
        _skill_index = skill_index;
        _skill_key = key;
    }

    public override bool IsTriggered( Godot.Object agent )
    {
        BasePlayer p = agent as BasePlayer;
        bool is_key_pressed = Input.IsKeyPressed( _skill_key );
        bool is_cooldown_complete = p.SkillTimers[_skill_index] == 0;
        return is_key_pressed && is_cooldown_complete;
    }
}