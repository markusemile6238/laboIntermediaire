using Agency.Domaine.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Mapper
{
    public static class DestinationMapper
    {
        public static Destination ToDestination(this SqlDataReader reader)
        {
            if(reader == null) throw new ArgumentNullException(nameof(reader));

            var dest =  new Destination(
               id: (int)reader["Id"],
               country: (string)reader["Country"],
               city: (string)reader["City"],
               description:(string)reader["Description"],
               imageUrl : (string)reader["ImageUrl"]
                );
            if (reader.HasColumn("ActivityCount"))
            {
                dest.SetActivityCount((int)reader["ActivityCount"]);
            }
            return dest;
        }


        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
