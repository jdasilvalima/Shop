﻿using Shop.Core.LogicMetier;
using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        //utilise le cache d'une application dans le DOT.net pour pouvoir mettre en place l'application > sera supprimé lors de l'EF
        //Control pour tester les controllers aussi
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[className] = items;
        }
        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T tToUpdate = items.Find(i => i.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
        public T FindById(int id)
        {
            T t = items.Find(i1 => i1.Id == id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
        public void Delete(int id)
        {
            T tToDelete = items.Find(i => i.Id == id);
            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
    }
}
