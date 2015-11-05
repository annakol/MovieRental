using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MovieRental.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }

        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
    }
}