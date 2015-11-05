using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRental.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int GenreId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public string Director { get; set; }

        [Range(0.01, 100.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Trailer")]
        public string TrailerUrl { get; set; }

        [DataType(DataType.ImageUrl)]
        [StringLength(1024)]
        [Display(Name = "Art")]
        public string ArtUrl { get; set; }

        [NotMapped]
        [Display(Name = "Art")]
        public HttpPostedFileBase ArtImage { get; set; }
        public virtual Genre Genre { get; set; }
    }
}