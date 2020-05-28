using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportLeagueASPNetCoreTest.Data;
using SportLeagueASPNetCoreTest.Models;
using SportLeagueASPNetCoreTest.ViewModels;

namespace SportLeagueASPNetCoreTest.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
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
                await UpdateMovie(model);

                return RedirectToAction("index", "home");
            }
            return View(model);
        }


        private async Task UpdateMovie(MovieViewModel model)
        {
            string uniquePosterName;
            FilmInfo movie;
            if (model.Editable == true)
            {
                //Если установлен флаг редактирования, то Постер изменяется только при условии что в есть новый постер, иначе оставляем старый
                uniquePosterName = model.Poster != null ? await UploadedFile(model) : null;

                movie = dbContext.Movies.SingleOrDefault(b => b.ID == model.Id);

                if (movie == null) return;

                if (uniquePosterName != null) movie.Poster = uniquePosterName;
            }
            else
            {
                //Доабляется в БД новый фильм
                uniquePosterName = await UploadedFile(model);

                movie = new FilmInfo();
                dbContext.Add(movie);

                movie.Poster = uniquePosterName;
            }

            movie.Title = model.Title;
            movie.Description = model.Description;
            movie.Year = model.Year;
            movie.Director = model.Director;

            movie.User = signInManager.Context.User.Identity.Name;

            await dbContext.SaveChangesAsync();

            return;
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
        [Authorize(policy: "EditMovie")]
        public IActionResult Edit(int moveid, string username)
        {
            var movie = dbContext.Movies.SingleOrDefaultAsync(m => m.ID == moveid).Result;

            if (movie != null && movie.User == username)
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
                    Id = moveid
                };


                return View("Add", model);
            }
            //фильм в БД не найден (ошибочный id)
            return RedirectToAction("error", BadRequest());
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
                    await UpdateMovie(model);
                    //перенаправление на основную страницу
                    return RedirectToAction("index", "home");
            }
            return View("Add", model);
        }
        #endregion
    }
}
