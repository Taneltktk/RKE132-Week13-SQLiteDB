

using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Xml.Linq;

//ReadData(CreateConnection());
InsertCustomer(CreateConnection());
//CreateConnection();
//RemoveCustomer(CreateConnection());
//FindCustomer(CreateConnection());
//DisplayProduct(CreateConnection());
//DisplayProductWithCategory(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source = mydb.db; Version = 3; New = True; Compress = True;"); // Muutsin andmebaasi ära test 13 lahenduste jaoks, muuda tagasi.

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }

    return connection;

}

SQLiteConnection connection = new SQLiteConnection("Data Source = mydb.db; Version = 3; New = True; Compress = True;");

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString(); //string readerRowid = reader.GetString(0);
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);
        Console.WriteLine($"{readerRowid}. Full name: {readerStringFirstName} {readerStringLastName}; Date of birth: {readerStringDoB}");

    }

    myConnection.Close();
}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, dob;
    Console.WriteLine("Enter firstname:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter lastname;");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yy);");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer (firstName, lastName, dateOfBirth) " +
        $"VALUES ('{fName}', '{lName}', '{dob}')";
    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");

    //myConnection.Close(); - see siia ei sobi, sest katkestab ühenduse ja ajab koodi errorisse, aga andmed siiski salvestatakse andmebaasi

    ReadData(myConnection);

}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

    ReadData(myConnection);


}



