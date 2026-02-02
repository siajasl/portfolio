using System.Linq;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Codes.Contracts
{
    /// <summary>
    /// Represents a code type.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class CodeType : 
        EntityBase
    {
        #region Properties

        /// <summary>
        /// The code type name.
        /// </summary>        
        [DataMember()]
        public string Name
        { get; set; }

        /// <summary>
        /// The code type description.
        /// </summary>        
        [DataMember()]
        public string Description
        { get; set; }

        /// <summary>
        /// Flag indicating whether a the code item category is used.
        /// </summary>        
        [DataMember()]
        public bool UsesCategory
        { get; set; }

        /// <summary>
        /// Flag indicating whether a null code item is allowed.
        /// </summary>        
        [DataMember()]
        public bool AllowNull
        { get; set; }

        /// <summary>
        /// The collection of associated code items.
        /// </summary>
        [DataMember()]
        public EntityBaseCollection<CodeItem> ItemList
        { get; set; }

        #endregion Properties

        #region EntityBase overrides

        /// <summary>
        /// State initialisation routine.
        /// </summary>
        public override void InitialiseState()
        {
            base.InitialiseState();
            ItemList = new EntityBaseCollection<CodeItem>();
        }

        /// <summary>
        /// State change scenrio handler.
        /// </summary>
        protected override void OnIdChange()
        {
            base.OnIdChange();
            if (ItemList != null)
                ItemList.ForEach(i => i.RefIdCodeType = base.Id);
        }

        #endregion EntityBase overrides

        #region Static methods

        /// <summary>
        /// Static methods to filter a list of code types by name.
        /// </summary>
        /// <param name="codeTypelist">The list being filtered.</param>
        /// <param name="codeItemId">The id of the code item being searched.</param>
        /// <returns>a code item if found.</returns>
        public static CodeItem GetCodeItem(
            IEnumerable<CodeType> codeTypelist,
            int codeItemId)
        {
            CodeItem result = null;
            if (codeTypelist != null)
            {
                foreach (CodeType codeType in codeTypelist)
                {
                    foreach (CodeItem codeItem in codeType.ItemList)
                    {
                        if (codeItem.Id.Equals(codeItemId))
                        {
                            result = codeItem;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Static method to associate a collection of code items with a collection of code types.
        /// </summary>
        /// <param name="codeTypeCollection">The collection of code types.</param>
        /// <param name="codeItemCollection">The collection of code items.</param>
        public static void AssociateCollections(
            IEnumerable<CodeType> codeTypeCollection,
            IEnumerable<CodeItem> codeItemCollection)
        {
            // Exit if either of the collections are null.
            if ((codeTypeCollection == null) ||
                (codeItemCollection == null))
                return;

            // Associate the corresponding collection of code items.
            foreach (CodeType codeType in codeTypeCollection)
            {
                codeType.ItemList =
                    CodeItem.GetCollection(codeItemCollection, codeType);
            }
        }

        #endregion Static methods
    }
}