using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;

public class SQLiteManager : MonoBehaviour
{
    public static string dbPath;
    private static IDbConnection dbConnection;

    public static LabelTable Labels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dbPath = "URI=file:" + Path.Combine(Application.persistentDataPath, "Database.db");
        OpenDatabase();

        Labels = new LabelTable(dbConnection);
    }

    void OnApplicationQuit()
    {
        CloseDatabase();   
    }

    private void OpenDatabase(){
        if (dbConnection == null){
            dbConnection = new SqliteConnection(dbPath);
            dbConnection.Open();
        }
    }

    private void CloseDatabase(){
        if (dbConnection != null){
            dbConnection.Close();
            dbConnection = null;
        }
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
}
