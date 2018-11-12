using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace run_sql
{
    public class runner
    {
        private readonly string _connectionString;
        private readonly DirectoryInfo _dirInfo;
        private readonly RichTextBox _tb;

        //Data Source = WINDEVAD1051; Initial Catalog = b037_Darta; Integrated Security = True
        public runner(string dbName, string dbTable, string directory, RichTextBox tb)
        {
            _connectionString = "Data Source = "+dbName+"; Initial Catalog = "+dbTable+"; Integrated Security = True";
            _dirInfo = new DirectoryInfo(directory);
            _tb = tb;
        }

        /// <summary>
        /// The mad thing about this method is that you never have a clue which piece of sql is running at any one time!
        /// </summary>
        /// <returns></returns>
        public async Task run()
        {
            try
            {                
                await Task.Run(() => {
                    // get the files with sql at the end
                    var files = _dirInfo.EnumerateFiles("*.sql");

                    Parallel.ForEach(files, async p =>
                    {
                        var sql = await p.OpenText().ReadToEndAsync();

                        _tb.Invoke((MethodInvoker)delegate
                        {
                            _tb.AppendText("\r\nRunning ========\r\n");
                            _tb.AppendText(sql);
                            _tb.AppendText("\r\n========= Complete\r\n\r\n");
                            _tb.ScrollToCaret();
                        });

                        using (SqlConnection conn = new SqlConnection(_connectionString))
                        {
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            conn.Open();

                        try
                            {
                                await cmd.ExecuteNonQueryAsync();
                            }
                            catch (SqlException e)
                            {
                                _tb.Invoke((MethodInvoker)delegate
                                {
                                    _tb.AppendText("\r\n!!!!!SQL EXCEPTION!!!!!\r\n");
                                    _tb.AppendText(e.Message);
                                    _tb.ScrollToCaret();
                                });
                            }
                        }
                    });

                });
            } catch(Exception e)
            {
              
                _tb.AppendText("\r\n!!!!!EXCEPTION!!!!!\r\n");
                _tb.AppendText(e.Message);
                _tb.ScrollToCaret();
            }
        }
    }
}
