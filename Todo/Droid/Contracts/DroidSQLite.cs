using System;
using System.IO;

using Xamarin.Forms;

using Todo.Contracts;
using Todo.Droid.Contracts;
using Android;

[assembly: Dependency(typeof(DroidSQLite))]

namespace Todo.Droid.Contracts
{
	public class DroidSQLite : ISQLite
	{
		public DroidSQLite()
		{
		}

		public global::SQLite.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "TodoSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, sqliteFilename);

			Console.WriteLine(path);

			if (!File.Exists(path)) {
				var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.TodoSQLite);  // RESOURCE NAME ###F
				FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

				//ReadWriteStream(s,writeStream);
			}

			var conn = new global::SQLite.SQLiteConnection(path);

			return conn;
		}

		private void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);

			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}

			readStream.Close();
			writeStream.Close();
		}
	}
}

