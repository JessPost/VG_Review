namespace VG_Review.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Developer { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
