using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

using Xamarin.Forms;
using Todo.Models;
using Todo.Contracts;

namespace Todo.ViewsCode
{
	public class BasePage : ContentPage
	{
	}

	public class TodoItemPage : BasePage
	{
		public TodoItemPage()
		{
			this.SetBinding(TitleProperty, "Name");

			NavigationPage.SetHasNavigationBar(this, true);

			var nameLabel = new Label { Text = "Name" };
			var nameEntry = new Entry
			{
				StyleId = "TodoName",
				Placeholder = "Todo action"
			};
			nameEntry.SetBinding(Entry.TextProperty, "Name");

			var notesLabel = new Label { Text = "Notes" };
			var notesEntry = new Entry
			{
				StyleId = "TodoNotes,",
				Placeholder = "More Info"
			};
			notesEntry.SetBinding(Entry.TextProperty, "Notes");

			var doneLabel = new Label { Text = "Done" };
			var doneEntry = new Switch { StyleId = "TodoDone" };
			doneEntry.SetBinding(Switch.IsToggledProperty, "Done");

			var saveButton = CreateButton("Save", Color.Green, 0, Color.White, "TodoSave");
			saveButton.Clicked += SaveButton_Clicked;

			var deleteButton = CreateButton("Delete", Color.Red, 0, Color.White, "TodoDelete");
			deleteButton.Clicked += DeleteButton_Clicked;

			var cancelButton = CreateButton("Cancel", Color.Gray, 0, Color.White, "TodoCancel");
			cancelButton.Clicked += CancelButton_Clicked;

			var speakButton = new Button { Text = "Speak", StyleId = "TodoSpeak" };
			speakButton.Clicked += SpeakButton_Clicked;

			Content = new StackLayout{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry,
					notesLabel, notesEntry,
					doneLabel, doneEntry,
					saveButton, deleteButton, cancelButton,
					speakButton
				}
			};
		}

		private Button CreateButton(string btnText, Color btnBackgroundColor, int btnRadius,
									Color btnTextColor, string btnStyleId)
		{
			return new Button
			{
				Text = btnText,
				BackgroundColor = btnBackgroundColor,
				BorderRadius = btnRadius,
				TextColor = btnTextColor,
				StyleId = btnStyleId
			};
		}

		#region EventHandlers
		private void SaveButton_Clicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Database.SaveItem(todoItem);
			this.Navigation.PopAsync();
		}

		private void DeleteButton_Clicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Database.DeleteItem(todoItem.ID);
			this.Navigation.PopAsync();
		}

		private void CancelButton_Clicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			this.Navigation.PopAsync();
		}

		private void SpeakButton_Clicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			DependencyService.Get<ITextToSpeech>().Speack(todoItem.Name + " " + todoItem.Notes);
		}
		#endregion

	}
}