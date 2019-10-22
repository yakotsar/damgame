using Godot;
using System;

public class PlayerController : KinematicBody2D
{
    [Export] float gravity;
    [Export] float mass;
    [Export] float movementSpeed;
    [Export] float dashForce;
    [Export] float kbInterp;
    [Export] float gravityInterp;
    float x;
    float y;

    public override void _PhysicsProcess(float delta)
    {
        Vector2 jumpDir = new Vector2(Input.GetActionStrength("left_c")-Input.GetActionStrength("right_c"), -Input.GetActionStrength("up_c")).Normalized();
        Vector2 dir = new Vector2(Input.GetActionStrength("right")-Input.GetActionStrength("left"), Input.GetActionStrength("up"));
        if(IsOnFloor() && jumpDir != Vector2.Zero)
        {
            x = Mathf.Lerp(x, dir.y*((jumpDir.x*dashForce)*(mass*gravity)), delta*gravityInterp);
            y = Mathf.Lerp(y, dir.y*((jumpDir.y*dashForce)*(mass*gravity)), delta*gravityInterp);
            MoveAndSlide(new Vector2(x, -y), new Vector2(0.0f, -1.0f));
            GD.Print(jumpDir.y);
        }
        else
        {
            x = Mathf.Lerp(x, (dir.x*movementSpeed)/mass, delta*kbInterp);
            y = Mathf.Lerp(y, mass*gravity, delta*gravityInterp);
            MoveAndSlide(new Vector2(x, -y), new Vector2(0.0f, -1.0f));
        }
    }
}
