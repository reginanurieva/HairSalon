using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;
using System.Collections.Generic;


namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpGet("stylists/{id}/clients/new")]
    public ActionResult CreateForm(int id)
    {
      Stylist foundStylist = Stylist.Find(id);
      return View(foundStylist);
    }

    [HttpPost("/clients")]
    public ActionResult Create(int stylistId)
    {
      // description = Request.Form("Description");

      Client newClient = new Client(Request.Form["clientsName"],  Int32.Parse(Request.Form["stylistId"]));
      newClient.Save();

      return RedirectToAction("Details", "Stylists", new { id = stylistId});
      // List<Client> allClients = Client.GetAll();
      // return View("Index", allClients);
    }

    [HttpPost("/clients/sorted")]
    public ActionResult Filter()
    {
      List<Client> allClients = Client.Filter(Request.Form["name"]);
      return View("Index", allClients);
    }

    [HttpGet("/clients/{id}")]
    public ActionResult Detail(int id)
    {
      Client newClient = Client.Find(id);

      return View(newClient);
    }

    [HttpGet("/clients/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Client thisClient = Client.Find(id);
      return View(thisClient);
    }

    // [HttpPost("/clients/{id}/update")]
    // public ActionResult Update(int id, string newName)
    // {
    //   Client thisClient = Client.Find(id);
    //   thisClient.Edit(newName);
    //   return RedirectToAction("Index");
    // }
  }
}
