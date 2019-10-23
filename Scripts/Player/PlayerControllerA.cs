using Godot;
using System;

public class PlayerControllerA : KinematicBody2D
{
    [Export] float gravity;
    [Export] float gravityInterp;
    [Export] float mass;
    [Export] float movementSpeed;
    [Export] float movementInterp;
    [Export] float jumpTime;
    float jumpTimer;
    bool canJump;
    [Export] float jumpSpeed;
    [Export] float jumpInterp;
    [Export(PropertyHint.Range, "-10, 10")] float airControl;
    [Export] bool continuousJumping;
    float x;
    float y;

    public override void _Process(float delta)
    {
        //count down jumpTimer
        jumpTimer -= delta;
    }

    public override void _PhysicsProcess(float delta)
    {
        MovementFunctionality(delta);
    }

    void MovementFunctionality(float delta)
    {
        //get input direction
        Vector2 moveDir = new Vector2(Input.GetActionStrength("right")-Input.GetActionStrength("left"), -Input.GetActionStrength("ui_accept"));
        Vector2 movement = new Vector2();
        y = Mathf.Lerp(y, -mass*gravity, delta*gravityInterp);
        movement.y = y;
        //check for jump release
        if(Input.IsActionJustReleased("ui_accept"))
            canJump = false;
            
        if(canJump && jumpTimer > 0.0f && Mathf.Abs(moveDir.y) > 0.0f)
        {
            //jump
            //ceiling detecion
            if(IsOnCeiling())
            {
                canJump = false;
                return;
            }
            //interpolate jump axes
            y = Mathf.Lerp(y, moveDir.y*jumpSpeed, delta*jumpInterp);
            //apply jump axes
            movement.y = y;
        }
        else if(IsOnFloor())
        {
            //set y motion to 0
            y = 0.0f;
            movement.y = y;
            //reset jumpTimer
            jumpTimer = jumpTime;
            //allow jumping
            if(continuousJumping)
                canJump = true;
            else if(moveDir.y == 0.0f)
                canJump = true;
            else
                canJump = false;
        }
        GD.Print(canJump);
        //interpolate movement axes
        x = Mathf.Lerp(x, moveDir.x*movementSpeed*(IsOnFloor()?1.0f:airControl), delta*movementInterp);
        movement.x = x;
        //move the player
        MoveAndSlide(movement, new Vector2(0.0f, -1.0f));
        //reset scene
        if(Position.y >= 650.0f)
        {
            GetTree().ReloadCurrentScene();
        }
    }
}
