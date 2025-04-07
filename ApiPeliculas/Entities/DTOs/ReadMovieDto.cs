using ApiPeliculas.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Entities.DTOs
{
    public class ReadMovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ImagenPath { get; set; }
        public string Classification { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CategoryName { get; set; }
    }
}
