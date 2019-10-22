extends KinematicBody2D
export var speed = 200
export var jump_force = 9000
export var gravity = 90
var x = 0	
var y = 0


func _physics_process(delta):
	var dir = Vector2(
		Input.get_action_strength('ui_right')-Input.get_action_strength('ui_left'),
		Input.get_action_strength('ui_select') if is_on_floor() and Input.is_action_just_pressed('ui_select') else 0.0
		).normalized()
		
	x = lerp(x, dir.x*speed, delta*10)
	y = lerp(y, dir.y*jump_force+gravity, delta*10)
	
	move_and_slide(Vector2(x,y), Vector2.UP)
	
	