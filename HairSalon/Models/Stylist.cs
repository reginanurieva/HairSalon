using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models

{
  public class Stylist
  {
    private string _name;
    private int _id;


    public Stylist(string name, int id=0)
    {
      _name = name;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }


    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int StylistId = rdr.GetInt32(0);
        string StylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(StylistName, StylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int StylistId = 0;
      string StylistName = "";

      while(rdr.Read())
      {
        StylistId = rdr.GetInt32(0);
        StylistName = rdr.GetString(1);
      }
      Stylist newStylist = new Stylist(StylistName, StylistId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }

    public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Client> GetClient()
        {
          List<Client> allStylistClients = new List<Client> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";

          MySqlParameter stylistId = new MySqlParameter();
          stylistId.ParameterName = "@stylist_id";
          stylistId.Value = this._id;
          cmd.Parameters.Add(stylistId);


          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int clientId = rdr.GetInt32(0);
            string clientName = rdr.GetString(1);
            //string itemDueDate = rdr.GetString(2);
            int clientStylistId = rdr.GetInt32(2);
            Client newClient = new Client(clientName, clientStylistId, clientId);
            allStylistClients.Add(newClient);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allStylistClients;
        }


        public void AddSpecialty(Specialty newSpecialty)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@StylistId";
      stylist_id.Value = newSpecialty.GetId();
      cmd.Parameters.Add(stylist_id);

      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@SpecialtyId";
      specialty_id.Value = _id;
      cmd.Parameters.Add(specialty_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


//     public List<Specialty> GetSpecialties()
// {
// MySqlConnection conn = DB.Connection();
// conn.Open();
// MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
// cmd.CommandText = @"SELECT authors.* FROM books
// JOIN books_authors ON (books.id = books_authors.book_id)
// JOIN authors ON (books_authors.author_id = authors.id)
// WHERE books.id = @BookId;";
//
// MySqlParameter bookIdParameter = new MySqlParameter();
// bookIdParameter.ParameterName = "@BookId";
// bookIdParameter.Value = _id;
// cmd.Parameters.Add(bookIdParameter);
//
// MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
// List<Author> allAuthors = new List<Author>{};
//
// while(rdr.Read())
// {
//   int authorId = rdr.GetInt32(0);
//   string authorName = rdr.GetString(1);
//   Author newAuthor = new Author(authorName, authorId);
//   allAuthors.Add(newAuthor);
// }
// conn.Close();
// if (conn != null)
// {
//   conn.Dispose();
// }
// return allAuthors;
// }
  }
}
