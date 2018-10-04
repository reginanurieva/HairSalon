using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;


namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=regina_nurieva_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
      Specialty.DeleteAll();
    }

    [TestMethod]
     public void Equals_ReturnsTrueIfDescriptionTheSame_Specialty()
     {
       // Arrange, Act
       Specialty firstSpecialty = new Specialty("Color");
       Specialty secondSpecialty = new Specialty("Color");

       // Assert
       Assert.AreEqual(firstSpecialty, secondSpecialty);
     }

     [TestMethod]
     public void GetAll_DbStartsEmpty_0()
     {
       //Arrange
       //Act
       int result = Specialty.GetAll().Count;

       //Assert
       Assert.AreEqual(0, result);
     }

    [TestMethod]
      public void Save_SavesToDatabase_Specialty()
      {
        //Arrange
        Specialty testSpecialty = new Specialty("Color");

        //Act
        testSpecialty.Save();
        List<Specialty> result = Specialty.GetAll();
        List<Specialty> testList = new List<Specialty>{testSpecialty};

        //Assert
      }

      [TestMethod]
      public void Find_FindsSpecialtyInDatabase_Specialty()
      {
          //Arrange
          Specialty testSpecialty = new Specialty("Color");
          testSpecialty.Save();

          //Act
          Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());

          //Assert
          Assert.AreEqual(testSpecialty, foundSpecialty);
      }


  }
}
