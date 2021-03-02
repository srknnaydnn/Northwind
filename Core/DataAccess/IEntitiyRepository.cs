﻿
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //generic constraint
    //class:referans tip
    //IEntitiy olabilir yada IEntitiyden implemente bir sınınıf olabilir

   public interface IEntitiyRepository<T> where T:class,IEntitiy,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entitiy);
        void Delete(T entitiy);
        void Update(T entitiy);
      
    }
}
