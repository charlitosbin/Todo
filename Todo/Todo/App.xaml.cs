﻿
using Xamarin.Forms;

using Todo.ViewsCode;
using Todo.Data;

namespace Todo
{
	public partial class App : Application
	{
		private static TodoItemDatabase database;
		public static TodoItemDatabase Database { 
			get {
				if (database == null) {
					database = new TodoItemDatabase();
				}

				return database;
			}
		}

		public App()
		{
			var tp = new TabbedPage();
			tp.Children.Add(new NavigationPage(new TodoListPage()) { Title = "C#", Icon = "csharp" });
			//tp.Children.Add(new NavigationPage(new TodoListXaml()) { Title = "XAML", Icon = "xaml" });

			MainPage = tp;
		}
	}
}