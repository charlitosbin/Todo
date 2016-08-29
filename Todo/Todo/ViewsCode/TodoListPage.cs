
using Todo.Models;
using Xamarin.Forms;

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


			layout = new RelativeLayout();
			layout.Children.Add(listView,
								Constraint.Constant(0),
								Constraint.Constant(0),
								Constraint.RelativeToParent((parent) => { return parent.Width; }),
			                    Constraint.RelativeToParent((parent => { return parent.Height;})));
		}

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
		#endregion
	}
}
