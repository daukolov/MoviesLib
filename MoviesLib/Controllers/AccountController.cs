using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportLeagueASPNetCoreTest.ViewModels;

namespace SportLeagueASPNetCoreTest.Controllers
{
    /// <summary>
    /// Регистрация новых пользователей
    /// <para/> Подтверждение захода на сайт пзарегистрированного пользователя
    /// </summary>
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        #region Свойства
        /// <summary>
        /// Регистрация
        /// </summary>
        private readonly UserManager<IdentityUser> userManager;
        /// <summary>
        /// Вход
        /// </summary>
        private readonly SignInManager<IdentityUser> signInManager;
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Запросы регистрации нового пользователя
        /// <summary>
        /// Вход на страницу регистрации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Запрос регистрации
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent:false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion

        #region Выход
        /// <summary>
        /// Пользователь покинул сайт, загружается анонимная страница
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }
        #endregion

        #region Заход на сайт зарегистрированного пользователя

        /// <summary>
        /// Заход на сайт зарегистрированного пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        /// <summary>
        /// Заход на сайт зарегистрированного пользователя
        /// <para/> Source: https://www.youtube.com/watch?v=9d8DXXc71RI
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Ошибка при попытке входа");
            }
            return View(model);
        }
        #endregion
    }
}
