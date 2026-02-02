using System.Runtime.Serialization;
using Keane.CH.Framework.Core.ExtensionMethods;
using Keane.CH.Framework.Services.Resources.Contracts;
using System.Collections.Generic;
using System;
using System.Linq;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Codes.Contracts
{
    /// <summary>
    /// Represents a code item attached to a code type.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class CodeItem : 
        EntityBase
    {
        #region Constructor

        public CodeItem() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The code item id.</param>
        public CodeItem(int id) : base(id)
        {
            Value = id.ToString();
        }

        #endregion Constructor

        #region EntityBase overrides

        /// <summary>
        /// Overrides the state initialisation routine.
        /// </summary>
        public override void InitialiseState()
        {
            base.InitialiseState();
            // TO DO remove.
            base.EntityInfo.EntityTypeId = 6;
        }

        #endregion EntityBase overrides

        #region Properties

        /// <summary>
        /// The id of the parent code type.
        /// </summary>        
        [DataMember()]
        public int RefIdCodeType
        { get; set; }

        /// <summary>
        /// The code item value, e.g. FR.
        /// </summary>        
        [DataMember()]
        public string Value
        { get; set; }

        /// <summary>
        /// The description, e.g. Fribourg.
        /// </summary>        
        public StringResource Description
        { get; set; }

        #region To be removed

        /// <summary>
        /// The description in German.
        /// </summary>        
        [DataMember()]
        public string DescriptionGerman
        { get; set; }

        /// <summary>
        /// The description in French.
        /// </summary>        
        [DataMember()]
        public string DescriptionFrench
        { get; set; }

        /// <summary>
        /// The description in English.
        /// </summary>        
        [DataMember()]
        public string DescriptionEnglish
        { get; set; }

        #endregion To be removed

        /// <summary>
        /// The code item category, e.g. Swiss Canton.
        /// </summary>        
        [DataMember()]
        public string Category
        { get; set; }

        #endregion Properties

        #region Static methods

        /// <summary>
        /// Static methods to filter a list of code types by name.
        /// </summary>
        /// <param name="codeTypelist">The list being filtered.</param>
        /// <param name="codeTypeName">The name of the code type being searched.</param>
        /// <returns></returns>
        public static EntityBaseCollection<CodeItem> GetCollection(
            IEnumerable<CodeItem> codeItemCollection,
            CodeType codeType)
        {
            EntityBaseCollection<CodeItem> result = new EntityBaseCollection<CodeItem>();
            if ((codeItemCollection != null) && (codeType != null))
            {
                List<CodeItem> matches = 
                    codeItemCollection.ToList().FindAll(i => i.RefIdCodeType.Equals(codeType.Id));
                matches.ForEach(i => result.Add(i));
            }
            return result;
        }

        #endregion Static methods
    }
}
