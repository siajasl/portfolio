using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Keane.CH.Framework.Core.Utility.Reflection
{
    /// <summary>
    /// Encapsulates a set of reflection utility methods.
    /// </summary>
    public static class ReflectionUtility
    {
        #region Extension methods

        /// <summary>
        /// Determines whether the member supports the passed attribute type.
        /// </summary>
        /// <typeparam name="A">The type of atribute beig interrogated.</typeparam>
        /// <param name="member">A member.</param>
        /// <returns>True if the attribute type is supported.</returns>
        public static bool SupportsAttribute<A>(
            this MemberInfo member)
            where A : System.Attribute
        {
            return
                (member.GetCustomAttributes(typeof(A), true).Length > 0);
        }

        /// <summary>
        /// Returns the first custom attribute that supports the generic attribute type.
        /// </summary>
        /// <typeparam name="A">The type of atribute beig interrogated.</typeparam>
        /// <param name="member">A member.</param>
        /// <returns>A custom attribute of the generic type.</returns>
        public static A GetAttribute<A>(
            this MemberInfo member)
            where A : System.Attribute
        {
            if (member.SupportsAttribute<A>())
                return (A)(member.GetCustomAttributes(typeof(A), true)[0]);
            else
                return null;
        }

        /// <summary>
        /// Returns the first custom attribute that supports the generic attribute type.
        /// </summary>
        /// <typeparam name="A">The type of atribute beig interrogated.</typeparam>
        /// <param name="member">A member.</param>
        /// <returns>A custom attribute of the generic type.</returns>
        public static A[] GetAttributes<A>(
            this MemberInfo member)
            where A : System.Attribute
        {
            List<A> result = new List<A>();
            if (member.SupportsAttribute<A>())
            {
                foreach (Attribute att in member.GetCustomAttributes(typeof(A), true))
                {
                    result.Add((A)att);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Determines whether the member supports the passed attribute type.
        /// </summary>
        /// <typeparam name="A">The type of atribute being filtered.</typeparam>
        /// <param name="properties">A list of properties.</param>
        /// <returns>True if the attribute type is supported.</returns>
        public static PropertyInfo[] FilterByAttribute<A>(
            this PropertyInfo[] properties)
            where A : System.Attribute
        {
            // If any custom attribute if os the passed type then return true.
            List<PropertyInfo> result = new List<PropertyInfo>();
            foreach (PropertyInfo pi in properties)
            {
                if (pi.SupportsAttribute<A>())
                    result.Add(pi);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Searches for the properties of the type that support the generic attribute type.
        /// </summary>
        /// <typeparam name="A">The type of atribute against which to filter the type properties.</typeparam>
        /// <param name="type">The target type.</param>
        /// <returns>An array of properties that support the type of attriute.</returns>
        public static PropertyInfo[] GetPropertiesSupportingAttribute<A>(
            this Type type)
            where A : System.Attribute
        {
            return
                type.GetProperties().FilterByAttribute<A>();
        }

        /// <summary>
        /// Searches for the properties of the type that support the generic attribute type.
        /// </summary>
        /// <typeparam name="A">The type of atribute against which to filter the type properties.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="bindingAttributes">The binding attributes used to filter the type properties.</param>
        /// <returns>An array of properties that support the type of attriute.</returns>
        public static PropertyInfo[] GetPropertiesSupportingAttribute<A>(
            this Type type,
            BindingFlags bindingAttributes)
            where A : System.Attribute
        {
            return
                type.GetProperties(bindingAttributes).FilterByAttribute<A>();
        }

        #endregion Extension methods
    }
}