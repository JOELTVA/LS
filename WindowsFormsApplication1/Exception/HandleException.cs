using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Exceptions
{
    public class HandleException
    {
        public string HandleExceptions(Exception ex) { 

            if (ex is NullReferenceException)
            {
                return "No record was found";
            }
            else if (ex is ArgumentNullException)
            {
                return "The value is not chosen or valid";
            }
            else if (ex is System.Data.SqlClient.SqlException)
            {
                return "Failed to connect to the database";
            }
            else if (ex is System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return "Could not update or make a change in the database";
            }
            else if (ex is System.InvalidOperationException)
            {
                return "No record was found on this search";
            }
            return "Your request could not be executed";
        }
    }
}
