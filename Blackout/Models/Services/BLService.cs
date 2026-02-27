using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace Blackout.Models.Services
{
    /// <summary>
        /// Business logic service providing CRUD operations for lots, products, and units.
        /// Implements <see cref="IBLService"/> and uses a DbContext factory for scoped access.
        /// </summary>
        public class BLService(IDbContextFactory<AppDbContext> contextFactory) : IBLService
    {
        // private readonly AppDbContext dbc;

        // public BLService(AppDbContext context)
        // {
        //     dbc = context;
        // }

        /// <summary>
        /// Insert a new lot record into the database and return its generated ID.
        /// </summary>
        /// <param name="lot">Data transfer object representing the lot to create.</param>
        public async Task<int> AddLotAsync(LotDTO lot)
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
            await dbc.SaveChangesAsync();
            return newlot.LotID;
        }

        /// <summary>
        /// Create a new product entry and return the new product's ID.
        /// </summary>
        /// <param name="product">Product data transfer object.</param>
        public async Task<int> AddnewProductAsync(ProductDTO product)
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
            return newProduct.ProductID;
        }

        /// <summary>
        /// Add a product entity directly (ID must be set by caller or default).
        /// </summary>
        public async Task AddProductAsync(Product product)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            dbc.Products.Add(product);
            dbc.SaveChanges();
        }

        /// <summary>
        /// Add a unit if it doesn't already exist in the database.
        /// </summary>
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

        /// <summary>
        /// Delete the lot with the specified ID if found.
        /// </summary>
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

        /// <summary>
        /// Remove a product by its ID.
        /// </summary>
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

        /// <summary>
        /// Delete a unit given its string identifier.
        /// </summary>
        public async Task DeleteUnitAsync(string id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            var unitToDelete = dbc.Units.FirstOrDefault(x => x.UnitID == id);
            if (unitToDelete != null)            {
                dbc.Units.Remove(unitToDelete);
                dbc.SaveChanges();
            }   
        }   

        /// <summary>
        /// Retrieve a list of all lots, including related product and unit data.
        /// </summary>
        public async Task<List<Lot>> GetAllLotsAsync()
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Lots.Include(x => x.Product).ThenInclude(x => x.Unit).ToListAsync();
        }

        /// <summary>
        /// Retrieve every product along with its unit and lots.
        /// </summary>
        public async Task<List<Product>> GetAllProductsAsync()
        {
            using var dbc = await contextFactory.CreateDbContextAsync();    
            return await dbc.Products.Include(x => x.Unit).Include(x => x.Lots).ToListAsync();
        }

        /// <summary>
        /// Get all units and the products associated with each.
        /// </summary>
        public async Task<List<Unit>> GetAllUnitsAsync()
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Units.Include(x => x.Products).ToListAsync();
        }
        
        /// <summary>
        /// Return a lightweight list of units used for index/select controls.
        /// </summary>
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

        /// <summary>
        /// Find a lot by its integer ID, including product details.
        /// </summary>
        public async Task<Lot?> GetLotByIdAsync(int id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Lots.Where(x => x.LotID == id).Include(x => x.Product).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Look up a product by its EAN code, including lots.
        /// </summary>
        public async Task<Product?> GetProductByEANAsync(int ean)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Products.Where(x => x.EAN == ean).Include(x => x.Lots).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieve a product by ID along with its lots.
        /// </summary>
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Products.Where(x => x.ProductID == id).Include(x => x.Lots).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Fetch a unit record by its string identifier.
        /// </summary>
        public async Task<Unit?> GetUnitByIdAsync(string id)
        {
            using var dbc = await contextFactory.CreateDbContextAsync();
            return await dbc.Units.Where(x => x.UnitID == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update an existing lot's details if it exists.
        /// </summary>
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

        /// <summary>
        /// Update an existing product record.
        /// </summary>
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

        /// <summary>
        /// Update notes or attributes of a unit.
        /// </summary>
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