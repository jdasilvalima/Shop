using Shop.Core.LogicMetier;
using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.SQL
{
    //Définir un Context par fonctionnalité
    //Créer l'acces aux données en dernier >> DataAccessInMemory est optionel > permet de fabriquer un squelette pour l'application puis ensuite remplace par la BDD
    //cache > sorte de BDD en mémoire
    //Context zone de mémoire intermédiaire entre la BDD et les controllers

    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal MyContext DataContext;
        internal DbSet<T> dbSet;

        public SQLRepository(MyContext DataContext)
        {
            this.DataContext = DataContext;
            this.dbSet = DataContext.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            //dbSet pointe vers le context
            return dbSet;
        }

        public void Commit()
        {
            //SaveChanges pour sauver dans la BDD >> confirmation pour "écrire" sur la BDD
            DataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var t = FindById(id);
            //Vérifie si l'objet est détaché du Context (n'existe pas dans le contexte)
            if (DataContext.Entry(t).State == EntityState.Detached)
            {
                //Attach charge l'objet dans le context
                dbSet.Attach(t);
            }
            //Remove modifie l'état dans le context > savechanges enclenche les requêtes SQL
            dbSet.Remove(t);
        }

        public T FindById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            //Pour charger un produit dans le context
            dbSet.Attach(t);
            //Regarde dans le context - l'objet qui aura un état modifié sera mis à jour par EF
            DataContext.Entry(t).State = EntityState.Modified;
        }
    }
}
