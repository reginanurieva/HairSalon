using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create()
    {
      Stylist newStylist = new Stylist(Request.Form["stylistName"]);
      newStylist.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClient();
      model.Add("Stylist", selectedStylist);
      model.Add("Client", stylistClients);
      return View(model);
    }


  [HttpPost("/stylists/{id}")]
  public ActionResult AddSpecialty(int stylistId)
  {
  Stylist stylist = Stylist.Find(stylistId);
  Specialty specialty = Specialty.Find(Int32.Parse(Request.Form["specialty-id"]));
  stylist.AddSpecialty(specialty);
  return RedirectToAction("Index");
    }


    [HttpGet("/stylists/{stylistId}/update")]
    public ActionResult UpdateForm(int stylistId)
    {
      Stylist thisStylist = Stylist.Find(stylistId);
      return View("UpdateForm", thisStylist);
    }

    [HttpPost("/stylists/{stylistId}/update")]
    public ActionResult Update(int stylistId)
    {
      Stylist thisStylist = Stylist.Find(stylistId);
      thisStylist.Update(Request.Form["newName"]);
      return RedirectToAction("Index");
    }
  }
}
