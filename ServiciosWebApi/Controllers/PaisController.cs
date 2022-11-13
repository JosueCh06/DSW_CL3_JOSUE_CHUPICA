using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Servicios.Data.Models;

namespace ServiciosWebApi.Controllers
{
    public class PaisController : ApiController
    {
        Negocios2021_2xEntitiePais db = new Negocios2021_2xEntitiePais();

        public IEnumerable<paises> Get()
        {
            var lista = db.paises.ToList();
            return lista;
        }

        [HttpGet]
        public paises Get(string id)
        {
            var paises = db.paises.FirstOrDefault(x => x.Idpais == id);
            return paises;
        }

    }
}
