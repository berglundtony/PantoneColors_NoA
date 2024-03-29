﻿using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using PantoneColors_NoA.Functions;
using PantoneColors_NoA.Models;
using PantoneColors_NoA.Models.ViewModels;

namespace PantoneColors_NoA.Controllers
{
    public class ColorsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Root>? colors = new List<Root>();
            Root? result = new();
            var model = new PantoneColors();

            result = await ColorMethods.GetColors(2,1,result);

            if(result != null)
            {
                for (int p = 1; p <= result.total_pages; p++)
                {
                    if (colors != null)
                    {
                        result = await ColorMethods.GetColors(2, p, result);
                        if (result != null)
                            colors.Add(result);
                    }
                }
            }

            model.Group1.OrderBy(o => o.year);
            model.Group2.OrderBy(o => o.year);
            model.Group3.OrderBy(o => o.year);

            model = ColorMethods.SortForListsOfPatoneColors(colors, model);

            return View(model);
        }

    }
}


