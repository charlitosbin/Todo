using SQLite;

namespace Todo.Contracts
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}