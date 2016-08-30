using System;
using System.IO;

using Xamarin.Forms;

using Todo.Contracts;
using Todo.iOS.Contracts;

[assembly: Dependency(typeof(IOSSQLite))]

namespace Todo.iOS.Contracts
{
	public class IOSSQLite : ISQLite
	{
		public IOSSQLite()
		{
		}

		public global::SQLite.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "TodoSQLite.db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libraryPath = Path.Combine(documentsPath, "..", "Library");
			var path = Path.Combine(libraryPath, sqliteFilename);

			Console.WriteLine(path);

			if (!File.Exists(path)) {
				//File.Copy(sqliteFilename, path);
				File.Create(path);
			}

			var conn = new global::SQLite.SQLiteConnection(path);

			return conn;
		}
	}
}