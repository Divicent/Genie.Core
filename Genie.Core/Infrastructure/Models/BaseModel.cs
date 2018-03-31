using System.Collections.Generic;
using Genie.Core.Infrastructure.Actions.Abstract;
using Genie.Core.Infrastructure.Interfaces;

namespace Genie.Core.Infrastructure.Models
{
    internal enum ModelStatus
    {
        JustInMemory = 1,
        Retrieved = 2,
        Deleted = 3,
        ToAdd = 4
    }

    public abstract class BaseModel
    {
        internal HashSet<string> UpdatedProperties { get; set; }
        internal ModelStatus DatabaseModelStatus { get; set; }
        internal IUnitOfWork DatabaseUnitOfWork { get; set; }
        internal List<IAddAction> ActionsToRunWhenAdding { get; set; } 
        internal abstract void SetId(object id);

		/// <summary>
        /// Checks the status of the object , and registers as updated property
        /// </summary>
        /// <param name="propertyName">The updated property name</param>
		protected void __Updated(string propertyName) 
		{
		    if(UpdatedProperties == null)
                UpdatedProperties = new HashSet<string>();

			if( DatabaseModelStatus == ModelStatus.Retrieved ) 
				UpdatedProperties.Add(propertyName);
		}
    }
}

