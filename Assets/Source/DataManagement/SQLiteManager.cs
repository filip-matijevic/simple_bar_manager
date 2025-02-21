using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;

public class SQLiteManager : MonoBehaviour
{
    private string dbPath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "Database.db");
        CreateDatabase();
        InsertLabel(new SQLabel("Test", "FF00FFFF"));
        GetAllLabels();

    }

    private void CreateDatabase()
    {
        using (var connection = new SqliteConnection("URI=file:" + dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Labels (
                    id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    name TEXT NOT NULL, 
                    hexColor TEXT NOT NULL);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    // Update is called once per frame
    public void InsertLabel(SQLabel label)
    {
        using (var connection = new SqliteConnection("URI=file:" + dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO Labels (name, hexColor) VALUES (@name, @hexColor);";
                command.Parameters.AddWithValue("@name", label.Name);
                command.Parameters.AddWithValue("@hexColor", label.HexColor);
                command.ExecuteNonQuery();
            }
        }
    }

    private void GetAllLabels()
    {
        using (var connection = new SqliteConnection("URI=file:" + dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Labels;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SQLabel label = new SQLabel(
                            reader.GetInt32(0),  // Id (auto-generated)
                            reader.GetString(1), // Name
                            reader.GetString(2)  // HexCode
                        );
                        Debug.Log(label);
                    }
                }
            }
            connection.Close();
        }
    }
}

    public class SQLabel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HexColor { get; set; }

        public SQLabel(int id, string name, string hexColor)
        {
            Id = id;
            Name = name;
            HexColor = hexColor;
        }
        public SQLabel(string name, string hexColor)
        {
            Id = -1;
            Name = name;
            HexColor = hexColor;
        }

        public override string ToString()
        {
            return $"Label: [Id={Id}, Name={Name}, HexCode={HexColor}]";
        }
    }
