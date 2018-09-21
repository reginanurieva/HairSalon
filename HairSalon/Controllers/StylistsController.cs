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
      // Client newClient = Stylist.GetClients();
      // Client newClient = Client.GetClients(id);
      //model.Add("stylist", selectedStylist);
      //model.Add("clients", newClient);
      // List<Item> categoryItems = selectedCategory.GetItems();
      // model.Add("category",selectedCategory);
      // model.Add("items",categoryItems);
      //return View(selectedStylist);
    }
  }
}
