using SQLite;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;

using Todo.Contracts;
using Todo.Models;

namespace Todo.Data
{
	public class TodoItemDatabase
	{
		private static object locker = new object();
		private SQLiteConnection database;

		public TodoItemDatabase()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
			database.CreateTable<TodoItem>();
		}

		public IEnumerable<TodoItem> GetItems()
		{ 
			lock(locker) {
				return (from i in database.Table<TodoItem>() select i).ToList();
			}
		}

		public IEnumerable<TodoItem> GetItemNotDone()
		{ 
			lock(locker) {
				return database.Query<TodoItem>("SELECT * FROM (TodoItem) WHERE [Done] = 0");
			}
		}

		public int SaveItem(TodoItem item)
		{ 
			lock(locker) {
				if (item.ID != 0){
					database.Update(item);
					return item.ID;
				}
				else {
					return database.Insert(item);
				}
			}
		}

		public TodoItem GetItem(int id) 
		{
			lock(locker) {
				return database.Table<TodoItem>().FirstOrDefault(x => x.ID == id);
			}
		}

		public int DeleteItem(int id) 
		{
			lock(locker) {
				return database.Delete<TodoItem>(id);
			}
		}
	}
}