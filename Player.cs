using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    float Speed;
    [Export]
    float MaxSpeed;
    [Export]
    float JumpSpeed;
    [Export]
    float LowGravity;
    [Export]
    float HighGravity;
    [Export]
    float MaxFallSpeed;

    RayCast2D LeftRay;
    RayCast2D MidRay;
    RayCast2D RightRay;
    bool canJump;

    Vector2 velocity = new Vector2();

    public override void _Ready()
    {
        LeftRay = (RayCast2D)GetNode("LeftRay");
        MidRay = (RayCast2D)GetNode("MidRay");
        RightRay = (RayCast2D)GetNode("RightRay");
    }

    public override void _PhysicsProcess(float delta)
    {
        if(Input.IsActionPressed("Left"))
        {
            if(velocity.x > 0)
                velocity.x = 0;
            velocity.x -= Speed;
        }
        else if(Input.IsActionPressed("Right"))
        {
            if(velocity.x < 0)
                velocity.x = 0;
            velocity.x += Speed;
        }
        else
        {
            if(velocity.x < 0)
                velocity.x += Speed;
            else if(velocity.x > 0)
                velocity.x -= Speed;
            else velocity.x = 0;
        }

        CheckJump();

        if(Input.IsActionPressed("Up"))
        {
            if(canJump)
            {
                velocity.y = -JumpSpeed;
            }
            else
            {
                if(velocity.y < 0)
                {
                    velocity.y += LowGravity;
                }
                else
                {
                    velocity.y += HighGravity;
                }
            }
            
        }
        else
        {
            velocity.y += HighGravity;
        }

        velocity.x = Mathf.Clamp(velocity.x, -MaxSpeed, MaxSpeed);
        if(velocity.y > MaxFallSpeed)
        {
            velocity.y = MaxFallSpeed;
        }
        velocity = MoveAndSlide(velocity);
    }

    private bool CheckJump()
    {
        return true;
    }
}
