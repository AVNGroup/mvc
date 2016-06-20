using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class PuppyMark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GeoLong { get; set; } // долгота - для карт google
        public string GeoLat { get; set; } // широта - для карт google
    }
}