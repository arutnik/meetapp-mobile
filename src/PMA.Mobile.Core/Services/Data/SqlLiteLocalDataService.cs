using PMA.Mobile.Core.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMA.Mobile.Core.Utility;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using PMA.Mobile.Core.Interfaces.Core;
using PMA.Mobile.Core.Models;
using System.Threading;
using System.Linq.Expressions;


namespace PMA.Mobile.Core.Services.Data
{
	public class SqlLiteLocalDataService : ILocalData, IApplicationService
    {
        const string MainDbName = "PMA_appdata";

        readonly ISQLiteConnectionFactory _connectionFactory;
        readonly IReflectionService _reflectionService;

        ISQLiteConnection _connection;
        OneTimeAwaitable _initializer;

        ReaderWriterLockSlim _readerWriterLock;

        public SqlLiteLocalDataService(ISQLiteConnectionFactory connectionFactory
            , IReflectionService reflectionService)
        {
            _connectionFactory = connectionFactory;
            _reflectionService = reflectionService;
            _initializer = new OneTimeAwaitable(InitializeInternal);

            _readerWriterLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        public Task Initialize()
        {
            return _initializer.Run();
        }

        protected virtual async Task InitializeInternal()
        {
			await Task.Delay (5000);

            _connection = _connectionFactory.Create(MainDbName);

            if (_connection == null)
            {
                throw new InvalidOperationException("Could not connect to sqllite database");
            }

            var tableTypes = _reflectionService.GetAllTypesAssignableTo(typeof(DataRecord));

            foreach (var table in tableTypes)
            {
                _connection.CreateTable(table);
            }
        }

        public T FindOne<T>(Expression<Func<T, bool>> predicate) where T : DataRecord, new()
        {
            try
            {
                _readerWriterLock.EnterReadLock();

                return _connection.Find<T>(predicate);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }

            return null;
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : DataRecord, new()
        {
            try
            {
                _readerWriterLock.EnterReadLock();

                var result = _connection.Table<T>();

                var where = result.Where(predicate);

                return where.ToArray();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }

            return Enumerable.Empty<T>();
        }

        public IEnumerable<T> All<T>() where T : DataRecord, new()
        {
            try
            {
                _readerWriterLock.EnterReadLock();

                var result = _connection.Table<T>();

                return result.ToArray();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }

            return Enumerable.Empty<T>();
        }

        public void Save<T>(IEnumerable<T> items) where T : DataRecord, new()
        {
            bool inTrans = false;
            bool rollback = false;
            try
            {
                _readerWriterLock.EnterWriteLock();

                _connection.BeginTransaction();
                inTrans = true;

                foreach (var item in items)
                {
                    _connection.InsertOrReplace(item);
                }

                _connection.Commit();
            }
            catch (Exception ex)
            {
                rollback = inTrans;
            }
            finally
            {
                if (rollback)
                {
                    try
                    {
                        _connection.Rollback();
                    }
                    catch (Exception rex)
                    {

                    }
                }
                _readerWriterLock.ExitWriteLock();
            }
        }

        public void Remove<T>(IEnumerable<T> items) where T : DataRecord, new()
        {
            bool inTrans = false;
            bool rollback = false;
            try
            {
                _readerWriterLock.EnterWriteLock();

                _connection.BeginTransaction();
                inTrans = true;

                foreach (var item in items)
                {
                    _connection.Delete<T>(item.PrimaryKey);
                }

                _connection.Commit();
            }
            catch (Exception ex)
            {
                rollback = inTrans;
            }
            finally
            {
                if (rollback)
                {
                    try
                    {
                        _connection.Rollback();
                    }
                    catch (Exception rex)
                    {

                    }
                }
                _readerWriterLock.ExitWriteLock();
            }
        }


        public void RemoveAll<T>() where T : DataRecord, new()
        {
            try
            {
                _readerWriterLock.EnterWriteLock();

                _connection.DeleteAll<T>();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _readerWriterLock.ExitWriteLock();
            }
        }
    }
}
