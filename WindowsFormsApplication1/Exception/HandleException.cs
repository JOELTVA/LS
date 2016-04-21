using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Exceptions
{
    public class HandleException
    {
        public string HandleExceptions(Exception ex)
        {
            if (ex is NullReferenceException)
            {
                return "Something went wrong, no record were found";
            }
            else if (ex is ArgumentNullException)
            {
                return "Something went wrong, the value is not chosen or valid";
            }
            else if (ex is System.Data.SqlClient.SqlException)
            {
                return "Something went wrong when connecting to the database";
            }
            else if (ex is System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return "Something went wrong, could not update or change in the database";
            }
            else if (ex is System.InvalidOperationException)
            {
                return "Something went wrong, no record found on this search";
            }
            return "Something went wrong, your request could not be executed";
        }
    }
}
