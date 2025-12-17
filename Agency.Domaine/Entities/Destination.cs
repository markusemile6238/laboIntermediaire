using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Domaine.Entities
{
    public class Destination : BasicEntity
    {
        public Destination() //EF
        {
        }

        // metier
        public int Id { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Description { get; private set; }

        public int ActivityCount { get; private set; } // pour calculer le nombre d'eexcursion par destination
        

        public List<Activity> Activities { get; private set; } = new List<Activity>();


        // constructeur pour BLL
        public Destination(string country, string city, string description) 
        {
            Country = country;
            City = city;
            Description = description;
            IsEnable = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
        }
        // pour mappage-DAL
        public Destination(int id,string country, string city, string description) 
        {
            Id = id;
            Country = country;
            City = city;
            Description = description;
            IsEnable = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
        }

        // methode metier
        public void AddActivity(Activity activity)
        {
            Activities.Add(activity);
        }

        public void SetDescription(string description)
        {
            if (description != null)
            {
                Description = description;
            }
        }
        public void SetCountry(string country)
        {
            if (country != null)
            {
                Country = country;
            }
        }
        public void SetCity(string city)
        {
            if (city != null)
            {
                City = city;
            }
        }

        public void SetActivityCount(int activityCount)
        {
            ActivityCount = activityCount;
        }

    }
}
