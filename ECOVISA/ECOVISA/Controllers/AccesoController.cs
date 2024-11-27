﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using CapaNegocio;
namespace ECOVISA.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(CapaEntidades.clsEntidadUsuario oUsuario)
        {
            try
            {
                if (oUsuario.Usuario == null || oUsuario.Contrasena == null)
                {
                    TempData["SweetAlertTitulo"] = "Error";
                    TempData["SweetAlertMensaje"] = "Completar los campos.";
                    TempData["SweetAlertTipo"] = "error";
                    return View();
                }

                System.Data.DataTable dt = new System.Data.DataTable();
                clsNegocioUsuario cnUsuario = new clsNegocioUsuario();
                cnUsuario.ceUsuario.Usuario = oUsuario.Usuario;
                cnUsuario.ceUsuario.Contrasena = cnUsuario.ConvertirSHA256(oUsuario.Contrasena);
                dt = cnUsuario.cdUsuario.ValidarUsuario(cnUsuario.ceUsuario);
                if (dt.Rows.Count > 0)
                {
                    Session["SesionUsuario"] = dt;
                    return RedirectToAction("Inicio", "Home");
                }
                else
                {
                    //Implementar Alerts
                    TempData["SweetAlertTitulo"] = "Error";
                    TempData["SweetAlertMensaje"] = "No se encontro el usuario ingresado.";
                    TempData["SweetAlertTipo"] = "error"; // You can sset 'success', 'error', 'warning', 'info'
                    return View();
                } 
            }
            catch (Exception e) 
            {
                string strErrorMsg = e.Message;
                TempData["SweetAlertTitulo"] = "Error";
                TempData["SweetAlertMensaje"] = "Ocurri&oacute; un error inesperado. Intentelo más tarde.";
                TempData["SweetAlertTipo"] = "error"; // You can set 'success', 'error', 'warning', 'info'
                return View();
            }
        }

        [HttpPost]
        public string IniciarSesion(string strUsuario, string strContrasena)//(CapaEntidades.clsEntidadUsuario oUsuario)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                clsNegocioUsuario cnUsuario = new clsNegocioUsuario();
                cnUsuario.ceUsuario.Usuario = strUsuario;
                cnUsuario.ceUsuario.Contrasena = cnUsuario.ConvertirSHA256(strContrasena);
                dt = cnUsuario.cdUsuario.ValidarUsuario(cnUsuario.ceUsuario);
                if (dt.Rows.Count > 0)
                {
                    Session["SesionUsuario"] = dt;
                    //return RedirectToAction("Index", "Home");
                    return "Ok";
                }
                else
                {
                    //Implementar Alerts
                    TempData["SweetAlertMessage"] = "No se encontró el usuario ingresado.";
                    TempData["SweetAlertType"] = "error"; // You can sset 'success', 'error', 'warning', 'info'
                    return TempData["SweetAlertMessage"].ToString();
                    //return View();
                }
            }
            catch (Exception e)
            {
                TempData["SweetAlertMessage"] = "Completar los campos";
                TempData["SweetAlertType"] = "error"; // You can set 'success', 'error', 'warning', 'info'
                return TempData["SweetAlertMessage"].ToString();
            }
        }
    }
}