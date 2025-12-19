namespace Agency.Domaine.Entities
{
    public class Activity : BasicEntity
    {
        public Activity() //ef
        {
        }

        // constructeur pour la dal
        public Activity(int id,string name, string description, double price, int destinationId, string? imageUrl=null)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DestinationId = destinationId;
            IsEnable = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
            ImageUrl = imageUrl;
        }

        // constructeur pour la bll
        public Activity( string name, string description, double price, int destinationId, string? imageUrl = null)
        {
            Name = name;
            Description = description;
            Price = price;
            DestinationId = destinationId;
            IsEnable = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
            ImageUrl = imageUrl;
        }




        // metier

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public int DestinationId { get; private set; }
        public string? ImageUrl { get; private set; }

        public Destination destination { get; private set; }

        // for some sql request
        public string CountryName { get; private set; }

        public void setCountryName(string name)
        {
            CountryName = name;
        }


        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeDescription(string desc)
        {
            Description = desc;
        }

        public void ChangePrice(double price)
        {
            Price = price;
        }
        public void SetImage(string imageUrl)
        {
            ImageUrl= imageUrl;
        }


    }
}