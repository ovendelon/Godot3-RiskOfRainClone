using Godot;
using System;

public class FollowCamera : Camera
{
    [Export]
    public NodePath FollowNode;

    [Export]
    public float FollowSpeed = 2;

    Spatial _follow;
    Vector3 _offset;

    public override void _Ready()
    {
        _offset = GetTranslation();
        _follow = (Spatial)GetNode( FollowNode );
    }

   public override void _Process(float delta)
   {
        if ( _follow == null )
            return;
        Vector3 target_pos = _follow.GetTranslation() + _offset;
        SetTranslation( GetTranslation().LinearInterpolate( target_pos, delta * FollowSpeed ) );
   }
}
