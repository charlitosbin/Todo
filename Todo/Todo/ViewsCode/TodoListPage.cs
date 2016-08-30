
using Xamarin.Forms;

using Todo.Contracts;
using Todo.Models;

namespace Todo.ViewsCode
{
	public class TodoListPage : ContentPage
	{
		private ListView listView;
		Image newImage;
		RelativeLayout layout;

		public TodoListPage()
		{
			Title = "Todo";

			listView = new ListView { StyleId = "TodoList" };
			listView.ItemTemplate = new DataTemplate(typeof(TodoItemCell));

			listView.ItemSelected += ListView_ItemSelected;

			var tap = GetTapGesture();
			newImage = new Image{
				Source = "newitem.png",
				WidthRequest = 40,
				Opacity = 0.8f,
				StyleId = "TodoAdd"
			};
			newImage.GestureRecognizers.Add(tap);


			layout = new RelativeLayout();
			layout.Children.Add(listView,
								Constraint.Constant(0),
								Constraint.Constant(0),
								Constraint.RelativeToParent((parent) => { return parent.Width; }),
			                    Constraint.RelativeToParent((parent => { return parent.Height;})));

			layout.Children.Add(newImage,
								Constraint.RelativeToParent((parent) =>
								{
									return (parent.Width / 2) - 20;
								}),
								Constraint.RelativeToParent((parent) =>
								{
									return parent.Height - 60;
								}));

			Content = layout;
			var tbiAdd = GetToolBarPlusItem();
			var tbiSpeak = GetToolBarChatItem();

			ToolbarItems.Add(tbiAdd);
			ToolbarItems.Add(tbiSpeak);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			listView.ItemsSource = App.Database.GetItems();
		}

		#region Toolbar
		private ToolbarItem GetToolBarPlusItem()
		{
			var tbiAdd = new ToolbarItem("+", "plus.png", () =>{
				var todoItem = new TodoItem();
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
			}, 0, 0);
			tbiAdd.StyleId = "ToolbarAdd";

			return tbiAdd;
		}

		private ToolbarItem GetToolBarChatItem()
		{
			var tbiSpeak = new ToolbarItem("?", "chat.png", () =>{
				var todos = App.Database.GetItemsNotDone();
				var tospeak = string.Empty;
				foreach (var t in todos)
					tospeak += t.Name + " ";
				if (string.IsNullOrEmpty(tospeak))
					tospeak = "there are not tasks to do";

				DependencyService.Get<ITextToSpeech>().Speak("Hello from Xamarin Forms");
			},0,0);
			tbiSpeak.StyleId = "ToolbarSpeak";

			return tbiSpeak;
		}
		#endregion

		#region EventHandlers
		private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			var todoItem = (TodoItem)e.SelectedItem;
			var todoPage = new TodoItemPage();
			todoPage.BindingContext = todoItem;

			await Navigation.PushAsync(todoPage);

			((ListView)sender).SelectedItem = null;
		}

		private TapGestureRecognizer GetTapGesture()
		{
			return new TapGestureRecognizer(async (View obj) =>
			{
				var todoItem = new TodoItem();
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;

				var b = newImage.Bounds;
				b.Y = b.Y - 50;

				await newImage.LayoutTo(b, 250, Easing.SinIn);
				b.Y = b.Y + 50;
				await newImage.LayoutTo(b, 250, Easing.SinOut);

				await Navigation.PushAsync(todoPage);
			});
		}
		#endregion
	}
}
