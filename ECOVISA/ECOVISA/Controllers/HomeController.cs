﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;

namespace ECOVISA.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class HomeController : Controller
    {
        public ActionResult Inicio()
        {
            if (Session["TARGET_PAGE"] == null) 
            {
                Session["TARGET_PAGE"] = "/Home/Index";
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult Index()
        {
            Session["TARGET_PAGE"] = "/Home/Index";
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult About()
        {
            ViewBag.Message = "Your contact page.";
            Session["TARGET_PAGE"] = "/Home/About";
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            Session["TARGET_PAGE"] = "/Home/Contact";
            return PartialView();
        }

        public ActionResult CerrarSesion()
        {
            Session["SesionUsuario"] = null;
            Session["TARGET_PAGE"] = null;
            return RedirectToAction("Login", "Acceso");
        }
    }
}