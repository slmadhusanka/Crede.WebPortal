using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace WebPortal.Utils
{
    public class ObjectConverter
    {
        public static DataTable ConvertObjectArrayToDataTable(object[] objArray)
        {
            DataTable table = new DataTable();
            try
            {
                string jsonString = JsonConvert.SerializeObject(objArray, Formatting.None);
                table = JsonConvert.DeserializeObject<DataTable>(jsonString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return table;
        }

        public static DataTable SetColumnsOrder(DataTable table, params String[] columnNames)
        {
            int columnIndex = 0;
            try
            {
                foreach (var columnName in columnNames)
                {
                    table.Columns[columnName].SetOrdinal(columnIndex);
                    columnIndex++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return table;
        }

        public static List<T> ToListof<T>(DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                {
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                }
                return instanceOfT;
            }).ToList();

            return targetList;
        }
    }
}