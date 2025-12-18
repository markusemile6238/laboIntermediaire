using Agency.API.Dtos.ActivityDtos;
using Agency.Domaine.Entities;

namespace Agency.API.Mapping
{
    public static class ActivityApiMapping
    {
        public static Activity ToActivity(this ActivityCreateDtos dto)
        {
            return new Activity(
                name:dto.Name,
                description:dto.Description,
                price:dto.Price,
                destinationId:dto.DestinationId
                );
     
        }
    }
}
