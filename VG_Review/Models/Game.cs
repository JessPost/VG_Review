using System.ComponentModel.DataAnnotations;

namespace VG_Review.Models
{
    public class Game
    {
        public int GameId { get; set; }

        [Required(ErrorMessage = "Please enter Game Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter Game Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter Game Genre")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Please enter Game Developer")]
        public string Developer { get; set; }

        [Required(ErrorMessage = "Please enter Game Price")]
        public decimal Price { get; set; }


        public List<Review> Reviews { get; set; }
    }
}
