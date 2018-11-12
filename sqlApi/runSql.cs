using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlApi
{
    public class runSql
    {
        private string _connectionString;
        private List<string> _files;
        private string _directory;

        public runSql(string connectionString, dir dir)
        {
            _connectionString = connectionString;
            _files = dir.listSql();
            _directory = dir.Dirpath;
        }

        /// <summary>
        /// This was originally written to be non-blocking (and parallel) but it intermittently started throwing exceptions,
        ///  which were (I think) either multi-reading files (doubtful) or multi-writing to the database.
        /// </summary>
        /// <returns></returns>
        public List<string> run()
        {
            List<string> logList = new List<string>();
            try
            {
                _files.ForEach(p=>
                {
                    // somehow find the different sql names the delete it RIGHT HERE 
                    //       ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
                    //findNextWord findNextWord = new findNextWord(_directory + p);

                    //deleteSql deleteSql = new deleteSql(_connectionString, "procedure");
                    //string spName = findNextWord.find("procedure"); // just realised that this could be Function, Table etc ...
                    //logList.Add(deleteSql.run(spName));
                    //       ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

                    var fi = new FileInfo(_directory + p);
                    var sql = fi.OpenText().ReadToEnd();

                    logList.Add("Attempted " + p);

                    using(SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        conn.Open();

                        try
                        {
                            cmd.ExecuteNonQuery();
                            logList.Add("Success " + p);
                        }
                        catch (SqlException e)
                        {
                            logList.Add("SQL EXCEPTION " + p +", msg: " + e.Message);
                        }
                    }
                    
                });
               
            }
            catch (Exception e)
            {
                logList.Add("GENERAL EXCEPTION msg: " + e.Message);
            }
            return logList;
        }
    }
}
