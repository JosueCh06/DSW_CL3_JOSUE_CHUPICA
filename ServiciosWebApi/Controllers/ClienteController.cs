using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Servicios.Data.Models;

namespace ServiciosWebApi.Controllers
{
    public class ClienteController : ApiController
    {
        Negocios2021_2xEntitieCliente db = new Negocios2021_2xEntitieCliente();

        public IEnumerable<clientes> Get()
        {
            var lista = db.clientes.ToList();
            return lista;
        }

        [HttpGet]
        public clientes Get(string id)
        {
            var objcli = db.clientes.FirstOrDefault(x => x.IdCliente == id);
            return objcli;
        }

        [HttpPost]
        public bool Post(clientes obj)
        {
            db.clientes.Add(obj);
            return db.SaveChanges() > 0;
        }

        [HttpPut]
        public bool Put(clientes obj)
        {
            var cliente = db.clientes.FirstOrDefault(x => x.IdCliente == obj.IdCliente);
            cliente.NomCliente = obj.NomCliente;
            cliente.DirCliente = obj.DirCliente;
            cliente.idpais = obj.idpais;
            cliente.fonoCliente = obj.fonoCliente;
            return db.SaveChanges() > 0;
        }

        [HttpDelete]
        public bool Delete(string idCli)
        {
            var cliente = db.clientes.FirstOrDefault(x => x.IdCliente == idCli);
            db.clientes.Remove(cliente);
            return db.SaveChanges() > 0;
        }

    }
}
