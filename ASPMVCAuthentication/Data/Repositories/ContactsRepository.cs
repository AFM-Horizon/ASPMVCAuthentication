using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ASPMVCAuthentication.Models;

namespace ASPMVCAuthentication.Data.Repositories
{
    /// <summary>
    /// The <see cref="ContactsRepository"/> Is a repository layer encapsulating database access logic
    /// </summary>
    public class ContactsRepository : IRepository<Contact>, IDisposable
    {
        private readonly ContactDbContext db = new ContactDbContext();

        /// <summary>
        /// Returns All <see cref="Contact"/> objects populated from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await db.Contacts.ToListAsync();
        }

        /// <summary>
        /// Returns a particular <see cref="Contact"/> object by its <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Contact> GetById(long? id)
        {
            return await db.Contacts.FindAsync(id);
        }

        /// <summary>
        /// Creates a new <see cref="Contact"/> object and persists it to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Create(Contact entity)
        {
            db.Contacts.Add(entity);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a <see cref="Contact"/> in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Update(Contact entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a <see cref="Contact"/> object from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Delete(Contact entity)
        {
            db.Contacts.Remove(entity);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a <see cref="Contact"/> object from the database by it's <paramref name="id"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteById(long? id)
        {
            var entityToDelete = db.Contacts.Find(id);
            await Delete(entityToDelete);
        }

        /// <summary>
        /// Allows the user to provide a function returning a <see cref="bool"/> to search
        /// for a particular <see cref="Contact"/> object in the database.  
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Contact>> Find(Expression<Func<Contact, bool>> predicate)
        {
            return await db.Contacts.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Ensures the <see cref="ContactDbContext"/> has been disposed of
        /// after use
        /// </summary>
        public void Dispose()
        {
            db?.Dispose();
        }
    }
}