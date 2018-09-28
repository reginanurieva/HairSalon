using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class SpecialtiesController : Controller
  {
    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View(allSpecialties);
    }

    [HttpGet("/specialties/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/specialties")]
    public ActionResult Create()
    {
      Specialty newSpecialty = new Specialty(Request.Form["description"], (Int32.Parse(Request.Form["stylistId"])));
      newSpecialty.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{stylistId}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);
      List<Stylist> listStylists = selectedSpecialty.GetStylists();
      model.Add("selectedSpecialty", selectedSpecialty);
      model.Add("listStylists", listStylists);
      return View(model);

    }
  }
}
