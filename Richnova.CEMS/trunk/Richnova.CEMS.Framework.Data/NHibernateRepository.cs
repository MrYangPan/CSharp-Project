using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Richnova.CEMS.Framework.Utility.Linq;
using Spring.Data.NHibernate.Support;

namespace Richnova.CEMS.Framework.Data
{
    /// <summary>
    /// NHibernate 仓储类
    /// </summary>
    public class NHibernateRepository : HibernateDaoSupport
    {
        #region Session
        public ISession CurrentSession
        {
            get
            {
                if (NHibernate.Context.CurrentSessionContext.HasBind(SessionFactory))
                   return SessionFactory.GetCurrentSession();
                var session = SessionFactory.OpenSession();
                NHibernate.Context.CurrentSessionContext.Bind(session);
                return session;
            }
        }

        /// <summary>
        /// 强制同步模式
        /// </summary>
        public FlushMode FlushMode
        {
            get { return CurrentSession.FlushMode; }
            set { CurrentSession.FlushMode = value; }
        }

        /// <summary>
        /// 强制同步
        /// </summary>
        public void Flush()
        {
            CurrentSession.Flush();
        }

        /// <summary>
        /// 清空一级缓存
        /// </summary>
        public void Clear()
        {
            CurrentSession.Clear();
        }

        /// <summary>
        /// 清除指定对象的以及缓存
        /// </summary>
        public void Evict<T>(T obj)
        {
            CurrentSession.Evict(obj);
        }

        #endregion

        #region Transaction Operation
        /// <summary>
        /// 开始一个事务
        /// </summary>
        public void BeginTransaction()
        {
            CurrentSession.Transaction.Begin();
        }

        /// <summary>
        /// 开始一个事务，指定隔离级别
        /// </summary>
        /// <param name="isolationLevel">事务的隔离级别</param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            CurrentSession.Transaction.Begin(isolationLevel);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            if (CurrentSession.Transaction != null && !CurrentSession.Transaction.WasCommitted)
                CurrentSession.Transaction.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            if (CurrentSession.Transaction != null && !CurrentSession.Transaction.WasRolledBack)
                CurrentSession.Transaction.Rollback();
        }
        #endregion

        #region ICriteria Operation
        /// <summary>
        /// 创建过滤条件
        /// </summary>
        public ICriteria CreateCriteria<T>() where T : NHibernateEntity
        {
            var criteria = CurrentSession.CreateCriteria(typeof(T));
            return criteria;
        }
        #endregion

        #region Total Count Queries
        /// <summary>
        /// 获取记录的总数
        /// </summary>
        public int Count<T>() where T : NHibernateEntity
        {
            return CurrentSession.Linq<T>().Count(t => !t.IsDeleted);
        }

        /// <summary>
        /// 获取记录的总数(带条件)
        /// </summary>
        public int Count<T>(ICriteria criteria) where T : NHibernateEntity
        {
            return CurrentSession.Linq<T>(criteria).Count(t => t.IsDeleted == false);
        }

        public int Count<T>(Expression<Func<T, bool>> expression) where T : NHibernateEntity
        {
            return CurrentSession.Linq<T>().Count(expression.And(t => t.IsDeleted == false));
        }

        #endregion

        #region Some one Entity Queries
        /// <summary>
        /// 根据Id获取对象
        /// </summary>
        public T Load<T>(object id) where T : NHibernateEntity
        {
            try
            {
                var t = CurrentSession.Load<T>(id);
                return t.IsDeleted ? null : t;
            }
            catch (ObjectNotFoundException)
            {
                return null;
            }
        }
        #endregion

        #region Simgle Queries
        /// <summary>
        /// 查询所有
        /// </summary>
        public IList<T> GetList<T>() where T : NHibernateEntity
        {
            return (from t in CurrentSession.Linq<T>() where t.IsDeleted == false select t).ToList();
        }

        /// <summary>
        /// 查询所有(带Criteria)
        /// </summary>
        public IList<T> GetList<T>(ICriteria criteria) where T : NHibernateEntity
        {
            return (from t in CurrentSession.Linq<T>(criteria) where t.IsDeleted == false select t).ToList();
        }
        /// <summary>
        /// 查询所有(带条件Lambda)
        /// </summary>
        public IList<T> GetList<T>(Expression<Func<T, bool>> where) where T : NHibernateEntity
        {
            Expression<Func<T, bool>> dExp = t => t.IsDeleted == false;
            return CurrentSession.Linq<T>().Where(dExp.And(where)).ToList();
        }
        /// <summary>
        /// 查询所有(带条件Lambda,带排序Lambda)
        /// </summary>
        public IList<T> GetOrderedList<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] orders) where T : NHibernateEntity
        {
            Expression<Func<T, bool>> dExp = t => t.IsDeleted == false;
            var query = CurrentSession.Linq<T>().Where(dExp.And(where));
            query = orders.Aggregate(query, (current, order) => current.OrderBy(order));
            return query.ToList();
        }

        /// <summary>
        /// 返回一个IQueryable对象，提供更灵活的查询
        /// </summary>
        public IQueryable<T> GetQueryable<T>() where T : NHibernateEntity
        {
            return CurrentSession.Linq<T>().Where(t => t.IsDeleted == false);
        }

        #endregion

        #region  Paged and Sorted Complex Queries
        /// <summary>
        /// 分页查询
        /// </summary>
        public IList<T> GetPage<T>(ICriteria criteria, int pageSize, int pageIndex, out int total) where T : NHibernateEntity
        {
            var linq = CurrentSession.Linq<T>(criteria).Where(t => t.IsDeleted == false);
            total = linq.Count();
            return linq.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 分页、排序查询
        /// </summary>
        public IList<T> GetPage<T>(ICriteria criteria, int pageSize, int pageIndex, out int total, string orderBy, string orderSort) where T : NHibernateEntity
        {
            criteria.AddOrder(orderSort.ToUpper() == "ASC" ? Order.Asc(orderBy) : Order.Desc(orderBy));
            var linq = CurrentSession.Linq<T>(criteria).Where(t => t.IsDeleted == false);
            total = linq.Count();
            return linq.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            //total = Count<T>();
            //criteria.Add(Restrictions.Eq("IsDisused", false));
            //criteria.AddOrder(orderSort.ToUpper() == "ASC" ? Order.Asc(orderBy) : Order.Desc(orderBy));
            //criteria.SetFirstResult((pageIndex - 1) * pageSize);
            //criteria.SetMaxResults(pageSize);
            //return criteria.List<T>();
        }
        #endregion

        #region HQL Execute
        /// <summary>
        /// 执行Hql查询语句
        /// </summary>
        public IList<T> ExecuteHql<T>(string hql) where T : NHibernateEntity
        {
            var query = CurrentSession.CreateQuery(hql);
            return query.List<T>();
        }

        /// <summary>
        /// 执行Hql分页查询语句
        /// </summary>
        public IList<T> ExecuteHql<T>(string hql, int pageSize, int pageIndex)
        {
            var query = CurrentSession.CreateQuery(hql);
            query.SetFirstResult((pageIndex - 1) * pageSize);
            query.SetMaxResults(pageSize);
            return query.List<T>();
        }

        /// <summary>
        /// 执行HQL查询，返回1条记录
        /// </summary>
        public T ExecuteScalarHql<T>(string hql)
        {
            var query = CurrentSession.CreateQuery(hql);
            return query.UniqueResult<T>();
        }
        /// <summary>
        ///  执行HQL语句
        /// </summary>
        public void ExecuteHql(string hql)
        {
            var query = CurrentSession.CreateQuery(hql);
            query.ExecuteUpdate();
        }

        #endregion

        #region SQL Execute
        /// <summary>
        /// 执行SQL查询语句
        /// </summary>
        public IList ExecuteSql(string sql)
        {
            var query = CurrentSession.CreateSQLQuery(sql);
            return query.List();
        }

        /// <summary>
        /// 执行SQL查询语句
        /// </summary>
        public IList<T> ExecuteSql<T>(string sql) where T : NHibernateEntity
        {
            var query = CurrentSession.CreateSQLQuery(sql).AddEntity(typeof(T));
            return query.List<T>();
        }

        /// <summary>
        /// 执行SQL查询语句，返回1条记录
        /// </summary>
        public object ExecuteScalarSql(string sql)
        {
            var query = CurrentSession.CreateSQLQuery(sql);
            return query.UniqueResult();
        }

        /// <summary>
        /// 执行SQL查询语句，返回1条记录
        /// </summary>
        public T ExecuteScalarSql<T>(string sql) where T : NHibernateEntity
        {
            var query = CurrentSession.CreateSQLQuery(sql).AddEntity(typeof(T));
            return query.UniqueResult<T>();
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        public int ExecuteNonQuery(string sql)
        {
            var transaction = CurrentSession.BeginTransaction();
            try
            {
                var command = CurrentSession.Connection.CreateCommand();
                command.CommandText = sql;
                transaction.Enlist(command);
                var count = command.ExecuteNonQuery();
                transaction.Commit();
                return count;
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                return 0;
            }
        }

        #endregion

        #region Delete Operation
        /// <summary>
        /// 物理删除数据
        /// </summary>
        public void PhysicsDelete<T>(object id) where T : NHibernateEntity
        {
            var t = CurrentSession.Load<T>(id);
            CurrentSession.Delete(t);
            CurrentSession.Flush();
        }

        /// <summary>
        /// 物理删除数据(清空表，慎用，切勿用于清空大表)
        /// </summary>
        public void PhysicsDelete<T>() where T : NHibernateEntity
        {
            var ls = CurrentSession.CreateCriteria(typeof(T)).List<T>();
            foreach (var t in ls)
            {
                CurrentSession.Delete(t);
            }
            CurrentSession.Flush();
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public void LogicDelete<T>(object id) where T : NHibernateEntity
        {
            var t = CurrentSession.Load<T>(id);
            if (t == null || t.IsDeleted) return;
            t.IsDeleted = true;
            t.DeletedAt = DateTime.Now;
            CurrentSession.Update(t);
            CurrentSession.Flush();
        }

        /// <summary>
        /// 逻辑删除(慎用，切勿用于清空大表)
        /// </summary> 
        public void LogicDelete<T>() where T : NHibernateEntity
        {
            var ls = GetList<T>();
            foreach (var t in ls)
            {
                if (t == null || t.IsDeleted) continue;
                t.IsDeleted = true;
                CurrentSession.Update(t);
            }
            CurrentSession.Flush();
        }
        #endregion

        #region Save or Update Operation
        /// <summary>
        /// 保存
        /// </summary>
        public void Save<T>(T obj) where T : NHibernateEntity
        {
            obj.IsDeleted = false;
            obj.CreatedAt = DateTime.Now;
            CurrentSession.Save(obj);
            CurrentSession.Flush();
        }

        /// <summary>
        /// 更新或者新增
        /// </summary>
        public Guid SaveOrUpdateRetrunId<T>(T obj) where T : NHibernateEntity
        {
            if (obj.Id.HasValue)
                obj.UpdatedAt = DateTime.Now;
            else
                obj.CreatedAt = DateTime.Now;
            CurrentSession.SaveOrUpdate(obj);
            CurrentSession.Flush();
            return obj.Id.GetValueOrDefault();
        }

        /// <summary>
        /// 更新或者新增
        /// </summary>
        public void SaveOrUpdate<T>(T obj) where T : NHibernateEntity
        {
            if (obj.Id.HasValue )
                obj.UpdatedAt = DateTime.Now;
            else
                obj.CreatedAt = DateTime.Now;
            CurrentSession.SaveOrUpdate(obj);
            CurrentSession.Flush();
        }

        /// <summary>
        /// 有相同属性对象则忽略，没有则保存
        /// </summary>
        public void Merge<T>(T obj) where T : NHibernateEntity
        {
            CurrentSession.Merge(obj);
            CurrentSession.Flush();
        }

        /// <summary>
        /// 持久对象
        /// </summary>
        public void Persist<T>(T obj) where T : NHibernateEntity
        {
            CurrentSession.Persist(obj);
        }

        /// <summary>
        /// 修改
        /// </summary>
        public void Update<T>(T obj) where T : NHibernateEntity
        {
            obj.UpdatedAt = DateTime.Now;
            CurrentSession.Update(obj);
            CurrentSession.Flush();
        }
        #endregion

    }
}