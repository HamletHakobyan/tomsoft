﻿using System.Linq;

namespace Millionaire.Design
{
    public class DesignStartPageViewModel
    {
        public DesignStartPageViewModel()
        {
            this.Place = "Place";
            this.Date = "Date";
            this.Footer = "Footer";
        }
        public string Place { get; set; }
        public string Date { get; set; }
        public string Footer { get; set; }
    }
}
