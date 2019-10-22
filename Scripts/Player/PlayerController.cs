using Godot;
using System;

public class PlayerController : KinematicBody2D
{
    [Export] float gravity;
    // [Export] float mass;
    [Export] float movementSpeed;
    // [Export] float kbInterp;
    Vector2 dashDirection;
    [Export] float dashSpeed;
    [Export] float dashTime;
    float dashTimer;

    public override void _Process(float delta)
    {
        dashTimer -= delta;
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 dashDir = new Vector2(Input.GetActionStrength("right_c")-Input.GetActionStrength("left_c"), -Input.GetActionStrength("up_c")+Input.GetActionStrength("down_c"));
        Vector2 moveDir = new Vector2(Input.GetActionStrength("right")-Input.GetActionStrength("left"), -Input.GetActionStrength("up")+Input.GetActionStrength("down"));
        if(dashTimer > 0.0f)
        {
            MoveAndSlide(dashDirection*dashSpeed);
            return;
        }
        if(IsOnFloor() && dashDir != Vector2.Zero && dashDir == moveDir)
        {
            dashTimer = dashTime;
            dashDirection = dashDir;
            MoveAndSlide(dashDir*dashSpeed, new Vector2(0.0f, -1.0f));
        }
        else
        {
            MoveAndSlide(new Vector2(moveDir.x, 0.0f)*movementSpeed+new Vector2(0.0f, gravity), new Vector2(0.0f, -1.0f));
        }
    }
}
