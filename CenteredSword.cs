using Godot;

public partial class CenteredSword : Node2D
{
	[Export] public float BaseAngle = -120f;      // начальный угол покоя
	[Export] public float SwingAngle = 60f;     // угол взмаха вперёд
	[Export] public float SwingTime = 0.2f;     // время туда (и обратно)

	private bool isSwinging = false;
	private float timer = 0f;

	public override void _Ready()
	{
		RotationDegrees = BaseAngle;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_attack") && !isSwinging)
		{
			isSwinging = true;
			timer = 0f;
		}

		if (!isSwinging)
			return;

		timer += (float)delta;

		float half = SwingTime;
		float total = SwingTime * 2f;

		if (timer <= half)
		{
			float t = timer / half;
			RotationDegrees = Mathf.Lerp(
				BaseAngle,
				BaseAngle + SwingAngle,
				EaseOut(t)
			);
		}
		else if (timer <= total)
		{
			float t = (timer - half) / half;
			RotationDegrees = Mathf.Lerp(
				BaseAngle + SwingAngle,
				BaseAngle,
				EaseIn(t)
			);
		}
		else
		{
			RotationDegrees = BaseAngle;
			isSwinging = false;
		}
	}

	private float EaseOut(float t) => 1f - Mathf.Pow(1f - t, 3);
	private float EaseIn(float t) => t * t * t;
}
