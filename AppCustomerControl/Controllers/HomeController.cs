﻿using AppCustomerControl.Models;
using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace AppCustomerControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public INotyfService _notifyService { get; }
        


        public static string MyProperty { get; set; }
        public HomeController(ILogger<HomeController> logger, INotyfService notifyService)
        {
            _logger = logger;
            _notifyService = notifyService;
        }
       

        public IActionResult Index()
        {
            var valorAtualLogin = Parametros.login_usuario;
            if (!string.IsNullOrEmpty(valorAtualLogin))
            {
                ViewBag.NomeUsuario = valorAtualLogin;
            }
            return View();
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

        [HttpGet]
        public async Task<IActionResult> ModalLogin()
        {
            
            return PartialView("_ModalLoginUsuario");
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dashboard(User modelUser)
        {
           
            List<User> listUser = new List<User>();
            var user1 = new User() { id = 1, login = "728.495.780-88", nome = "Fokubara", password = "123456" };
            var user2 = new User() { id = 2, login = "484.671.460-87", nome = "Norberto", password = "123456" };
            listUser.Add(user1);
            listUser.Add(user2);

            var pesquisaUsuario = listUser.Where(x => x.login == modelUser.login).FirstOrDefault();
            if (pesquisaUsuario == null)
                return BadRequest("Usuário não encontrado" + " 😭");

            if (pesquisaUsuario.password != modelUser.password)
                return BadRequest("Senha não existe" + " 😭");

            var user = new UsuarioAtual()
            {
                id_usuario = 1,
                login_usuario = "maycon"
            };

            Parametros.atualiza(user);

            var teste1 = Parametros.login_usuario;
            //var nomeUsuarioLogado = pesquisaUsuario.nome;
            // ViewBag.NomeUsuario = "aaaaaaaaaaaa";
            //_notifyService.Success("Bem vindo ao sistema, aproveite todas funcionalidades!!");
            //return RedirectToAction(nameof(DashBoardPrincipal));
            return RedirectToAction("Index");
            // return View(user);
          
        }

        public IActionResult Sair()
        {
            var user = new UsuarioAtual()
            {
                id_usuario = 0,
                login_usuario = null
            };
            Parametros.atualiza(user);
            return RedirectToAction("Index");
        }
    }
}