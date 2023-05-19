using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PasswordManagerWEBAPP.Data.Repositories;
using PasswordManagerWEBAPP.Models;

namespace PasswordManagerWEBAPP.Controllers;

public class PasswordManagerController : Controller
{
    private readonly ILogger<PasswordManagerController> _logger;
    private readonly IPasswordmngrsRepository _passwordmngrsRepository;

    public PasswordManagerController(ILogger<PasswordManagerController> logger, IPasswordmngrsRepository passwordmngrsRepository)
    {
        _logger = logger;
        _passwordmngrsRepository = passwordmngrsRepository;
    }

    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JWToken");
        var viewModel = new IndexViewModel
        {
            Passwordmngrs = await _passwordmngrsRepository.GetAllPasswordmngrs(token)
        };

        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Passwordmngr newPasswordmngr)
    {
        newPasswordmngr.UserId = 1;
        //newPasswordmngr.Accountr = "";
        //newPasswordmngr.Passwordr = "";
        //newPasswordmngr.Description = "";

        await _passwordmngrsRepository.CreatePasswordmngr(newPasswordmngr);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int passwordmngrId)
    {
        var passwordmngr = await _passwordmngrsRepository.GetPasswordmngrById(passwordmngrId);

        if (passwordmngr is null)
            return NotFound();

        return View(passwordmngr);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Passwordmngr updatedPasswordmngr)
    {
        var test = await _passwordmngrsRepository.UpdatePasswordmngr(updatedPasswordmngr.Id, updatedPasswordmngr);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int passwordmngrId, string token)
    {
        await _passwordmngrsRepository.DeletePasswordmngr(passwordmngrId, token);
        return RedirectToAction("Index");
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
