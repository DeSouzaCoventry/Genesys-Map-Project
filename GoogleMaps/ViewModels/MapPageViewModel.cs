using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleMaps.ViewModels
{
    public class MapPageViewModel
    {
        

        public MapPageViewModel()
        {
        }

        public class VehicleLocations
        {
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }

        internal async Task<List<VehicleLocations>> LoadVehicles()
        {
            List<VehicleLocations> vehicleLocations = new List<VehicleLocations>
            {
             new VehicleLocations{Latitude = "52.404777",Longitude="-1.500181"},
             new VehicleLocations{Latitude = "52.40891",Longitude="-1.499623"},
             new VehicleLocations{Latitude = "52.409673",Longitude="-1.501640"},
             new VehicleLocations{Latitude = "52.410170",Longitude="-1.498250"},


            };

            return vehicleLocations;
            
        }

    }
}
