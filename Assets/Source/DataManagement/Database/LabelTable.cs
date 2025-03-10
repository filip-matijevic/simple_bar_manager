
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Mono.Data.Sqlite;

public class LabelTable
{
    private IDbConnection dbConnection;

    public static Action OnInsert;
    public LabelTable(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public int Insert(Label label)
    {
        var dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = @"INSERT INTO Labels (name, hexColor) VALUES (@name, @hexColor);";
        dbCommand.Parameters.Add(new SqliteParameter("@name", label.Name));
        dbCommand.Parameters.Add(new SqliteParameter("@hexColor", label.HexColor));

        int response = dbCommand.ExecuteNonQuery();

        if (response == 1)
        {
            OnInsert?.Invoke();
        }
        return response;
    }

    public List<Label> GetLabels()
    {
        List<Label> labels = new List<Label>();
        var dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM Labels;";
        using (var reader = dbCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                Label label = new Label(
                    reader.GetInt32(0),  // Id (auto-generated)
                    reader.GetString(1), // Name
                    reader.GetString(2)  // HexCode
                );
                labels.Add(label);
            }
        }
        return labels;
    }

    public void DropLabelTable(){
    using (var connection = new SqliteConnection(SQLiteManager.dbPath))
    {
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM Labels; VACUUM;";
            command.ExecuteNonQuery();
        }
        connection.Close();
    }

    }
}
