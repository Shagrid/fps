namespace Geekbrains
{
	public class PlayerController : BaseController, IExecute
	{
		private readonly IMotor _motor;

		public PlayerController(IMotor motor)
		{
			_motor = motor;
		}

		public void Execute()
		{
			_motor.Move();
		}
	}
}