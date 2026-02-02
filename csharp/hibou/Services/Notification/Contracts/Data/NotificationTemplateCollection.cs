using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Notification.Contracts.Data
{
    /// <summary>
    /// Encapsualtes the collection of templates at use within the security sub-system.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    [Serializable]
    public class NotificationTemplateCollection
    {
        #region Constructor

        public NotificationTemplateCollection()
        {
            InitialiseMembers();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the managed list of templates.
        /// </summary>
        [DataMember()]
        private List<NotificationTemplate> TemplateList
        { get; set; }

        /// <summary>
        /// Gets the count of templtes.
        /// </summary>
        public int TemplateCount
        {
            get 
            { 
                int result = default(int);
                if (TemplateList != null)
                    result = TemplateList.Count;
                return result;
            }
        }


        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a notification template of the matching type.
        /// </summary>
        /// <param name="templateType">A template type.</param>
        /// <returns>A notification template.</returns>
        public NotificationTemplate GetTemplate(NotificationTemplateType templateType)
        {
            return 
                TemplateList.FirstOrDefault(nt => nt.TemplateType.Equals(templateType));
        }

        /// <summary>
        /// Adds a temaplte to the managed collection.
        /// </summary>
        /// <param name="template">A temaplte being added to the collection.</param>
        public void AddTemplate(NotificationTemplate template)
        {
            // Defensive programming.
            if (template == null)
                throw new ArgumentNullException("template");

            // Remove existing if necessary.
            NotificationTemplate existing = GetTemplate(template.TemplateType);
            if (existing != null)
                this.TemplateList.Remove(existing);
            
            // Add template.
            this.TemplateList.Add(template);
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        private void InitialiseMembers()
        {
            TemplateList = new List<NotificationTemplate>();
        }

        #endregion Methods
    }
}