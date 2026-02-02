using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Keane.CH.Framework.Core.ExtensionMethods;
using Keane.CH.Framework.Core.Utilities.IO;

namespace Keane.CH.Framework.Core.Utilities.Configuration
{
    /// <summary>
    /// Base class for all config managers.
    /// </summary>
    public class ConfigurationManagerBase
    {
        #region Constants

        private const string SEPARATOR = "\\";

        #endregion Constants

        #region Constructors

        protected ConfigurationManagerBase()
        {
            Initialise();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the configuration root folder path.
        /// </summary>
        public string RootFolderPath
        {
            get { return rootFolderPathField; }
            set 
            {
                if (!Directory.Exists(value))
                    throw new ApplicationException(String.Format("RootFolderPath does not exist: {0}"));
                rootFolderPathField = value;
            }
        }
        string rootFolderPathField = String.Empty;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialises the config manager.
        /// </summary>
        public virtual void Initialise()
        { }

        /// <summary>
        /// Derives the configuration root path.
        /// </summary>
        /// <returns>The configuration root path.</returns>
        public string GetRootFolderPath()
        {
            string result = RootFolderPath;
            result += SEPARATOR;
            return result.ParseFilePath();
        }

        /// <summary>
        /// Returns the full sub folder path by suffixing the passed relative folder path ot the root path.
        /// </summary>
        /// <returns>The full sub folder path.</returns>
        public string GetSubFolderPath(string subFolderPath)
        {
            string result = GetRootFolderPath();
            result += subFolderPath;
            result += SEPARATOR;
            return result.ParseFilePath();
        }

        /// <summary>
        /// Returns the full file path by prefixing the config root path and the sub folder path.
        /// </summary>
        /// <param name="relativeSubFolderPath">The relative sub folder path.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns>The full file path.</returns>
        public string GetFilePath(
            string relativeSubFolderPath, string fileName)
        {
            string result = GetSubFolderPath(relativeSubFolderPath);
            result += fileName;
            return result.ParseFilePath();
        }

        /// <summary>
        /// Returns the full file path by prefixing the config root path.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>The full file path.</returns>
        public string GetFilePath(string fileName)
        {
            string result = GetRootFolderPath();
            result += fileName;
            return result.ParseFilePath();
        }
        
        /// <summary>
        /// Returns the passed envionrment variable value.
        /// </summary>
        /// <param name="environmentVariable">The name of the environment variable being retrieved.</param>
        /// <param name="mustExist">Flag indicating whether the variable must exist or not.</param>
        /// <returns>The value of the passed environment value.</returns>
        public string GetEnvironmentVariable(
            string environmentVariable, bool mustExist)
        {
            string result = 
                Environment.GetEnvironmentVariable(environmentVariable);
            if (String.IsNullOrEmpty(result) && mustExist)
            {
                throw new ApplicationException(String.Format("The environment variable {0} has not been initialised.  The environment variable {0} does not exist.", environmentVariable));
            }
            return result;
        }

        #endregion Methods
    }
}
