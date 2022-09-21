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

            if (result != null)
            {
                for (int i = 1; i <= result.total_pages; i++)
                {
                    Root? root = await GetColors(2, i, result) ?? null;
                    if (root != null)
                        colors.Add(item: result = root);
                }
            }
            else { return NotFound(); }


            foreach (var color in colors)
            {
                foreach (var col in color.data)
                {
                    string group = Groups(col.pantone_value);
                    //if(group == "Group1") { }
                    model.Colors.Add(new PantoneColor { id = col.id, name = col.name, color = col.color, pantone_value = col.pantone_value, year = col.year, group = group});
                }

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
        private static async Task<Root?> GetAPIDataColorResult(Root? result, string apiUrl)
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

        private string Groups(string pantone_value)
        {
            pantone_value = pantone_value.Substring(0, 2);
            int.TryParse(pantone_value, out int num);

            if (num % 3 == 0)
            {
                return "Group 1";
            }
            else if (num % 2 == 0)
            {
                return "Group 2";
            }
            else
            {
                return "Group 3";
            }

        }
    }
}

