namespace Smart_Games.Models
{
    public class StoreModel
    {

        public long id { get; set; }
        public long game_id { get; set; }
        public string store { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public DateTime inclusion_date { get; set; }
        public DateTime edit_date { get; set; }

    }
}
