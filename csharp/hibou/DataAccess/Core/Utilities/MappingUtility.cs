using System;
using System.Data;

namespace Keane.CH.Framework.DataAccess.Core.Utilities
{
    /// <summary>
    /// Encapsulates generic mapping from relational form to object form.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public class MappingUtility
    {
        #region Public static methods

        /// <summary>
        /// Deserializes from a column within a data reader.
        /// </summary>
        public static T Map<T>(DataRow dr, string col) where T : new()
        {

            try
            {
                return Map(dr[col.Trim()], new T());
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("Column : " + col, ex);
            }
        }

        /// <summary>
        /// Deserializes from a column within a data reader.
        /// </summary>
        public static T Map<T>(DataRow dr, string col, T nullValue)
        {
            try
            {
                return Map(dr[col.Trim()], nullValue);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("Column : " + col, ex);
            }
        }

        /// <summary>
        /// Deserializes from a column within a data reader.
        /// </summary>
        public static T Map<T>(IDataReader dr, string col, T nullValue)
        {
            try
            {
                return Map(dr[col.Trim()], nullValue);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("Column : " + col, ex);
            }
        }

        /// <summary>
        /// Deserializes from a column within a data reader.
        /// </summary>
        private static T Map<T>(object col, T nullValue)
        {
            try
            {
                if (col is DBNull)
                {
                    return nullValue;
                }
                else if (col == null)
                {
                    return nullValue;
                }
                else
                {
                    return (T)col;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("Invalid cast upon column : " + col, ex);
            }
        }

        /// <summary>
        /// Deserializes from a column within a data reader.
        /// </summary>
        private static T Map<T>(object col) where T: new()
        {
            return Map(col, new T());
        }

        /// <summary>
        /// Deserializes from a column within a data reader.
        /// </summary>
        public static T Map<T>(IDataReader dr, string col) where T : new()
        {
            try
            {
                return Map(dr[col.Trim()], new T());
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("Column : " + col, ex);
            }
        }

        #endregion Public static methods
    }
}
