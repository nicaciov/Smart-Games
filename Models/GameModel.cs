using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;

namespace Smart_Games.Models
{
    public class GameModel
    {

        public long id { get; set; }
        public string game_name { get; set; }
        public string game_description { get; set; }
        public double game_price { get; set; }
        public string photo { get; set; }


        public DateTime inclusion_date { get; set;}
        public DateTime edit_date { get; set;}

        public List<PlatformModel> listPlatforms { get; set; }
        public List<StoreModel> listStores { get; set; }

    }

   
}
