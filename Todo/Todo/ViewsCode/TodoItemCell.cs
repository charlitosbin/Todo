using System;

using Xamarin.Forms;

namespace Todo.ViewsCode
{
	public class TodoItemCell : ViewCell
	{
		public TodoItemCell()
		{
			StyleId = "Cell";

			var label = new Label{
				StyleId="CellLabel",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			label.SetBinding(Label.TextProperty, "Name");

			var tick = new Image{
				StyleId = "CellTick",
				Source = ImageSource.FromFile("check"),
				HorizontalOptions = LayoutOptions.End
			};

			tick.SetBinding(VisualElement.IsVisibleProperty, "Done");

			var layout = new StackLayout{
				Padding = new Thickness(20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { label, tick }
			};

			View = layout;
		}
	}
}