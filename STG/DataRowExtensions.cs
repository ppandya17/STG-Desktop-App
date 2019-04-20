using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    static class DataRowExtensions
    {
        public static object GetValue(this DataRow row, String column)
        {
            return row.Table.Columns.Contains(column) ? row[column] : null;
        }
    }
}
