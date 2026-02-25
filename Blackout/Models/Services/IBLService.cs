using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blackout.Models.DTO;

namespace Blackout.Models.Services
{
    public interface IBLService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id); 
        Task<Product?> GetProductByEANAsync(int ean);
        Task AddnewProductAsync(ProductDTO product);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

        Task<List<Lot>> GetAllLotsAsync();
        Task<Lot?> GetLotByIdAsync(int id);
        Task AddLotAsync(LotDTO lot);
        Task UpdateLotAsync(Lot lot);
        Task DeleteLotAsync(int id);

        Task<List<Unit>> GetAllUnitsAsync();
        Task<IEnumerable<UnitIdxDTO>> GetAllUnitsIdx();
        Task<Unit?> GetUnitByIdAsync(string id);
        Task AddUnitAsync(Unit unit);
        Task UpdateUnitAsync(Unit unit);
        Task DeleteUnitAsync(string id);
    }
}