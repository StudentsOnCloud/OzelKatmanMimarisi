using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Common
{
    public class ORMBase<ET, OT> : IORM<ET> 
        where ET : class , new()
        where OT: class, new()
    {
        private static OT _current;

        public static OT Current
        {
            get
            {
                if (_current == null)
                    _current = new OT();
                return _current;
            }
        }
        private Type ETType
        {
            get
            {
                return typeof(ET);
            }
        }

        private Table TableAtt
        {
            get
            {
                var attiributes = ETType.GetCustomAttributes(typeof(Table), false);
                if (attiributes!=null && attiributes.Any())
                {
                    Table tbl = (Table)attiributes[0];
                    return tbl;
                }
                return null;
            }
        }

        public Result<bool> Delete(ET entity)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Insert(ET entity)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Tools.Connection;
            string query = "insert into ";

            query += string.Format(" {0}(", TableAtt.TableName);
            string values = " values(";
            PropertyInfo[] properties = ETType.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (pi.Name==TableAtt.IdentityColumn)
                {
                    continue;
                }

                object value = pi.GetValue(entity);
                if (value == null) continue;
                query += string.Format(" {0},",pi.Name);
                values += string.Format("@{0},",pi.Name);
                cmd.Parameters.AddWithValue(string.Format("@{0}", pi.Name), value);
            }

            query = query.Remove(query.Length-1,1);
            values = values.Remove(values.Length - 1, 1);
            query += string.Format(") {0})", values);
            
            cmd.CommandText = query;

            return cmd.Exec();

        }

        public Result<List<ET>> Select()
        {
            Type type = typeof(ET);

            string query = "select * from" ;
            var attributes = type.GetCustomAttributes(typeof(Table), false);
                if (attributes!=null&&attributes.Any())
            {
                Table tbl = (Table)attributes[0];
                query += tbl.TableName;
            }
            SqlDataAdapter adp = new SqlDataAdapter(query, Tools.Connection);
            return adp.ToList<ET>();             
        }

        public Result<bool> Update(ET entity)
        {
            throw new NotImplementedException();
        }
    }
}
