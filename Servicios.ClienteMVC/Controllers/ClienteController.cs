using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;
using Servicios.Dominio;

namespace Servicios.ClienteMVC.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            HttpClient clientehttp = new HttpClient();
            clientehttp.BaseAddress = new Uri("https://localhost:44350/");

            var request = clientehttp.GetAsync("api/Cliente").Result;

            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var lista = JsonConvert.DeserializeObject<List<Cliente>>(resultado);
                return View(lista.ToList());

            }
            return View();
        }

        public ActionResult Create()
        {
            HttpClient clientehttp = new HttpClient();
            clientehttp.BaseAddress = new Uri("https://localhost:44350/");

            var request = clientehttp.GetAsync("api/Pais").Result;

            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var lista = JsonConvert.DeserializeObject<List<Pais>>(resultado);
                ViewBag.paises = new SelectList(lista.ToList(), "Idpais", "NombrePais");
                return View(new Cliente());

            }
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult Create(Cliente obj)
        {
            HttpClient clientehttp = new HttpClient();
            clientehttp.BaseAddress = new Uri("https://localhost:44350/");
            var request = clientehttp.PostAsync("api/Cliente", obj, new JsonMediaTypeFormatter()).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var estado = JsonConvert.DeserializeObject<bool>(resultado);

                if (estado == true)
                {
                    return RedirectToAction("Index");
                }
                return View(obj);
            }

            return View(obj);

        }

        public ActionResult Edit(String id)
        {

            HttpClient clientehttp = new HttpClient();
            clientehttp.BaseAddress = new Uri("https://localhost:44350/");
            var request = clientehttp.GetAsync("api/Cliente/" + id).Result;
            if (request.IsSuccessStatusCode)
            {
                var respuesta = request.Content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<Cliente>(respuesta);
                var request2 = clientehttp.GetAsync("api/Pais").Result;
                if (request2.IsSuccessStatusCode)
                {
                    var respuesta2 = request2.Content.ReadAsStringAsync().Result;
                    var resultado2 = JsonConvert.DeserializeObject<List<Pais>>(respuesta2);
                    ViewBag.paises = new SelectList(resultado2.ToList(), "Idpais", "NombrePais");
                    return View(resultado);
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Cliente obj)
        {
            HttpClient clientehttp = new HttpClient();
            clientehttp.BaseAddress = new Uri("https://localhost:44350/");
            var request = clientehttp.PutAsync("api/Cliente", obj, new JsonMediaTypeFormatter()).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var estado = JsonConvert.DeserializeObject<bool>(resultado);
                if (estado)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(obj);
                }
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult Delete(String id)
        {
            HttpClient clientehttp = new HttpClient();
            clientehttp.BaseAddress = new Uri("https://localhost:44350/");
            var request = clientehttp.GetAsync("api/Cliente/" + id).Result;
            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var cliente = JsonConvert.DeserializeObject<Cliente>(resultado);
                return View(cliente);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCliente(String id)
        {
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("https://localhost:44350/");
            var request = httpclient.DeleteAsync("api/Cliente?idCli=" + id).Result;
            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var estado = JsonConvert.DeserializeObject<bool>(resultado);
                if (estado)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}