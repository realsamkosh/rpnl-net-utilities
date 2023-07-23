using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using RPNL.Net.Utilities.DbContextExtension.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.DbContextExtension.Repository

{
    public abstract class AbstractDbContext : DbContext, IDbContext
    {
        private IDbContextTransaction _transaction;

        #region update
        public AbstractDbContext()
        {

        }
        public AbstractDbContext(DbContextOptions options) : base(options)
        {

        }

        #endregion

        private static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(AbstractDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>(entityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return false;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
         where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted;
            expression = softDeleteFilter;

            return expression;
        }

        protected virtual void ApplyAbpConcepts(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyAbpConceptsForAddedEntity(entry);
                    break;
                case EntityState.Modified:
                    ApplyAbpConceptsForModifiedEntity(entry);
                    break;
                case EntityState.Deleted:
                    ApplyAbpConceptsForDeletedEntity(entry);
                    break;
            }
        }

        protected virtual void ApplyAbpConceptsForAddedEntity(EntityEntry entry)
        {
            SetCreationAuditProperties(entry.Entity);
        }

        protected virtual void ApplyAbpConceptsForModifiedEntity(EntityEntry entry)
        {
            SetModificationAuditProperties(entry.Entity);
            if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
            {
                SetDeletionAuditProperties(entry.Entity);
            }
        }

        protected virtual void ApplyAbpConceptsForDeletedEntity(EntityEntry entry)
        {
            CancelDeletionForSoftDelete(entry);
            SetDeletionAuditProperties(entry.Entity);
        }

        protected virtual void SetCreationAuditProperties(object entityAsObj)
        {
            if (!(entityAsObj is IDateAudit entityWithCreationTime))
                //Object does not implement IHasCreationTime
                return;

            if (entityWithCreationTime.CreatedDate == default)
                entityWithCreationTime.CreatedDate = DateTime.Now;
        }

        protected virtual void SetModificationAuditProperties(object entityAsObj)
        {
            if (entityAsObj is IDateAudit)
                entityAsObj.As<IDateAudit>().LastModified = DateTime.Now;
        }

        protected virtual void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (entry.Entity is ISoftDelete)
                return;

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDelete>().IsDeleted = true;
        }

        protected virtual void SetDeletionAuditProperties(object entityAsObj)
        {
            if (entityAsObj is ISoftDelete)
            {
                var entity = entityAsObj.As<ISoftDelete>();

                if (entity.DeletedOn == null)
                {
                    entity.DeletedOn = DateTime.Now;
                }
            }
        }

        internal int SaveChangesWithoutApplyingAbpConcept()
        {
            try
            {
                var result = base.SaveChanges();
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public override int SaveChanges()
        {
            try
            {
                ApplyAbpConcepts();
                var result = base.SaveChanges();
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                ApplyAbpConcepts();
                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        protected virtual void ApplyAbpConcepts()
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                ApplyAbpConcepts(entry);
            }

        }


        protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                {
                    return _newValue;
                }

                return base.Visit(node);
            }
        }

        public static string GetFileWithName(string filePath)
        {
            var baseDir = $@"{AppDomain.CurrentDomain.BaseDirectory}";

            if (Directory.Exists($"{baseDir}\bin"))
                return $@"{baseDir}\\bin{filePath}";
            else
                return $@"{baseDir}\{filePath}";
        }


        public new DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }
        public void SetAsDetached<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            UpdateEntityState(entity, EntityState.Detached);
        }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public int Commit()
        {
            var saveChanges = SaveChanges();
            _transaction.Commit();
            return saveChanges;
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public async Task<int> CommitAsync()
        {
            var saveChangesAsync = await SaveChangesAsync();
            _transaction.Commit();
            return saveChangesAsync;
        }
        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class, IEntity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }
        private EntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var dbEntityEntry = Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }
        public override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// Create database script
        /// </summary>
        /// <returns>SQL to generate database</returns>
        public string CreateDatabaseScript()
        {
            return string.Empty;
        }


        /// <summary>
        /// Use when Model isn't attach in context
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IEnumerable<TElement> ExecuteSqlQuery<TElement>(string sql, params object[] parameters) where TElement : new()
        {
            return Database.GetModelFromQuery<TElement>(sql, parameters);
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IQueryable<TElement> SqlQuery<TElement>(string sql, params object[] parameters) where TElement : class, IEntity
        {
            return base.Set<TElement>().FromSqlRaw(sql, parameters);
        }

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters)
        {
            var result = Database.ExecuteSqlRaw(sql, parameters);
            return result;
        }

        async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();
    }
}
