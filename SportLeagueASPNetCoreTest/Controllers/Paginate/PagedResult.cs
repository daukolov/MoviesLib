using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SportLeagueASPNetCoreTest.Controllers.Paginate
{


    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        private string title;
        /// <summary>
        /// Демонстрация текущей страницы
        /// </summary>
        public string PageTitle
        {
            get 
            {
                return $"{title}";
            }
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// Конструктор
        /// <para/>page - текущая страница
        /// <para/>pageSize - число объектов на странице
        /// <para/>count - общее число объектов
        /// </summary>
        public PagedResult(int page, int pageSize, int count)
        {
            CurrentPage = page;
            PageSize = pageSize;
            RowCount = count;

            var pageCount = (double)RowCount / pageSize;
            PageCount = (int)Math.Ceiling(pageCount);

            CurrentUser = "All";

            title = "Все фильмы";
        }

        public PagedResult(string user, int page, int pageSize, int count) : this(page, pageSize, count)
        {
            CurrentUser = user;
            title = $"Фильмы {user}";
        }       
    }
}
