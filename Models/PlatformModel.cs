namespace Smart_Games.Models
{
    public class PlatformModel
    {

        public long id { get; set; }
        public long game_id { get; set; }
        public string platform { get; set; }
        public DateTime inclusion_date { get; set; }
        public DateTime edit_date { get; set; }

    }
}
