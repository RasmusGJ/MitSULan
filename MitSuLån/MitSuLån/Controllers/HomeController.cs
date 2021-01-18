using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MitSuLån.Models;

namespace MitSuLån.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, Route("beregner")]
        public IActionResult Beregner()
        {
            ViewModel vm = new ViewModel();
            vm.Studie.EffektivKredit = 200.0;
            vm.Tilbagebetaling.Gebyr = 20.0;

            return View(vm);
        }

        [HttpPost, Route("beregner")]
        public IActionResult Beregner(ViewModel vm)
        {
            //UNDER STUDIE
            //Beregner den samlet gæld under studiet
            for (int i = 0; i < vm.SUData.AntalMåneder; i++)
            {
                vm.Studie.SamletLån = (vm.Studie.SamletLån + vm.SUData.MånedligSU) * (1 + vm.SUData.Rente);
            }
            //Beregner effektiv kredit under studiet
            vm.Studie.EffektivKredit =  (vm.SUData.AntalMåneder * vm.SUData.MånedligSU);

            //Beregner gebyr for lån
            vm.Studie.Gebyr = vm.Studie.SamletLån - (vm.SUData.AntalMåneder * vm.SUData.MånedligSU);

            //MELLEM STUDIE OG TILBAGEBETALING
            vm.Mellem.SamletLån = vm.Studie.SamletLån;

            for (int i = 0; i < vm.SUData.MånederFørAfbetaling; i++)
            {
                vm.Mellem.SamletLån = vm.Mellem.SamletLån * (1 + (vm.SUData.Diskonto + vm.SUData.Diskonto));
            }

            //LÅN UNDER TILBAGEBETALING

            vm.Tilbagebetaling.SamletLån = vm.Mellem.SamletLån;
            vm.SamletLån = vm.Tilbagebetaling.SamletLån;

            while (vm.Tilbagebetaling.SamletLån > vm.SUData.MåndeligAfbetaling)
            {
                vm.Tilbagebetaling.SamletLån = (vm.Tilbagebetaling.SamletLån - vm.SUData.MåndeligAfbetaling) * (1 + (vm.SUData.Diskonto + vm.SUData.TillægsRente));
                vm.SUData.AntalTilbageBetalingsMåneder++;
            }

            //Måneders afbetaling: 
            vm.SUData.AntalTilbageBetalingsMåneder++;

            //Total pris på lån: 
            vm.SamletLån = vm.SUData.AntalTilbageBetalingsMåneder * vm.SUData.MåndeligAfbetaling + vm.Tilbagebetaling.SamletLån;

            //Gebyr ved lån: 
            vm.Tilbagebetaling.Gebyr = (vm.SUData.AntalTilbageBetalingsMåneder * vm.SUData.MåndeligAfbetaling + vm.Tilbagebetaling.SamletLån) - (vm.SUData.MånedligSU * vm.SUData.AntalMåneder);

            //Sidste månedsafbetaling:  vm.Tilbagebetaling.SamletLån;

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
