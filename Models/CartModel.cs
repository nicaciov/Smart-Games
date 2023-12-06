namespace Smart_Games.Models
{
    public class CartModel
    {

        public long id { get; set; }
        public int game_id { get; set; }
        public int user_id { get; set; }

        public DateTime inclusion_date { get; set; }
        public DateTime edit_date { get; set; }
    }
}
