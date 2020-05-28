using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SportLeagueASPNetCoreTest.Models
{
    /// <summary>
    /// Информация о фильме
    /// </summary>
    public class FilmInfo
    {
        /// <summary>
        /// Уникальный Идентификатор
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Название фильма
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// год
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// режисер
        /// </summary>
        public string Director { get; set; }
        /// <summary>
        /// Пользователь, который внес фильм в базу
        /// </summary>
        public string User {get; set;}
        /// <summary>
        /// Ссылка на файл в соседней таблице
        /// </summary>
        public string Poster { get; set; }
    }
}
