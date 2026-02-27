using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace Blackout.Models.Services
{
    public class BLService(IDbContextFactory<AppDbContext> contextFactory) : IBLService
    {
        // private readonly AppDbContext dbc;

        // public BLService(AppDbContext context)
        // {
        //     dbc = context;
        // }

        public async Task AddLotAsync(LotDTO lot)
        {
            var newlot = new Lot
            {
                LotID = lot.LotID,
                ProductID = lot.ProductID,
                ExpiryDate = lot.ExpiryDate,
                EntryDate = lot.EntryDate,
                Amount = lot.Amount,
                Size = lot.Size,
                Location = lot.Location
            };

            using var dbc = await contextFactory.CreateDbContextAsync();
            dbc.Lots.Add(newlot);
            dbc.SaveChanges();
        }

        public async Task AddnewProductAsync(ProductDTO product)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            Product newProduct = new Product
            {
                ProductID = product.ProductID,
                Title = product.Title,
                Notes = product.Notes,
                EAN = product.EAN,
                UnitID = product.UnitID
            };
            dbc.Products.Add(newProduct);
            dbc.SaveChanges();
        }

        public async Task AddProductAsync(Product product)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            dbc.Products.Add(product);
            dbc.SaveChanges();
        }

        public async Task AddUnitAsync(Unit unit)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            var existingUnit = dbc.Units.FirstOrDefault(x => x.UnitID == unit.UnitID);
            if (existingUnit == null)
            {
                dbc.Units.Add(unit);
                dbc.SaveChanges();
            }
        }

        public async Task DeleteLotAsync(int id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            var lot = dbc.Lots.FirstOrDefault(x => x.LotID == id);
            if (lot != null)
            {
                dbc.Lots.Remove(lot);
                dbc.SaveChanges();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            var prodToDelete = dbc.Products.FirstOrDefault(x => x.ProductID == id);
            if (prodToDelete != null)
            {
                dbc.Products.Remove(prodToDelete);
                dbc.SaveChanges();
            }
        }

        public async Task DeleteUnitAsync(string id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            var unitToDelete = dbc.Units.FirstOrDefault(x => x.UnitID == id);
            if (unitToDelete != null)            {
                dbc.Units.Remove(unitToDelete);
                dbc.SaveChanges();
            }   
        }   

        public async Task<List<Lot>> GetAllLotsAsync()
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Lots.Include(x => x.Product).ThenInclude(x => x.Unit).ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            using var dbc = await contextFactory.CreateDbContextAsync();    
            return await dbc.Products.Include(x => x.Unit).Include(x => x.Lots).ToListAsync();
        }

        public async Task<List<Unit>> GetAllUnitsAsync()
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Units.Include(x => x.Products).ToListAsync();
        }
        
        public async Task<IEnumerable<UnitIdxDTO>> GetAllUnitsIdx()
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            var unit = from u in dbc.Units
                orderby u.UnitID
                select new UnitIdxDTO
                {
                    UnitID = u.UnitID,
                };
            return await unit.ToListAsync();
        }

        public async Task<Lot?> GetLotByIdAsync(int id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Lots.Where(x => x.LotID == id).Include(x => x.Product).FirstOrDefaultAsync();
        }

        public async Task<Product?> GetProductByEANAsync(int ean)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Products.Where(x => x.EAN == ean).Include(x => x.Lots).FirstOrDefaultAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Products.Where(x => x.ProductID == id).Include(x => x.Lots).FirstOrDefaultAsync();
        }

        public async Task<Unit?> GetUnitByIdAsync(string id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Units.Where(x => x.UnitID == id).FirstOrDefaultAsync();
        }

        public async Task UpdateLotAsync(Lot lot)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            var existing = await dbc.Lots.FirstOrDefaultAsync(x => x.LotID == lot.LotID);
            if (existing != null)
            {
                existing.ProductID = lot.ProductID;
                existing.ExpiryDate = lot.ExpiryDate;
                existing.EntryDate = lot.EntryDate;
                existing.Amount = lot.Amount;
                existing.Size = lot.Size;
                existing.Location = lot.Location;

                await dbc.SaveChangesAsync();
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            var existing = await dbc.Products.FirstOrDefaultAsync(x => x.ProductID == product.ProductID);
            if (existing != null)
            {
                existing.Title = product.Title;
                existing.Notes = product.Notes;
                existing.EAN = product.EAN;
                existing.UnitID = product.UnitID;

                await dbc.SaveChangesAsync();
            }
        }

        public async Task UpdateUnitAsync(Unit unit)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            var existingUnit = await dbc.Units
                .FirstOrDefaultAsync(x => x.UnitID == unit.UnitID);

            if (existingUnit != null)
            {
                existingUnit.Notes = unit.Notes;

                await dbc.SaveChangesAsync();
            }
        }
    }
}