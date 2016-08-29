
using Xamarin.Forms;

using Todo.ViewsCode;

namespace Todo
{
	public partial class App : Application
	{
		public App()
		{
			var tp = new TabbedPage();
			tp.Children.Add(new NavigationPage(new TodoListPage()) { Title = "C#", Icon = "csharp" });
			//tp.Children.Add(new NavigationPage(new TodoListXaml()) { Title = "XAML", Icon = "xaml" });

			MainPage = tp;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

