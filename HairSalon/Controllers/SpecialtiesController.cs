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
      Specialty newSpecialty = new Specialty(Request.Form["description"]);
      newSpecialty.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);
      List<Stylist> listStylists = Stylist.GetAll();
      List<Stylist> currentStylist = selectedSpecialty.GetStylists();
      model.Add("selectedSpecialty", selectedSpecialty);
      model.Add("listStylists", listStylists);
      model.Add("currentStylist", currentStylist);
      return View(model);

    }

    [HttpPost("/specialties/{id}")]
    public ActionResult Details(int id, int StylistId)
    {
      Specialty selectedSpecialty = Specialty.Find(id);
      Stylist foundStylist = Stylist.Find(StylistId);
      selectedSpecialty.AddStylist(foundStylist);
      return RedirectToAction("Details", new {id=id});

    }

    [HttpGet("/specialties/{specialtyId}/delete")]
    public ActionResult DeleteOne(int specialtyId)
    {
      Specialty thisSpecialty = Specialty.Find(specialtyId);
      thisSpecialty.Delete();
      return RedirectToAction("Index");
    }
  }
}
