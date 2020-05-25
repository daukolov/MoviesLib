using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportLeagueASPNetCoreTest.Data;
using SportLeagueASPNetCoreTest.Models;
using SportLeagueASPNetCoreTest.ViewModels;

namespace SportLeagueASPNetCoreTest.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly SignInManager<IdentityUser> signInManager;

        public MovieController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            dbContext = context;
            this.signInManager = signInManager;
        }

        #region Добавление фильмов зарегистрированным пользователем
        /// <summary>
        /// Запрос страницы для добавления на сайт нового фильма
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Сохранение нового фильма на сайте
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model).Result;

                FilmInfo movie = new FilmInfo
                {
                    Title = model.Title,
                    Description = model.Description,
                    Year = model.Year,
                    Director = model.Director,
                    Poster = uniqueFileName,
                    User = signInManager.Context.User.Identity.Name
                };
                dbContext.Add(movie);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("index", "home");
            }
            return View(model);
        }

        /// <summary>
        /// Загрузка постера для фильма в бд
        /// <para/> Для коллекции FilmInfo генерируется уникальное имя файла
        /// <para/> Файл сохраняется в таблице Poster
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<string> UploadedFile(MovieViewModel model)
        {
            string uniqueFileName = null;

            if (model.Poster != null)
            {
                byte[] imageArray = null;
                //имя файла для основной коллекции
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Poster.FileName;
                using (var memoryStream = new MemoryStream())
                {
                    model.Poster.CopyTo(memoryStream);
                    //массив байт для сохранения в дб
                    imageArray = memoryStream.ToArray();
                }

                
                PosterInfo newPoster = new PosterInfo
                {
                    Name = uniqueFileName,
                    Image = imageArray
                };

                //добавления данных в дб
                dbContext.Add(newPoster);
                await dbContext.SaveChangesAsync();
            }
            return uniqueFileName;
        }
        #endregion

        /// <summary>
        /// Запрос страницы с фильмами, добавленными на сайт пользователем
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MyList()
        {
            return RedirectToAction("MyMovies", "home", new { signIn = signInManager.Context.User.Identity.Name });
        }

        #region Редактирование информации о фильме
        /// <summary>
        /// Запрос страницы для редактирования на сайт нового фильма
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = dbContext.Movies.SingleOrDefaultAsync(m => m.ID == id).Result;

            if (movie != null)
            {
                MovieViewModel model = new MovieViewModel
                {
                    Title = movie.Title,
                    Description = movie.Description,
                    Year = movie.Year,
                    Director = movie.Director,
                    //Признак что информация требует Изменения
                    Editable = true,
                    //Номер в БД
                    Id = id
                };

                return View("Add", model);
            }
            //фильм в БД не найден (ошибочный id)
            return RedirectToAction("error", NotFound());
        }

        /// <summary>
        /// Запрос страницы для редактирования на сайт нового фильма
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(MovieViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = model.Poster != null ? UploadedFile(model).Result : null;
                
                var result = dbContext.Movies.SingleOrDefault(b => b.ID == model.Id);
                if (result != null)
                {
                    //копирование данных
                    result.Title = model.Title;
                    result.Description = model.Description;
                    result.Year = model.Year;
                    result.Director = model.Director;
                    if (uniqueFileName != null) result.Poster = uniqueFileName;
                    result.User = signInManager.Context.User.Identity.Name;

                    await dbContext.SaveChangesAsync();
                }
                //перенаправление на основную страницу
                return RedirectToAction("index", "home");
            }
            return View(model);
        }
        #endregion
    }
}
