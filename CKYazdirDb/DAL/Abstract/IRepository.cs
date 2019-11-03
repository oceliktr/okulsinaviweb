﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ODM.CKYazdirDb
{
    interface IRepository<T>
    {
        List<T> List();
        List<T> List(Expression<Func<T, bool>> where);
        IQueryable<T> ListQueryable();
        int Insert(T obj);
        int Update(T obj);
        int Delete(T obj);
        T Find(Expression<Func<T, bool>> where);
        int Save();
    }
}