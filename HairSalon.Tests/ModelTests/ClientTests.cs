using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;


namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=regina_nurieva_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void Save_ItemSavesInDadabase_True()
    {
      //Arange
      Client newClient = new Client("Regina",1);
      newClient.Save();
      //Act
      int id = newClient.id;
      Client anotherClient = Client.Find(id);
      //Assert
      Assert.AreEqual(newClient, anotherClient);
    }
  }
}
