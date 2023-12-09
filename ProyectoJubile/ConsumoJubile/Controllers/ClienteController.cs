using ConsumoJubile.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ConsumoJubile.Controllers
{
    public class ClienteController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44345/api");
        private readonly HttpClient _client;

        public ClienteController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult IndexAsync()
        {
            List<ClienteViewModel> clientList = new List<ClienteViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/Get").Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                clientList = JsonConvert.DeserializeObject<List<ClienteViewModel>>(data);
            }
            return View(clientList);
        }

        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult CreateAsync(ClienteViewModel model) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/cliente/Post", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Cliente Creado.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditarAsync(int id)
        {
            try
            {
                ClienteViewModel client = new ClienteViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/Get" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    client = JsonConvert.DeserializeObject<ClienteViewModel>(data);
                }
                return View(client);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        public IActionResult EditarAsync(ClienteViewModel model) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/cliente/Put", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Cliente ha sido actualizado.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAsync(int id) 
        {
            try
            {
                ClienteViewModel client = new ClienteViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/Get/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    client = JsonConvert.DeserializeObject<ClienteViewModel>(data);
                }
                return View(client);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmAsync(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/cliente/Delete/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Cliente ha sido eliminado.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

    }
}
