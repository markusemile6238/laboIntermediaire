namespace Agency.API.Dtos.DestinationDtos
{
    public class DestinationDetails
    {
        public int Id { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Description { get; private set; }
        public int ActivityCount { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }



    }
}
