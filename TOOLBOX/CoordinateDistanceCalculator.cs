using Smart_Games.Models;

namespace Smart_Games.TOOLBOX
{
    public class CoordinateDistanceCalculator
    {

        public static StoreModel FindClosestCoordinate(int id, string userLatitude, string userLongitude, IConfiguration _configuration)
        {

            List<StoreModel> coordinates = DAO.DAO.listAllStores(_configuration, id);
            StoreModel nextStore = null;
            double smallestDistance = double.MaxValue;

            foreach (var coordinate in coordinates)
            {
                double distance = CalculateHaversineDistance(Double.Parse(userLatitude), Double.Parse(userLongitude), Double.Parse(coordinate.lat), Double.Parse(coordinate.lon));

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    nextStore = coordinate;
                }
            }

            return nextStore;
        }

        private static double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadius = 6371; // Average Earth radius in kilometers

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = EarthRadius * c;
            return distance;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

    }
}
