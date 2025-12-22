using Agency.Domaine.Entities;

namespace Agency.API.Dtos.DestinationDtos
{
    public class DestinationCreateNewOne
    {

        
        public string Country { get;  set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }


       

    }
}
