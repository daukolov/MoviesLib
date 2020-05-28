using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportLeagueASPNetCoreTest.Data;
using SportLeagueASPNetCoreTest.Models;
using SportLeagueASPNetCoreTest.ViewModels;
using SportLeagueASPNetCoreTest.Controllers.Paginate;
using Microsoft.AspNetCore.Identity;

namespace SportLeagueASPNetCoreTest.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }

        #region Запросы - обработка
        /// <summary>
        /// Get запрос главной страницы
        /// <para/>Номер страницы данных
        /// </summary>
        public async Task<IActionResult> Index(int page = 1)
        {
            var dataPage = await Task.Run(() => GetModel(page, 10));

            return View(dataPage);
        }

        /// <summary>
        /// Get запрос главной страницы
        /// <para/>Номер страницы данных
        /// </summary>
        public async Task<IActionResult> MyMovies(string signIn, int page = 1)
        {
            var dataPage = await Task.Run(() => GetModel(signIn, page, 10));

            return View("Index", dataPage);
        }


        /// <summary>
        /// Перенаправление на страницу с ошибкой
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return RedirectToAction("error", NotFound());
        }
        #endregion

        #region Model Бизнес логика получения модели
        /// <summary>
        /// Запрос данных из модели c фильтром по пользователю
        /// <para/>page - страница
        /// <para/>pagesize - количество данных на странице
        /// </summary>
        private PagedResult<HomeViewModel> GetModel(string user, int page, int pagesize)
        {
            //фильтр по user, по нижнему регистру
            var query = dbContext.Movies.Where(m => m.User.ToLower() == user.ToLower());

            //текущяя страница
            var pageMovies = new PagedResult<HomeViewModel>(user, page, pagesize, query.Count());

            //указатель
            var skip = (page - 1) * pagesize;

            return GetModel(pageMovies, query.Skip(skip).Take(pagesize));
        }

        /// <summary>
        /// Запрос данных из модели
        /// <para/>page - страница
        /// <para/>pagesize - количество данных на странице
        /// </summary>
        private PagedResult<HomeViewModel> GetModel(int page, int pagesize)
        {
            var query = dbContext.Movies;

            //текущяя страница
            var pageMovies = new PagedResult<HomeViewModel>(page, pagesize, query.Count());

            //указатель
            var skip = (page - 1) * pagesize;

            return GetModel(pageMovies, query.Skip(skip).Take(pagesize));
        }

        /// <summary>
        /// Объединение двух таблиц с целью присвоения постера фильму с Пангинацией
        /// </summary>
        private PagedResult<HomeViewModel> GetModel(PagedResult<HomeViewModel> pageMovies, IQueryable<FilmInfo> query)
        {
            var modelResult = from movie in query
                              join poster in dbContext.Posters on movie.Poster equals poster.Name into Posters
                        from p in Posters.DefaultIfEmpty()
                        select new HomeViewModel
                        {
                            ID = movie.ID,
                            Title = movie.Title,
                            Description = movie.Description,
                            Director = movie.Director,
                            Year = movie.Year,
                            User = movie.User,
                            Poster = GetFile(p.Image, webHostEnvironment)
                        };

            pageMovies.Results = modelResult.ToList();
            return pageMovies;
        }
        #region Poster
        /// <summary>
        /// Формирование массива байт из источника с картинкой
        /// </summary>
        static private byte[] GetFile(byte[] source, IWebHostEnvironment webHostEnvironment)
        {
            return source != null ? source : DefaultMoviePoster(webHostEnvironment);
        }

        /// <summary>
        /// Эта катринка выводиться по умолчанию
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        /// <returns></returns>
        static private byte[] DefaultMoviePoster(IWebHostEnvironment webHostEnvironment)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
            return GetBytes(Path.Combine(uploadsFolder, "bobbin.png")).Result;
        }

        /// <summary>
        /// из статических файлов
        /// </summary>
        static private async Task<byte[]> GetBytes(string formFile)
        {
            return await System.IO.File.ReadAllBytesAsync(formFile);
        }
        #endregion
        #endregion
    }
}
