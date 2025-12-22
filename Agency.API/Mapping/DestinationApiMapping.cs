using Agency.API.Dtos.DestinationDtos;
using Agency.Domaine.Entities;
using System.Diagnostics.Metrics;

namespace Agency.API.Mapping
{
    public static class DestinationApiMapping
    {
        public static Destination ToDestination(this DestinationCreateNewOne dto)
        {
            return new Destination(

                country : dto.Country,
                city : dto.City,
                description : dto.Description,
                imageUrl:dto.ImageUrl
                );
        }
    }
}
