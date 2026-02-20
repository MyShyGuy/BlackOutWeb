using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackout.Models.Services
{
    public class UnitOfWork : IDisposable
    {
        private readonly AppDbContext dbc;

        public UnitOfWork(AppDbContext context)
        {
            dbc = context;
        }

        public int Commit()
        {
            return dbc.SaveChanges();
        }

        public void Dispose()
        {
            dbc.Dispose();
        }

        public void RollBack()
        {
            // RollBack: Alle Änderungen verwerfen
            foreach (var entry in dbc.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached; // Änderungen verwerfen
            }
        }
    }
}