using System;
using System.Reflection;

namespace Keane.CH.Framework.Core.Utilities.Exceptions
{
    /// <summary>
    /// A function pointer to an exception handler.
    /// </summary>
    /// <param name="fault">The fault that has occurred.</param>
    /// <param name="failingMethod">The method that has failed.</param>
    public delegate void ExceptionHandlerDelegate(Exception fault, MethodBase failingMethod);
}
