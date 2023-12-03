using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Smart_Games.Models;
using System.Diagnostics;

namespace Smart_Games.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            string connectionString = "Server=localhost;Database=SMART_GAMES;User ID=root;Password=123456;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM tabela_jogos";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Ler os dados do resultado da consulta
                            int id = reader.GetInt32("id");
                            string nome_jogo = reader.GetString("nome_jogo");

                            Console.WriteLine($"ID: {id}, Nome Jogo: {nome_jogo}");
                        }
                    }
                }
            }

            return View();
        }

    }
}
