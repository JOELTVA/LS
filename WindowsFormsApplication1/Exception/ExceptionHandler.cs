using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Exceptions
{
    public static class ExceptionHandler
    {
        public static string HandleExceptions(Exception ex)
        {

            if (ex is NullReferenceException)
            {
                return "No record was found";
            }
            else if (ex is ArgumentNullException)
            {
                return "Please make a selection first";
            }
            else if (ex is System.InvalidOperationException)
            {
                return "Invalid operation, check the console for more information";
            }
            else if (ex is System.Data.SqlClient.SqlException)
            {
                return "Failed to make a connection to the database";
            }
            else if (ex is System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return "Could not make changes in the database";
            }
            return "Your request could not be executed";
        }
    }
}
