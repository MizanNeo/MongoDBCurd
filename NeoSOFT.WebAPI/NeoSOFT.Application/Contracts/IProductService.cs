using NeoSOFT.Common.Classes;
using NeoSOFT.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSOFT.Application.Contracts
{
    public interface IProductService
    {
        Task<ApiResponse<List<ProductDto>>> GetAll();
        Task<ApiResponse<ProductDto>> GetById(string id);
        Task<ApiResponse<ProductDto>> Create(ProductDto ProductDto);
        Task<ApiResponse<ProductDto>> Update(ProductDto ProductDto);
        Task<ApiResponse<bool>> Delete(string id);

    }
}
