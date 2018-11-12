using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlApi {

    /// <summary>
    /// this is to delete a stored procedure it requires the SP name
    /// </summary>
    public class deleteSql {

        // the sql to run (naughty!!! :))
        string SQL = "";
        private string _cs;

        public deleteSql(string cs, string type) {
            _cs = cs;
            SQL = "DROP " + type + " {0}";
        }

        public string run(string typeName) {
            try {
                using(SqlConnection connection = new SqlConnection(_cs))
                using(SqlCommand command = new SqlCommand(string.Format(SQL, typeName), connection)) {

                    connection.Open();
                    command.ExecuteScalar(); // suitably vague!
                    return string.Format("{0} deleted.", typeName);
                }
            }
            catch(SqlException ex) {
                return string.Format("Exception trying to delete {0}: {1}", typeName, ex.Message);
            }
        }
    }
}
