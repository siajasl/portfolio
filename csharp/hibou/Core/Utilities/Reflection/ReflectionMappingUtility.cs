using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Keane.CH.Framework.Core.Utility.Reflection
{
    /// <summary>
    /// Encapsulate object to object mapping.
    /// </summary>
    public static class ReflectionMappingUtility
    {
        /// <summary>
        /// Maps one object to another.
        /// </summary>
        /// <param name="sourceInstance">The mapping source instance.</param>
        /// <param name="targetInstance">The mapping target instance.</param>
        /// <param name="suppressPropertyNotFoundExceptions">Flag indicating whether property not found exceptions are suppressed or not (i.e. the property exists in the source but not in the target).</param>
        public static void Map(
            object sourceInstance, 
            object targetInstance,
            bool suppressPropertyNotFoundExceptions)
        {
            // Defensive programming.
            Debug.Assert(sourceInstance != null, "sourceInstance");
            Debug.Assert(targetInstance != null, "targetInstance");
            
            // Map.
            Type sourceType = sourceInstance.GetType();
            Type targetType = targetInstance.GetType();
            Map(sourceInstance, sourceType, targetInstance, targetType, suppressPropertyNotFoundExceptions);
        }

        /// <summary>
        /// Maps one object to another.
        /// </summary>
        /// <param name="sourceInstance">The mapping source instance.</param>
        /// <param name="sourceType">The mapping source type.</param>
        /// <param name="targetInstance">The mapping target instance.</param>
        /// <param name="suppressPropertyNotFoundExceptions">Flag indicating whether property not found exceptions are suppressed or not (i.e. the property exists in the source but not in the target).</param>
        public static void Map(
            object sourceInstance,
            Type sourceType,
            object targetInstance,
            bool suppressPropertyNotFoundExceptions)
        {
            // Defensive programming.
            Debug.Assert(sourceInstance != null, "sourceInstance");
            Debug.Assert(sourceType != null, "sourceType");
            Debug.Assert(targetInstance != null, "targetInstance");

            // Map.
            Type targetType = targetInstance.GetType();
            Map(sourceInstance, sourceType, targetInstance, targetType, suppressPropertyNotFoundExceptions);
        }

        /// <summary>
        /// Maps one object to another.
        /// </summary>
        /// <param name="sourceInstance">The mapping source instance.</param>
        /// <param name="targetInstance">The mapping target instance.</param>
        /// <param name="targetType">The mapping target type.</param>
        /// <param name="suppressPropertyNotFoundExceptions">Flag indicating whether property not found exceptions are suppressed or not (i.e. the property exists in the source but not in the target).</param>
        public static void Map(
            object sourceInstance,
            object targetInstance, 
            Type targetType,
            bool suppressPropertyNotFoundExceptions)
        {
            // Defensive programming.
            Debug.Assert(sourceInstance != null, "sourceInstance");
            Debug.Assert(targetInstance != null, "targetInstance");
            Debug.Assert(targetType != null, "targetType");

            // Map.
            Type sourceType = sourceInstance.GetType();
            Map(sourceInstance, sourceType, targetInstance, targetType, suppressPropertyNotFoundExceptions);
        }

        /// <summary>
        /// Maps one object to another via the passed types.
        /// </summary>
        /// <param name="sourceInstance">The mapping source instance.</param>
        /// <param name="sourceType">The mapping source type.</param>
        /// <param name="targetInstance">The mapping target instance.</param>
        /// <param name="targetType">The mapping target type.</param>
        /// <param name="suppressPropertyNotFoundExceptions">Flag indicating whether property not found exceptions are suppressed or not (i.e. the property exists in the source but not in the target).</param>
        public static void Map(
            object sourceInstance, 
            Type sourceType,
            object targetInstance,
            Type targetType,
            bool suppressPropertyNotFoundExceptions)
        {
            // Defensive programming.
            Debug.Assert(sourceInstance != null, "sourceInstance");
            Debug.Assert(sourceType != null, "sourceType");
            Debug.Assert(targetInstance != null, "targetInstance");
            Debug.Assert(targetType != null, "targetType");

            // Map using reflection.
            foreach (PropertyInfo piSource in sourceType.GetProperties())
            {
                PropertyInfo piTarget = targetType.GetProperty(piSource.Name);
                if (piTarget != null && 
                    piTarget.PropertyType.Equals(piSource.PropertyType))
                {
                    object sourceValue = piSource.GetValue(sourceInstance, null);
                    if (sourceValue != null)
                    {
                        try
                        {
                            piTarget.SetValue(targetInstance, sourceValue, null);
                        }
                        catch (Exception ex)
                        {
                            if (!suppressPropertyNotFoundExceptions)
                                throw(ex);
                        }
                    }
                }
            }
        }
    }
}