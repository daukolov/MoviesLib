using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportLeagueASPNetCoreTest.ViewModels
{
    public class MovieViewModel
    {
        [Required(ErrorMessage = "Введите название фильма")]
        [Display(Name = "Название фильма")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Год")]
        public int Year { get; set; }


        [Display(Name = "Режисер")]
        public string Director { get; set; }

        [Display(Name = "Постер")]
        [DataType(DataType.Upload)]
        public IFormFile Poster { get; set; }

        private bool? editable = false;
        public bool? Editable { get => editable; set => editable = value; }

        private int? id = 0;
        public int? Id { get => id; set => id = value; }
    }
}
