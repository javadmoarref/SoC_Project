﻿using System.Linq.Expressions;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Infrastructure;

public class RepositoryBase<TKey,T>:IRepository<TKey,T> where T:class
{
    private readonly DbContext _context;

    public RepositoryBase(DbContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Add<T>(entity);
    }

    public T Get(TKey id)
    {
        return _context.Find<T>(id);
    }

    public List<T> Get()
    {
        return _context.Set<T>().ToList();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public bool Exists(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Any(expression);
    }


    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}