namespace VG_Review.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string ReviewTitle { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public int Rating { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
