using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportLeagueASPNetCoreTest.ViewModels
{
    public class HomeViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string Director { get; set; }

        public string User { get; set; }

        public byte[] Poster { get; set; }
    }
}
