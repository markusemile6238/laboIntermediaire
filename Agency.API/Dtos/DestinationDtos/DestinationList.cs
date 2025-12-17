namespace Agency.API.Dtos.DestinationDtos
{
    public class DestinationList
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int ActivityCount { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }



    }
}
