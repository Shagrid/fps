namespace Geekbrains
{
	public class Wall : BaseObjectScene, ISelectObj
	{
		public string GetMessage()
		{
			return Name;
		}
	}
}