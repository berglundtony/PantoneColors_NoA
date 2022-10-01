using Newtonsoft.Json;
using PantoneColors_NoA.Models;
using PantoneColors_NoA.Models.ViewModels;
using System.Runtime.CompilerServices;

namespace PantoneColors_NoA.Functions
{
    public static class ColorMethods
    {
        static readonly HttpClient _client = new HttpClient();
        internal static PantoneColors SortForListsOfPatoneColors(List<Root> colors, PantoneColors model)
        {
            foreach (var color in colors)
            {
                foreach (var col in color.data)
                {
                    string group = SetGroups(col.pantone_value);

                    if (group == "Group1")
                    {
                        model.Group1.Add(new PantoneColor { id = col.id, name = col.name, color = col.color, pantone_value = col.pantone_value, year = col.year, group = group });
                    }
                    else if (group == "Group2")
                    {
                        model.Group2.Add(new PantoneColor { id = col.id, name = col.name, color = col.color, pantone_value = col.pantone_value, year = col.year, group = group });
                    }
                    else
                    {
                        model.Group3.Add(new PantoneColor { id = col.id, name = col.name, color = col.color, pantone_value = col.pantone_value, year = col.year, group = group });
                    }
                }

            }
            return model;
        }



        internal static string SetGroups(string pantone_value)
        {
            pantone_value = pantone_value.Substring(0, 2);
            int.TryParse(pantone_value, out int num);

            if (num % 3 == 0)
            {
                return "Group1";
            }
            else if (num % 2 == 0)
            {
                return "Group2";
            }
            else
            {
                return "Group3";
            }

        }


        internal static async Task<Root?> GetColors(int perpage, int page, Root? result)
        {
            try
            {
                string apiUrl = $"https://reqres.in/api/example?per_page={perpage}&page={page}";
                result = await GetAPIDataColorResult(result, apiUrl).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return null;
            }
      

            return await Task.FromResult(result).ConfigureAwait(false);

        }
        internal static async Task<Root?> GetAPIDataColorResult(Root? result, string apiUrl)
        {

            using (var httpClient = new HttpClient())
            {
                using (var stream = await _client.GetStreamAsync(apiUrl))
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    HttpResponseMessage response = await _client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        result =  JsonConvert.DeserializeObject<Root?>(await response.Content.ReadAsStringAsync());
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
