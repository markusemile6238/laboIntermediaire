namespace Agency.Domaine.Entities
{
    public class Activity : BasicEntity
    {
        public Activity() //ef
        {
        }

        // constructeur pour la dal
        public Activity(int id,string name, string description, double price, int destinationId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DestinationId = destinationId;
            IsEnable = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
        }

        // constructeur pour la bll
        public Activity( string name, string description, double price, int destinationId)
        {
            Name = name;
            Description = description;
            Price = price;
            DestinationId = destinationId;
            IsEnable = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
        }




        // metier

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public int DestinationId { get; private set; }

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


    }
}