using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=regina_nurieva_test;";
    }
    public void Dispose()
    {
      Stylist.DeleteAll();
    }

    [TestMethod]
     public void GetAll_EmptyDB_0()
     {
       //Arrange
       int result = Stylist.GetAll().Count;

       //Assert
       Assert.AreEqual(0, result);
     }

     [TestMethod]
     public void Save_SavesToDB_StylistList()
     {
       //Arrange
       Stylist testStylist = new Stylist("Ryan", 1);

       //Act
       testStylist.Save();
       List<Stylist> result = Stylist.GetAll();
       List<Stylist> testList = new List<Stylist>{testStylist};

     }


     [TestMethod]
      public void Find_FindsStylistInDB_Stylist()
      {
          //Arrange
          Stylist testStylist = new Stylist("Chan", 0);
          testStylist.Save();

          //Act
          Stylist foundStylist = Stylist.Find(testStylist.GetId());

          //Assert
          Assert.AreEqual(testStylist.GetName(), foundStylist.GetName());
      }


     [TestMethod]
    public void Edit_UpdatesStylistInDB_String()
    {
        //Arrange
        string firstStylist = "Jack";
        Stylist testStylist = new Stylist (firstStylist, 1);
        testStylist.Save();
        string secondStylist = "Mark";

        //Act
        testStylist.Update(secondStylist);

        string result = Stylist.Find(testStylist.GetId()).GetName();

        //Assert
        Assert.AreEqual(secondStylist, result);
    }

  }
}
