using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.ExtensionMethods;

namespace Keane.CH.Framework.Core.Utilities.Factory
{
    /// <summary>
    /// Encapsulates factory methods for creating instances & assigning their intial state.
    /// </summary>
    public static class InstanceFactory
    {
        /// <summary>
        /// Creates & returns an instance performing a deep instantiation if requested.
        /// </summary>
        /// <param name="objectType">The type of object to be created.</param>
        /// <param name="performDeep">Flag indicating whether the full object graph will be created.</param>
        /// <returns>An instance of the passed type.</returns>
        public static object Create(
            Type objectType,
            bool performDeep)
        {
            // Exception if passed a null.
            if (objectType == null)
                throw new ArgumentNullException("Cannot instantiate a null type.");

            // Instantiate the test instance.
            object result = Activator.CreateInstance(objectType); ;

            // Initialise it's state.
            InitialiseState(result);

            // Return the new instance.
            return result;
        }

        /// <summary>
        /// Creates & returns an instance performing a deep instantiation if requested.
        /// </summary>
        /// <param name="objectType">The type of object to be created.</param>
        /// <param name="performDeep">Flag indicating whether the full object graph will be created.</param>
        /// <returns>An instance of the passed type.</returns>
        public static void InitialiseState(
            object instance)
        {
            // Exception if passed a null.
            if (instance == null)
                throw new ArgumentNullException("Cannot initialise a null type.");

            // If not dealing with a collection then then proceed.
            ICollection collection = instance as ICollection;
            if (collection == null)
            {
                // Iterate properties & set initial test values.
                PropertyInfo[] propertyList =
                    instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo pi in propertyList)
                {
                    // Exclude extension data objects, read only properties, value types.
                    if ((pi.CanWrite) &&
                        (!pi.PropertyType.IsValueType) &&
                        (!pi.PropertyType.Equals(typeof(String))) &&
                        (!pi.PropertyType.Equals(typeof(ExtensionDataObject))))
                    {
                        object propertyValue = Create(pi.PropertyType, true);
                        pi.SetValue(instance, propertyValue, null);
                    }
                }
            }

            // If the object implements the set test state interface then invoke.
            IStateInitialiser stateInitialiser = instance as IStateInitialiser;
            if (stateInitialiser != null)
                stateInitialiser.InitialiseState();
        }

        /// <summary>
        /// Creates & returns a test instance.
        /// </summary>
        /// <param name="objectType">The type of object to be created.</param>
        /// <param name="performDeep">Flag indicating whether the full object graph will be created.</param>
        /// <returns>A test instance with state initialised to test values.</returns>
        public static object CreateForTest(
            Type objectType,
            bool performDeep)
        {
            // Exception if passed a null.
            if (objectType == null)
                throw new ArgumentNullException("Cannot instantiate a null type.");

            // Instantiate the test instance (unless dealing with arrays).
            object result = null;
            if (!typeof(byte[]).Equals(objectType))
                result = Activator.CreateInstance(objectType);

            // If not dealing with a collection then then proceed.
            ICollection collection = result as ICollection;
            if (collection == null)
            {
                // Iterate properties & set initial test values.
                PropertyInfo[] propertyList =
                    objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo pi in propertyList)
                {
                    // Exclude extension data objects & read only properties.
                    if (!pi.PropertyType.Equals(typeof(ExtensionDataObject)) &&
                        (pi.CanWrite))
                    {
                        // Set the property value.
                        object propertyValue = GetStateForTest(pi);

                        // If unassigned then create (if performing deep).
                        if ((propertyValue == null) && (performDeep))
                            propertyValue = CreateForTest(pi.PropertyType, performDeep);

                        // Set the property.
                        pi.SetValue(result, propertyValue, null);
                    }
                }
            }

            // Return the new instance.
            return result;
        }

        /// <summary>
        /// Determines whether the test state cna be set automatically or not.
        /// </summary>
        /// <returns>True if it can.</returns>
        private static bool StateCanBeSet(PropertyInfo pi)
        {
            bool result = false;
            if ((pi.PropertyType.IsValueType) |
                (typeof(String).Equals(pi.PropertyType)))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Returns a tests state value by examining the relected property type.
        /// </summary>
        /// <param name="pi">Property information of the reflected type.</param>
        /// <returns>The test state value.</returns>
        private static object GetStateForTest(
            PropertyInfo pi)
        {
            object result = null;
            if (typeof(Int16).Equals(pi.PropertyType))
                result = Int16.MinValue;
            else if (typeof(UInt16).Equals(pi.PropertyType))
                result = UInt16.MaxValue;
            else if (typeof(Int32).Equals(pi.PropertyType))
                result = Int32.MaxValue;
            else if (typeof(UInt32).Equals(pi.PropertyType))
                result = UInt32.MaxValue;
            else if (typeof(Int64).Equals(pi.PropertyType))
                result = Int64.MaxValue;
            else if (typeof(UInt64).Equals(pi.PropertyType))
                result = UInt64.MaxValue;
            else if (typeof(Decimal).Equals(pi.PropertyType))
                result = Convert.ToDecimal(Int32.MaxValue);
            else if (typeof(Double).Equals(pi.PropertyType))
                result = Double.MaxValue;
            else if (typeof(Single).Equals(pi.PropertyType))
                result = Single.MaxValue;
            else if (typeof(String).Equals(pi.PropertyType))
                result = "TestString";
            else if (typeof(Boolean).Equals(pi.PropertyType))
                result = default(Boolean);
            else if (typeof(DateTime).Equals(pi.PropertyType))
                result = DateTime.Now.PrecisionSafe();
            else if (typeof(Byte).Equals(pi.PropertyType))
                result = Convert.ToByte(0);
            else if (typeof(SByte).Equals(pi.PropertyType))
                result = Convert.ToByte(0);
            else if (typeof(Char).Equals(pi.PropertyType))
                result = "A";
            else if (typeof(Enum).Equals(pi.PropertyType.BaseType))
                result = 1;
            else if (typeof(Guid).Equals(pi.PropertyType))
            {
                const string guidAsString = "dcac7ddd-29df-47ea-b152-a7207e9c9baa";
                result = new Guid(guidAsString);
            }
            return result;
        }
    }
}