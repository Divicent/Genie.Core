using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Genie.Core.Extensions;
using Genie.Core.Infrastructure.Interfaces;
using Genie.Core.Infrastructure.Models;

namespace Genie.Core.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
		private IProcedureContainer _procedureContainer;

        private readonly List<IOperation> _operations;
        private readonly HashSet<BaseModel> _objects;

		public IProcedureContainer Procedures { get { return _procedureContainer ?? ( _procedureContainer = new ProcedureContainer(Context)); } }

        private IDBContext Context { get;}
        private IDbTransaction Transaction { get; set; }

        public UnitOfWork(IDBContext context)
        {
            Context = context;
            _objects = new HashSet<BaseModel>();
            _operations = new List<IOperation>();
        }
            
        public IDbTransaction BeginTransaction()
        {
            if (Transaction != null)
            {
                throw new NullReferenceException("Not finished previous transaction");
            }
            Transaction = Context.Connection.BeginTransaction();
            return Transaction;
        }


        public void Commit()
        {
            var updated = _objects.Where(o => o.UpdatedProperties != null && o.UpdatedProperties.Count > 0).ToList();

            try
            {
                if (updated.Count > 0)
                    _operations.AddRange(updated.Select(u => new Operation(OperationType.Update, u)));

                if (_operations.Count > 0)
                {
                    var toAdd = _operations.Where(o => o.Type == OperationType.Add).ToList();
                    var toDelete = _operations.Where(o => o.Type == OperationType.Remove).ToList();
                    var toUpdate = _operations.Where(o => o.Type == OperationType.Update).ToList();

                    var connection = Context.Connection;

					if (toDelete.Count > 0)
					{
                    	foreach (var operation in toDelete)
						{
                            var deleted = connection.Delete(operation.Object);
                            if (deleted) { operation.Object.DatabaseModelStatus = ModelStatus.Deleted; }
                        }
					}

					if (toAdd.Count > 0)
					{
                        foreach (var operation in toAdd)
                        {
                            var newId = connection.Insert(operation.Object);
                             if(newId != null)
                                operation.Object.SetId((int)newId);
                            operation.Object.DatabaseModelStatus = ModelStatus.Retrieved;
                            if (operation.Object.ActionsToRunWhenAdding != null && operation.Object.ActionsToRunWhenAdding.Count > 0)
                            {
                                foreach (var addAction in operation.Object.ActionsToRunWhenAdding)
                                    addAction.Run();
                                operation.Object.ActionsToRunWhenAdding.Clear();
                            }
                        }
                    }

					if (toUpdate.Count > 0)
					{
						foreach (var operation in toUpdate)
						{
                            connection.Update(operation.Object);
                            operation.Object.UpdatedProperties.Clear();
                        }
					}
                    
					_operations.Clear();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to commit changes", e);
            }
        }

        public async Task CommitAsync()
        {
            var updated = _objects.Where(o => o.UpdatedProperties != null && o.UpdatedProperties.Count > 0).ToList();

            try
            {
                if (updated.Count > 0)
                    _operations.AddRange(updated.Select(u => new Operation(OperationType.Update, u)));

                if (_operations.Count > 0)
                {
                    var toAdd = _operations.Where(o => o.Type == OperationType.Add).ToList();
                    var toDelete = _operations.Where(o => o.Type == OperationType.Remove).ToList();
                    var toUpdate = _operations.Where(o => o.Type == OperationType.Update).ToList();

                    var connection = Context.Connection;

					if (toDelete.Count > 0)
					{
                    	foreach (var operation in toDelete)
						{
                            var deleted = await connection.DeleteAsync(operation.Object);
                            if (deleted) { operation.Object.DatabaseModelStatus = ModelStatus.Deleted; }
                        }
					}

					if (toAdd.Count > 0)
					{
                        foreach (var operation in toAdd)
                        {
                            var newId = await connection.InsertAsync(operation.Object);
                             if(newId != null)
                                operation.Object.SetId((int)newId);
                            operation.Object.DatabaseModelStatus = ModelStatus.Retrieved;
                            if (operation.Object.ActionsToRunWhenAdding != null && operation.Object.ActionsToRunWhenAdding.Count > 0)
                            {
                                foreach (var addAction in operation.Object.ActionsToRunWhenAdding)
                                    addAction.Run();
                                operation.Object.ActionsToRunWhenAdding.Clear();
                            }
                        }
                    }

					if (toUpdate.Count > 0)
					{
						foreach (var operation in toUpdate)
						{
                            await connection.UpdateAsync(operation.Object);
                            operation.Object.UpdatedProperties.Clear();
                        }
					}
                    
					_operations.Clear();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to commit changes", e);
            }
        }


        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
            }
        }

        public void AddOp(IOperation operation)
        {
            _operations.Add(operation);
        }

        public void AddObj(BaseModel obj)
        {
            _objects.Add(obj);
        }    
    }
}
