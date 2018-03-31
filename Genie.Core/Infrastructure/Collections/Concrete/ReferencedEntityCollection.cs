using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Genie.Core.Infrastructure.Actions.Abstract;
using Genie.Core.Infrastructure.Actions.Concrete;
using Genie.Core.Infrastructure.Collections.Abstract;
using Genie.Core.Infrastructure.Models;

namespace Genie.Core.Infrastructure.Collections.Concrete
{
    internal class ReferencedEntityCollection<T> : IReferencedEntityCollection<T> where T: BaseModel
	{
		private readonly List<T> _collection;
		private readonly Action<object> _addAction;
        private readonly BaseModel _creator;

		internal ReferencedEntityCollection(IEnumerable<T> collection, Action<object> addAction, BaseModel creator)
		{
			_collection = collection.ToList();
			_addAction = addAction;
            _creator = creator;
		}

		public void Add(T entityToAdd) 
		{
            if (entityToAdd == null)
                return;
            switch (_creator.DatabaseModelStatus)
            {
                case ModelStatus.Retrieved:
                    _addAction(entityToAdd);
                    break;
                case ModelStatus.ToAdd:
                    if(_creator.ActionsToRunWhenAdding == null)
                        _creator.ActionsToRunWhenAdding = new List<IAddAction>();
                    _creator.ActionsToRunWhenAdding.Add(new AddAction(_addAction, entityToAdd));
                    break;
                case ModelStatus.JustInMemory:
                case ModelStatus.Deleted:
                    break;
                default:
                    break;
            }    
			_collection.Add(entityToAdd);
		}

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
} 


