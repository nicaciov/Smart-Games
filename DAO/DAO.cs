using Smart_Games.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Smart_Games.Models;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace Smart_Games.DAO
{
    public static class DAO
    {

        private static GameModel ObjectGame(IConfiguration _configuration, MySqlDataReader reader)
        {

            GameModel gameModel = new GameModel();

            gameModel.id = reader.GetInt32("id");
            gameModel.game_name = reader.GetString("game_name");
            gameModel.game_description = reader.GetString("game_description");
            gameModel.game_price = reader.GetDouble("game_price");

            gameModel.listPlatforms = listAllPlatforms(_configuration, gameModel.id);
            gameModel.listStores = listAllStores(_configuration, gameModel.id);

            try
            {
                string mysqlDateFormat = "dd/MM/yyyy HH:mm:ss";
                gameModel.inclusion_date = DateTime.ParseExact(reader.GetString("inclusion_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
                gameModel.edit_date = DateTime.ParseExact(reader.GetString("edit_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
            }catch(Exception ex)
            {
                string mysqlDateFormat = "yyyy-MM-dd HH:mm:ss";
                gameModel.inclusion_date = DateTime.ParseExact(reader.GetString("inclusion_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
                gameModel.edit_date = DateTime.ParseExact(reader.GetString("edit_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
            }            

            gameModel.photo = reader.GetString("photo"); 

            return gameModel;

        }

        private static PlatformModel ObjectPlatform(IConfiguration _configuration, MySqlDataReader reader)
        {

            PlatformModel platformModel = new PlatformModel();

            platformModel.id = reader.GetInt32("id");
            platformModel.game_id = reader.GetInt32("game_id"); 
            platformModel.platform = reader.GetString("platform");

            try
            {
                string mysqlDateFormat = "dd/MM/yyyy HH:mm:ss";
                platformModel.inclusion_date = DateTime.ParseExact(reader.GetString("inclusion_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
                platformModel.edit_date = DateTime.ParseExact(reader.GetString("edit_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                string mysqlDateFormat = "yyyy-MM-dd HH:mm:ss";
                platformModel.inclusion_date = DateTime.ParseExact(reader.GetString("inclusion_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
                platformModel.edit_date = DateTime.ParseExact(reader.GetString("edit_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
            }

            return platformModel;

        }

        private static StoreModel ObjectStore(IConfiguration _configuration, MySqlDataReader reader)
        {

            StoreModel storeModel = new StoreModel();

            storeModel.id = reader.GetInt32("id");
            storeModel.game_id = reader.GetInt32("game_id");

            storeModel.store = reader.GetString("store");
            storeModel.lat = reader.GetString("lat");
            storeModel.lon = reader.GetString("lon");

            try
            {
                string mysqlDateFormat = "dd/MM/yyyy HH:mm:ss";
                storeModel.inclusion_date = DateTime.ParseExact(reader.GetString("inclusion_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
                storeModel.edit_date = DateTime.ParseExact(reader.GetString("edit_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                string mysqlDateFormat = "yyyy-MM-dd HH:mm:ss";
                storeModel.inclusion_date = DateTime.ParseExact(reader.GetString("inclusion_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
                storeModel.edit_date = DateTime.ParseExact(reader.GetString("edit_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
            }

            return storeModel;

        }

        private static CartModel ObjectCart(IConfiguration _configuration, MySqlDataReader reader)
        {

            CartModel cartModel = new CartModel();

            cartModel.id = reader.GetInt32("id");
            cartModel.game_id = reader.GetInt32("game_id"); 

            try
            {
                string mysqlDateFormat = "dd/MM/yyyy HH:mm:ss";
                cartModel.inclusion_date = DateTime.ParseExact(reader.GetString("inclusion_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
                cartModel.edit_date = DateTime.ParseExact(reader.GetString("edit_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                string mysqlDateFormat = "yyyy-MM-dd HH:mm:ss";
                cartModel.inclusion_date = DateTime.ParseExact(reader.GetString("inclusion_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
                cartModel.edit_date = DateTime.ParseExact(reader.GetString("edit_date"), mysqlDateFormat, CultureInfo.InvariantCulture);
            }

            return cartModel;
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

                            list.Add(ObjectGame(_configuration, reader));

                        }
                    }
                }
            }

            return list;
        }

        public static List<GameModel> searchGameName(IConfiguration _configuration, string game_name)
        {

            List<GameModel> list = new List<GameModel>();

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT g.id, g.game_name, g.game_description, g.game_price, g.inclusion_date, g.edit_date, i.photo FROM games_table AS g INNER JOIN images_table AS i ON g.id = i.game_id WHERE g.game_name LIKE '%"+game_name+"%';";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            list.Add(ObjectGame(_configuration, reader));

                        }
                    }
                }
            }

            return list;
        }

        public static List<GameModel> listAllGamesRand(IConfiguration _configuration)
        {

            List<GameModel> list = new List<GameModel>();

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT g.id, g.game_name, g.game_description, g.game_price, g.inclusion_date, g.edit_date, i.photo FROM games_table AS g INNER JOIN images_table AS i ON g.id = i.game_id ORDER BY RAND();";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            list.Add(ObjectGame(_configuration, reader));

                        }
                    }
                }
            }

            return list;
        }

        public static GameModel getGame(IConfiguration _configuration, int id)
        {

            GameModel gameModel = null;

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT g.id, g.game_name, g.game_description, g.game_price, g.inclusion_date, g.edit_date, i.photo FROM games_table AS g INNER JOIN images_table AS i ON g.id = i.game_id WHERE g.id = " + id +";";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            gameModel = ObjectGame(_configuration, reader);

                        }
                    }
                }
            }

            return gameModel;
        }

        public static List<PlatformModel> listAllPlatforms(IConfiguration _configuration, long game_id)
        {

            List<PlatformModel> list = new List<PlatformModel>();

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT id, game_id, platform, inclusion_date, edit_date FROM platforms_table WHERE game_id = " + game_id + ";";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            list.Add(ObjectPlatform(_configuration, reader));

                        }
                    }
                }
            }

            return list;
        }

        public static List<StoreModel> listAllStores(IConfiguration _configuration, long game_id)
        {

            List<StoreModel> list = new List<StoreModel>();

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT s.id, game_id, store, lat, lon, s.inclusion_date, s.edit_date FROM stores_table AS s INNER JOIN place_stores_table AS p ON p.id = s.store_id WHERE game_id = " + game_id + ";";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            list.Add(ObjectStore(_configuration, reader));

                        }
                    }
                }
            }

            return list;
        }

        public static List<CartModel> listCart(IConfiguration _configuration)
        {

            List<CartModel> list = new List<CartModel>();

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT id, game_id, inclusion_date, edit_date FROM shop_cart;";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            list.Add(ObjectCart(_configuration, reader));

                        }
                    }
                }
            }

            return list;
        }

        public static List<CartModel> listCheckOutItens(IConfiguration _configuration, long checkOutid)
        {

            List<CartModel> list = new List<CartModel>();

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT id, game_id, check_out_id, inclusion_date, edit_date FROM checkout_shop_itens WHERE check_out_id = "+checkOutid+";";
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            list.Add(ObjectCart(_configuration, reader));

                        }
                    }
                }
            }

            return list;
        }

        public static void addCart(IConfiguration _configuration, int id)
        {

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");
             
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            { 
                connection.Open(); 
                 
                string sql = "INSERT INTO shop_cart (game_id, inclusion_date, edit_date) VALUES (@gameId, Now(), Now())";
                MySqlCommand command = new MySqlCommand(sql, connection);
                 
                command.Parameters.AddWithValue("@gameId", id); 
                 
                int rowsAffected = command.ExecuteNonQuery();
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            { 
                connection.Close();
            }
        }

        public static void removeCart(IConfiguration _configuration, int id)
        {

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string sql = "DELETE FROM shop_cart WHERE game_id = @gameId LIMIT 1;";
                MySqlCommand command = new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("@gameId", id);

                int rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public static void clearCart(IConfiguration _configuration)
        {

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string sql = "DELETE FROM shop_cart;";
                MySqlCommand command = new MySqlCommand(sql, connection);

                int rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public static long addCheckout(IConfiguration _configuration, CheckOutModel checkOut)
        {
            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            string insertQuery = @"INSERT INTO check_out 
                                  (name, family_name, email, cpf, address, address_complement, 
                                  country, state, cep, type_payment_credit, type_payment_debit, 
                                  type_payment_paypal, card_name, card_number, card_date_expiration, 
                                  card_cvv, code, inclusion_date, edit_date) 
                                  VALUES 
                                  (@Name, @FamilyName, @Email, @Cpf, @Address, @AddressComplement, 
                                  @Country, @State, @Cep, @TypePaymentCredit, @TypePaymentDebit, 
                                  @TypePaymentPaypal, @CardName, @CardNumber, @CardDateExpiration, 
                                  @CardCvv, @Code, NOW(), NOW())";

            using MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
            cmd.Parameters.AddWithValue("@Name", checkOut.name);
            cmd.Parameters.AddWithValue("@FamilyName", checkOut.family_name);
            cmd.Parameters.AddWithValue("@Email", checkOut.email);
            cmd.Parameters.AddWithValue("@Cpf", checkOut.cpf);
            cmd.Parameters.AddWithValue("@Address", checkOut.address);
            cmd.Parameters.AddWithValue("@AddressComplement", checkOut.address_complement);
            cmd.Parameters.AddWithValue("@Country", checkOut.country);
            cmd.Parameters.AddWithValue("@State", checkOut.state);
            cmd.Parameters.AddWithValue("@Cep", checkOut.cep);
            cmd.Parameters.AddWithValue("@TypePaymentCredit", checkOut.type_payment_credit);
            cmd.Parameters.AddWithValue("@TypePaymentDebit", checkOut.type_payment_debit);
            cmd.Parameters.AddWithValue("@TypePaymentPaypal", checkOut.type_payment_paypal);
            cmd.Parameters.AddWithValue("@CardName", checkOut.card_name);
            cmd.Parameters.AddWithValue("@CardNumber", checkOut.card_number);
            cmd.Parameters.AddWithValue("@CardDateExpiration", checkOut.card_date_expiration);
            cmd.Parameters.AddWithValue("@CardCvv", checkOut.card_cvv);
            cmd.Parameters.AddWithValue("@Code", checkOut.code);

            cmd.ExecuteNonQuery();

            return cmd.LastInsertedId;

        }

        public static void addCheckout_Shop_Itens(IConfiguration _configuration, int id, long checkoutId)
        {

            string connectionString = _configuration.GetConnectionString("SmartGamesConnection");

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string sql = "INSERT INTO checkout_shop_itens (game_id, check_out_id, inclusion_date, edit_date) VALUES (@gameId, @checkoutId, Now(), Now())";
                MySqlCommand command = new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("@gameId", id);
                command.Parameters.AddWithValue("@checkoutId", checkoutId);

                int rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
