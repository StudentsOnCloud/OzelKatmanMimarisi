using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Common
{
    public static class Tools
    {
        private static SqlConnection _connection;

        public static SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eticaret"].ConnectionString);
                }
                return _connection;
            }
            set { _connection = value; }
        }
         public static Result<List<ET>> ToList<ET>(this SqlDataAdapter adp) where ET : class, new()
        {
            try
            {
                DataTable dt = new DataTable();
                adp.Fill(dt);
                Type type = typeof(ET);
                List<ET> list = new List<ET>();
                PropertyInfo[] properties = type.GetProperties();
                foreach (DataRow dr in dt.Rows)
                {
                    ET tip = new ET();
                    foreach (PropertyInfo pi in properties)
                    {
                        object value = dr[pi.Name];
                        if (value != null)
                            pi.SetValue(tip, value);
                    }
                    list.Add(tip);

                }
                return new Result<List<ET>>
                {
                    IsSuccess = true,
                    Message="İşlem Başarılı.",
                    Data=list
                };
            }
            catch (Exception ex)
            {

                return new Result<List<ET>>
                {
                    IsSuccess = true,
                    Message="Hata! "+ex.Message
                };
            }
           
            }

        }
        public static Result<bool> Exec(this SqlCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();
                int result = command.ExecuteNonQuery();
                return new Result<bool>
                {
                    IsSuccess = true,
                    Message = "İşlem Başarılı",
                    Data = result > 0
                };
               
            }
            catch (Exception ex)
            {

                return new Result<bool>
                {
                    IsSuccess = false,
                    Message="Hata! "+ex.Message,
                };
            }
            finally
            {
                if (command.Connection.State != ConnectionState.Closed)
                    command.Connection.Close();
            }
        }

    }
}
