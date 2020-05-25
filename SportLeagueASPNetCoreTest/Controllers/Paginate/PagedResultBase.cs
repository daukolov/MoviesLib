using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportLeagueASPNetCoreTest.Controllers.Paginate
{
    public abstract class PagedResultBase
    {
        /// <summary>
        /// текущая страница
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// число страниц
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// количество объектов на странице
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// всего объектов
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// наименование текущего пользователя
        /// </summary>
        public string CurrentUser { get; set; }
        /// <summary>
        /// превая страница 
        /// </summary>
        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }
        /// <summary>
        /// последняя страница
        /// </summary>
        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }

        /// <summary>
        /// Есть что отобразить в Предыдущих
        /// </summary>
        public bool ShowPrevious => CurrentPage > 1;
        /// <summary>
        /// Есть что отобразить в Следующих
        /// </summary>
        public bool ShowNext => CurrentPage < PageCount;
        /// <summary>
        /// Первая страница?
        /// </summary>
        public bool ShowFirst => CurrentPage != 1;
        /// <summary>
        /// Последняя страница?
        /// </summary>
        public bool ShowLast => CurrentPage != PageCount;
    }
}
