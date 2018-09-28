using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _description;
    //private int _stylist_id;

    public Specialty(string description, int id=0)
    {
      _id=id;
      _description=description;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetDescription()
    {
      return _description;
    }

    // public int GetStylistId()
    // {
    //   return _stylist_id;
    // }
    //
    // public void SetStylistId(int stylistId)
    // {
    //   _stylist_id = stylistId;
    // }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties (description) VALUES (@description);";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


      public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyDescription = rdr.GetString(1);
        // int stylistId = rdr.GetInt32(2);

        Specialty newSpecialty = new Specialty(specialtyDescription, specialtyId);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }


    public static Specialty Find(int id)
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";

    MySqlParameter searchId = new MySqlParameter();
    searchId.ParameterName = "@searchId";
    searchId.Value = id;
    cmd.Parameters.Add(searchId);

    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    int specialtyId = 0;
    string specialtyDescription = "";

    while(rdr.Read())
    {
    specialtyId = rdr.GetInt32(0);
    specialtyDescription = rdr.GetString(1);
    }
    Specialty newSpecialty = new Specialty(specialtyDescription, specialtyId);
    conn.Close();
    if (conn != null)
  {
    conn.Dispose();
  }

  return newSpecialty;
  }

  public void AddSpecialty(Specialty newSpecialty)
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

    MySqlParameter stylist_id = new MySqlParameter();
    stylist_id.ParameterName = "@AuthorId";
    stylist_id.Value = _id;
    cmd.Parameters.Add(stylist_id);

    MySqlParameter specialty_id = new MySqlParameter();
    specialty_id.ParameterName = "@BookId";
    specialty_id.Value = newSpecialty.GetId();
    cmd.Parameters.Add(specialty_id);

    cmd.ExecuteNonQuery();
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
  }


  public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@StylistId";
      stylist_id.Value = newStylist.GetId();
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


  public List<Stylist> GetStylists()
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT stylists.* FROM specialties
    JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
    JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
    WHERE specialties.id = @SpecialtyId;";

    MySqlParameter specialtyIdParameter = new MySqlParameter();
    specialtyIdParameter.ParameterName = "@SpecialtyId";
    specialtyIdParameter.Value = _id;
    cmd.Parameters.Add(specialtyIdParameter);

    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    List<Stylist> stylists = new List<Stylist>{};

    while(rdr.Read())
    {
      int stylistId = rdr.GetInt32(0);
      string stylistName = rdr.GetString(1);

      Stylist newStylist = new Stylist(stylistName, stylistId);
      stylists.Add(newStylist);
    }
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return stylists;
  }


  }
}
