using Newtonsoft.Json.Linq;
using PruebaTecnica.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Index(string NombreInput, string PaisInput, string NumberInput, string emailInput)
        {
            PruebaTecnicaEntities1 BDO = new PruebaTecnicaEntities1();
            Formulario formContactos = new Formulario();

            formContactos.Nombre = NombreInput;
            formContactos.Pais = PaisInput;
            formContactos.Numero = NumberInput;
            formContactos.Email = emailInput;

            BDO.Formulario.Add(formContactos);

            BDO.SaveChanges();

            JObject Pais = await Paises();
            List<String> ListaPaises = new List<string>();
            foreach (JProperty item in Pais.Properties())
            {
                ListaPaises.Add(item.Value.ToString());
            }
            ListaPaises.Sort();
            return View(ListaPaises);
        }

        public async Task<JObject> Paises()
        {
            JObject Paises = new JObject();
            try
            {
                var api = new HttpClient();
                var jsonapi = await api.GetStringAsync("http://country.io/names.json");
                Paises = JObject.Parse(jsonapi);
            }
            catch (Exception e)
            {

            }
            return Paises;
        }
        

        public async Task<ActionResult> Index()
        {
            JObject Pais = await Paises();
            List<String> ListaPaises = new List<string>();
            foreach (JProperty item in Pais.Properties())
            {
                ListaPaises.Add(item.Value.ToString());
            }
            ListaPaises.Sort();
            return View(ListaPaises);
        }

        public ActionResult About()
        {
            PruebaTecnicaEntities1 BDO = new PruebaTecnicaEntities1();
            List<Formulario> FormContactView = new List<Formulario>();
            FormContactView = BDO.Formulario.ToList();

            ViewBag.Message = "Your application description page.";

            return View(FormContactView);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}