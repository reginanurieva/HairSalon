using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    public int id {get; set; }
    public string name {get; set; }
    public int stylist_id {get; set;}

    public Client(string newName, int newStylistId, int Id = 0)
    {
      id = Id;
      name = newName;
      stylist_id = newStylistId;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};

      // List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);

        Client newClient = new Client(clientName, clientStylistId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }


    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@clientName, @clientStylist_Id);";

      MySqlParameter ClientName = new MySqlParameter();
      ClientName.ParameterName = "@clientName";
      ClientName.Value = this.name;
      cmd.Parameters.Add(ClientName);


      MySqlParameter newStylistId = new MySqlParameter();
      newStylistId.ParameterName = "@clientStylist_Id";
      newStylistId.Value = this.stylist_id;
      cmd.Parameters.Add(newStylistId);

      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<Client> Filter(string sortOrder)
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients ORDER BY name " + sortOrder + ";";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int stylist_id = rdr.GetInt32(2);
        Client newClient = new Client(name, stylist_id);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // public override bool Equals(System.Object otherClient)
    // {
    //   if (!(otherClient is Client))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //
    //     Client newClient = (Client) otherClient;
    //     bool idEquality = (this.id == newClient.id);
    //
    //     bool descriptionEquality = (this.name == newClient.name);
    //     return (nameEquality && idEquality);
    //   }
    // }

    public override int GetHashCode()
    {
      return this.name.GetHashCode();
    }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `clients` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int clientId = 0;
      string clientName = "";
      int clientStylistId = 0;

      while (rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        clientStylistId = rdr.GetInt32(2);
      }

      Client foundClient= new Client(clientName, clientStylistId, clientId);  // This line is new!

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundClient;  // This line is new!
    }
  }
}
