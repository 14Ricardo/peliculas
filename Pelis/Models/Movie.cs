using System;
using System.Collections.Generic;


#nullable disable

namespace Pelis.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Genero { get; set; }
        public string Puntuacion { get; set; }
        public string Rating { get; set; }
        public DateTime FechaDePublicacion { get; set; }
    }
}
