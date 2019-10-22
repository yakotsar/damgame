using Godot;
using System;

public class PlayerControllerA : KinematicBody2D
{
    [Export] float gravity;
    [Export] float mass;
    [Export] float movementSpeed;
    [Export] float movementInterp;
    [Export] float jumpTime;
    float jumpTimer;
    bool canJump;
    [Export] float jumpSpeed;
    [Export] float jumpInterp;
    float x;
    float y;

    public override void _Process(float delta)
    {
        //count down jumpTimer
        jumpTimer -= delta;
    }

    public override void _PhysicsProcess(float delta)
    {
        //get input direction
        Vector2 moveDir = new Vector2(Input.GetActionStrength("right")-Input.GetActionStrength("left"), -Input.GetActionStrength("ui_accept"));
        Vector2 movement = new Vector2();
        movement.y = -mass*gravity;
        //interpolate jump axes
        y = Mathf.Lerp(y, moveDir.y*jumpSpeed, delta*jumpInterp);
        if(canJump && jumpTimer > 0 && moveDir.y < 0.0f)
        {
            //jump
            movement.y = y;
            //check for jump release
            if(Input.IsActionJustReleased("ui_accept"))
                canJump = false;
        }
        else if(IsOnFloor())
        {
            //reset jumpTimer
            jumpTimer = jumpTime;
            //allow jumping
            if(!canJump)
                canJump = true;
        }
        //interpolate movement axes
        x = Mathf.Lerp(x, moveDir.x*movementSpeed, delta*movementInterp);
        movement.x = x;
        //move the player
        MoveAndSlide(movement, new Vector2(0.0f, -1.0f));
    }
}
