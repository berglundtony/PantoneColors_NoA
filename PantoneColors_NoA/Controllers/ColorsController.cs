using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using PantoneColors_NoA.Models;
using PantoneColors_NoA.Models.ViewModels;

namespace PantoneColors_NoA.Controllers
{
    public class ColorsController : Controller
    {
        //static readonly JsonSerializer _serializer = new JsonSerializer();
        static readonly HttpClient _client = new HttpClient();
       
        public async Task<IActionResult> Index()
        {
            List<Root>? colors = new List<Root>();
            Root? result = new();
            var model = new PantoneColors();

            result = await GetColorPerPage(2, result);
            if(result != null)
            {
                for (int i = 1; i <= result.total_pages; i++)
                {
                    colors.Add(result = await GetColors(2, i, result));  
                }
            }
            else { return NotFound(); }


            foreach (var color in colors)
            {
                model.Colors.Add(new PantoneColor { id = color.data.First().id, name = result.data.First().name, color = color.data.First().color, pantone_value = color.data.First().pantone_value, year = color.data.First().year });
            }

            return View(model);
        }

        private static async Task<Root?> GetColorPerPage(int perpage, Root? result)
        {
            string apiUrl = $"https://reqres.in/api/example?per_page={perpage}";
            result = await GetAPIDataColorResult(result, apiUrl).ConfigureAwait(false);
            return await Task.FromResult(result).ConfigureAwait(false);

        }


        private static async Task<Root?> GetColors(int perpage, int page, Root? result)
        {  
            string apiUrl = $"https://reqres.in/api/example?per_page={perpage}&page={page}";
            result = await GetAPIDataColorResult(result, apiUrl).ConfigureAwait(false);

            return await Task.FromResult(result);

        }
        private static async Task<Root?>  GetAPIDataColorResult(Root? result, string apiUrl)
        {

            using (var httpClient = new HttpClient())
            {
                using (var stream = await _client.GetStreamAsync(apiUrl))
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    HttpResponseMessage response = _client.GetAsync(apiUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<Root?>(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        result = null;
                    }

                }
            }
            return result;
        }
    }
}
