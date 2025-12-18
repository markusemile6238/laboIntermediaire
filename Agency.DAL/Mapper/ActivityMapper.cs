using Agency.Domaine.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Mapper
{
    public static class ActivityMapper
    {
        public static Activity ToActivity(this SqlDataReader reader)
        {
            if(reader == null) throw new ArgumentNullException(nameof(reader));
            var act = new Activity(
                id: (int)reader["Id"],
                name: (string)reader["Name"],
                description:(string)reader["Description"],
                price: (double)reader["Price"],
                destinationId: (int)reader["DestinationId"]               
                );
            if (reader.HasColumn("CountryName"))
            {
              act.setCountryName((string)reader["CountryName"]);
            }
            return act;

        }
    }
}
