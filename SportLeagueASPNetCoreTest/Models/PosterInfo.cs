using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportLeagueASPNetCoreTest.Models
{
    /// <summary>
    /// Постер к фильму
    /// </summary>
    public class PosterInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Файл
        /// </summary>
        public byte[] Image { get; set; }
    }
}
