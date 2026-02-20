using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Blackout.Models.Services
{
    public class BLService : IBLService
    {
        private readonly AppDbContext dbc;

        public BLService(AppDbContext context)
        {
            dbc = context;
        }

        public Task AddLotAsync(Lot lot)
        {
            dbc.Lots.Add(lot);
            dbc.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddProductAsync(Product product)
        {
            dbc.Products.Add(product);
            dbc.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddUnitAsync(Unit unit)
        {
            dbc.Units.Add(unit);
            dbc.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteLotAsync(int id)
        {
            var lot = dbc.Lots.FirstOrDefault(x => x.LotID == id);
            if (lot != null)
            {
                dbc.Lots.Remove(lot);
                dbc.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task DeleteProductAsync(int id)
        {
            var prodToDelete = dbc.Products.FirstOrDefault(x => x.ProductID == id);
            if (prodToDelete != null)
            {
                dbc.Products.Remove(prodToDelete);
                dbc.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task DeleteUnitAsync(string id)
        {
            var unitToDelete = dbc.Units.FirstOrDefault(x => x.UnitID == id);
            if (unitToDelete != null)            {
                dbc.Units.Remove(unitToDelete);
                dbc.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task<List<Lot>> GetAllLotsAsync()
        {
            return dbc.Lots.Include(x => x.Product).ToListAsync();
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            return dbc.Products.Include(x => x.Unit).Include(x => x.Lots).ToListAsync();
        }

        public Task<List<Unit>> GetAllUnitsAsync()
        {
            return dbc.Units.Include(x => x.Products).ToListAsync();
        }

        public Task<Lot?> GetLotByIdAsync(int id)
        {
            return dbc.Lots.Where(x => x.LotID == id).Include(x => x.Product).FirstOrDefaultAsync();
        }

        public Task<Product?> GetProductByEANAsync(int ean)
        {
            return dbc.Products.Where(x => x.EAN == ean).Include(x => x.Lots).FirstOrDefaultAsync();
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            return dbc.Products.Where(x => x.ProductID == id).FirstOrDefaultAsync();
        }

        public Task<Unit?> GetUnitByIdAsync(string id)
        {
            return dbc.Units.Where(x => x.UnitID == id).FirstOrDefaultAsync();
        }

        public Task UpdateLotAsync(Lot lot)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUnitAsync(Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}