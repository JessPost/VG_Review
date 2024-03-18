using System.ComponentModel.DataAnnotations;
using VG_Review.Areas.Identity.Data;

namespace VG_Review.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "Please enter Review Title")]
        public string ReviewTitle { get; set; }

        [Required(ErrorMessage = "Please enter Review Content")]
        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Please select game rating")]
        public int? Rating { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
