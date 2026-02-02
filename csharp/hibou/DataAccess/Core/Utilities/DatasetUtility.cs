using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Keane.CH.Framework.DataAccess.Core.Utilities
{
    /// <summary>
    /// Encapsulates dataset related utility functions.
    /// </summary>
    /// <created by="Alain Dafflon" date="01-Jan-2008" />
    public class DatasetUtility
    {
        #region Public static methods

        /// <summary>
        /// Fills a data table with data derived froma generic list.
        /// </summary>
        /// <typeparam name="T">The type being managed by the list.</typeparam>
        /// <param name="dt">A datatable.</param>
        /// <param name="list">A generic list.</param>
        public static void FillDataTable<T>(DataTable dt, List<T> list)
        {
            // Defensive programming.
            if (dt == null)
                throw new ArgumentNullException("dt");
            if (list == null)
                throw new ArgumentNullException("list");
            if (list.Count == 0)
                return;

            // Iterate the collection & write a data row.
            foreach (T item in list)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn column in dt.Columns)
                {
                    dr[column] = GetPropertyValue(column.ColumnName, item);
                }
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
        }

        #endregion Public static methods

        #region Private methods

        /// <summary>
        /// Gets the relfected property value from the reflection target.
        /// </summary>
        /// <param name="propertyName">The name of the property to be reflected.</param>
        /// <param name="target">The reflection target.</param>
        /// <returns>The reflected property value.</returns>
        private static object GetPropertyValue(
            string propertyName, object target)
        {
            // Initialise result.
            object result = null;

            // Derive reflected property info.
            PropertyInfo propertyInfo = 
                target.GetType().GetProperty(propertyName, BindingFlags.Instance |
                                                             BindingFlags.Public |
                                                             BindingFlags.NonPublic);

            // Push data into target.
            if (propertyInfo != null)
            {
                MethodInfo methodInfo = GetMethodInfo(propertyInfo);
                if (methodInfo != null)
                {
                    result = methodInfo.Invoke(target, null);
                }
            }

            // Parse nulls.
            if (result == null)
            {
                result = System.DBNull.Value;
            }
            // Parse null dates.
            else if (result is DateTime && result.Equals(DateTime.MinValue))
            { 
                result = System.DBNull.Value;
            }
            // Parse null integer.
            else if (result is Int32 && result.Equals(Int32.MinValue))
            {
                result = System.DBNull.Value;
            }
            // Parse null double.
            else if (result is Double && result.Equals(Double.MinValue))
            {
                result = System.DBNull.Value;
            }
            else if (result is float && result.Equals(float.MinValue))
            {
                result = System.DBNull.Value;
            }
            // Return.
            return result;
        }

        /// <summary>
        /// Gets the method information in readiness for invocation.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns>The corresponding method info.</returns>
        private static MethodInfo GetMethodInfo(PropertyInfo propertyInfo)
        {
            foreach (MethodInfo accessor in propertyInfo.GetAccessors(true))
            {
                if (accessor.Name.StartsWith("get"))
                {
                    return accessor;
                }
            }
            return null;
        }

        #endregion Private methods
    }
}