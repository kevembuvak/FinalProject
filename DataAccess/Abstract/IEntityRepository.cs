using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    // generic constraint
    // class: referans tip
    public interface IEntityRepository<Type> where Type: class, IEntity, new() // IEntity ya da IEntity implementasyonları, ama new() ile beraber yenilenebilir referansları alıyoruz artık bu sayede IEntity interface girilemez
    {
        List<Type> GetAll(Expression<Func<Type, bool>> filter = null); // GetAllByCategory yerine GetAll metodunu linq ile geliştirdik
        Type Get(Expression<Func<Type, bool>> filter);
        void Add(Type entity);
        void Update(Type entity);
        void Delete(Type entity);
     // List<Type> GetAllByCategory(int categoryId);
    }
}
