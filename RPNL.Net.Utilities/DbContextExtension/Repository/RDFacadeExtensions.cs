﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.DbContextExtension.Repository

{
    public static class RDFacadeExtensions
    {
        public static IEnumerable<T> GetModelFromQuery<T>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
            where T : new()
        {
            using DbDataReader dr = databaseFacade.ExecuteSqlQuery(sql, parameters).DbDataReader;
            List<T> lst = new List<T>();
            PropertyInfo[] props = typeof(T).GetProperties();
            while (dr.Read())
            {
                T t = new T();
                IEnumerable<string> actualNames = dr.GetColumnSchema().Select(o => o.ColumnName);
                if (t.GetType().IsGenericType && t.GetType().GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    PropertyInfo pi = props.First(x => x.CanWrite == true);
                    for (int i = 0; i < dr.FieldCount; ++i)
                    {
                        if (dr.GetValue(i) != DBNull.Value)
                        {
                            pi.SetValue(t, dr.GetValue(i), new[] { dr.GetColumnSchema()[i].ColumnName });
                        }
                    }
                    lst.Add(t);
                }
                else
                {
                    for (int i = 0; i < props.Length; ++i)
                    {
                        PropertyInfo pi = props[i];

                        if (!pi.CanWrite) continue;

                        System.ComponentModel.DataAnnotations.Schema.ColumnAttribute ca = pi.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute)) as System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;
                        string name = ca?.Name ?? pi.Name;

                        if (pi == null) continue;

                        if (!actualNames.Contains(name))
                        {
                            continue;
                        }
                        object value = dr[name];
                        Type pt = pi.DeclaringType;
                        bool nullable = pt.GetTypeInfo().IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>);
                        if (value == DBNull.Value)
                        {
                            value = null;
                        }
                        if (value == null && pt.GetTypeInfo().IsValueType && !nullable)
                        {
                            value = Activator.CreateInstance(pt);
                        }
                        pi.SetValue(t, value);
                    }//for i

                }
                lst.Add(t);
            }//while
            return lst;
            //using dr
        }

        public static RelationalDataReader ExecuteSqlQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();

            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade
                    .GetService<IRawSqlCommandBuilder>()
                    .Build(sql, parameters);

                return rawSqlCommand
                    .RelationalCommand
                    .ExecuteReader(new RelationalCommandParameterObject(connection: databaseFacade.GetService<IRelationalConnection>()
                    , parameterValues: rawSqlCommand.ParameterValues, null, null, null));
            }
        }

        public static async Task<RelationalDataReader> ExecuteSqlCommandAsync(this DatabaseFacade databaseFacade, string sql, CancellationToken cancellationToken = default,
                                                             params object[] parameters)
        {
            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();

            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade
                    .GetService<IRawSqlCommandBuilder>()
                    .Build(sql, parameters);

                return await rawSqlCommand
                    .RelationalCommand
                   .ExecuteReaderAsync(
                    new RelationalCommandParameterObject(connection: databaseFacade.GetService<IRelationalConnection>()
                    , parameterValues: rawSqlCommand.ParameterValues, null, null, null), cancellationToken);
            }
        }
    }
}
