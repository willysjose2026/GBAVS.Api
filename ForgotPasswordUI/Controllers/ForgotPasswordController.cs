using EmailService;
using EmailService.Contracts;
using ForgotPasswordUI.Models;
using GbAviationTicketApi.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForgotPasswordUI.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly UserManager<GbavsUser> _userManager;
        private readonly IEmailSender _emailSender;
        private IServer _server;

        public ForgotPasswordController(UserManager<GbavsUser> userManager, 
            IEmailSender emailSender, IServer server)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _server = server;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = "https://localhost:7227" + Url.Action(nameof(ResetPassword), "ForgotPassword", new { Token = token, Email = model.Email });

            var message = new Message(new string[] { user.Email ?? "" }, "GBAVS - Reset password token", callback ?? "no link");
            _emailSender.SendEmail(message);

            return RedirectToAction(nameof(ForgotPasswordConfirmation));

        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                return RedirectToAction(nameof(ResetPasswordConfirmation));

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach(var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View();
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
