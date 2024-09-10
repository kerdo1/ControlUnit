using MauiPlanets.Views;

namespace MAUI_Planets
{
	public partial class App : Application
	{

		const int WindowWidth = 540;
		const int WindowHeight = 1200;

		//
		public App()
		{
			InitializeComponent();

			MainPage = new StartPage();
		}
	}
}
