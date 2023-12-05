using Smart_Games.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Smart_Games.Models;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart_Games.DAO
{
    public class DAO
    {

        public static GameModel CRUDGame(MySqlDataReader reader)
        {

            GameModel gameModel = new GameModel();

            gameModel.id = reader.GetInt32("id");
            gameModel.game_name = reader.GetString("game_name");
            gameModel.game_description = reader.GetString("game_description");
            gameModel.game_price = reader.GetDouble("game_price");
             

            gameModel.photo = reader.GetString("photo"); 

            return gameModel;

        }


        public static List<GameModel> listAllGames(IConfiguration _configuration)
        {

            List<GameModel> list = new List<GameModel>(); 

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT g.id, g.game_name, g.game_description, g.game_price, g.inclusion_date, g.edit_date, i.photo FROM games_table AS g INNER JOIN images_table AS i ON g.id = i.game_id;";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            list.Add(CRUDGame(reader));

                        }
                    }
                }
            }

            return list;
        }

    }
}
