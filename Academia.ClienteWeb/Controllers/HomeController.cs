using Academia.ClienteWeb.Models;
using Academia.Negocio.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Academia.ClienteWeb.Controllers
{
    public class HomeController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        public async Task<ActionResult> Index(int grimorioId=0)
        {
            ViewBag.SelectGrimorios = await ObtenerCatalogoSelect("ObtenerGrimonios");
            ViewBag.SelectAfinidad = await ObtenerCatalogoSelect("ObtenerAfinidad");

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerSolicitudes(int grimorioId)
        {
            List<Registro> model = new List<Registro>();

            try
            {
                HttpResponseMessage response = await client.GetAsync($"http://localhost:5004/api/Academia/ConsultarSolicitudes?identificador={grimorioId}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var consulta = JsonConvert.DeserializeObject<List<Registro>>(responseBody);
                if (consulta != null && consulta.Count > 0)
                {
                    model.AddRange(consulta);
                }
            }
            catch { }

            ViewBag.SelectAfinidad = await ObtenerCatalogoSelect("ObtenerAfinidad");
            ViewBag.selectEstatus = await ObtenerCatalogoSelect("ObtenerEstatus");

            return PartialView("_TablaSolicitudes", model);
        }

        [HttpPost]
        public async Task<ActionResult> GuardarSolicitudes(Registro registro)
        {
            var resultEstatus = new Resultado();
            try
            {
                if (registro.Estatus >= 1)
                {
                    var estatusnuevo = new ActualizaEstatus()
                    {
                        Identificador = registro.Identificador,
                        NuevoValor = registro.Estatus
                    };
                    var contentE = new StringContent(JsonConvert.SerializeObject(estatusnuevo), Encoding.UTF8, "application/json");
                    var responseE = await client.PostAsync($"http://localhost:5004/api/Academia/ActulizarEstatusSolicitud", contentE);

                    if (responseE.IsSuccessStatusCode)
                    {
                        var jsonString = await responseE.Content.ReadAsStringAsync();
                        resultEstatus = JsonConvert.DeserializeObject<Resultado>(jsonString);
                        if (!resultEstatus.Success)
                        {
                            return Json(resultEstatus);
                        }                        
                    }
                }

                var content = new StringContent(JsonConvert.SerializeObject(registro), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"http://localhost:5004/api/Academia/GuardarSolicitud", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Resultado>(jsonString);
                    if (resultEstatus.Success)
                    {
                        result.Mensajes.AddRange(resultEstatus.Mensajes);
                    }

                    return Json(result);
                }
            }
            catch(Exception error)
            { 
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<ActionResult> EliminarSolicitudes(int id)
        {
            try
            {
                var response = await client.PostAsync($"http://localhost:5004/api/Academia/EliminarSolicitud?identificador={id}", null);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Resultado>(jsonString);
                    return Json(result);
                }
            }
            catch { }

            return Json(new { success = false });
        }


        #region select's
        private async Task<List<Catalogo>> ObtenerCatalogoSelect(string nombreAPI)
        {
            var result = new List<Catalogo>();

            try
            {
                HttpResponseMessage response = await client.GetAsync($"http://localhost:5004/api/Academia/{nombreAPI}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var consulta = JsonConvert.DeserializeObject<List<Catalogo>>(responseBody);

                if (consulta != null && consulta.Count > 0)
                {
                    result.AddRange(consulta);
                }
            }
            catch { }

            return result;
        }
        #endregion
    }
}