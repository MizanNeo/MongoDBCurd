using AutoMapper;
using NeoSOFT.Application.Contracts;
using NeoSOFT.Common.Classes;
using NeoSOFT.Common.Helpers;
using NeoSOFT.Domain.DTO;
using NeoSOFT.Domain.Model;
using NeoSOFT.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NeoSOFT.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ProductDto>>> GetAll()
        {
            var resultList = await _repository.GetAllAsync();
            if (resultList == null)
                return ApiResponseHelper.CreateApiResponse<List<ProductDto>>(HttpStatusCode.BadRequest);
            else
                return ApiResponseHelper.CreateApiResponse(_mapper.Map<List<ProductDto>>(resultList), HttpStatusCode.OK);
        }

        public async Task<ApiResponse<ProductDto>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return ApiResponseHelper.CreateApiResponse<ProductDto>(HttpStatusCode.BadRequest);
            }

            var result = await _repository.GetByIdAsync(id);
            if(result==null)
                return ApiResponseHelper.CreateApiResponse<ProductDto>(HttpStatusCode.BadRequest);
            else
                return ApiResponseHelper.CreateApiResponse(_mapper.Map<ProductDto>(result), HttpStatusCode.OK);
        }

        public async Task<ApiResponse<ProductDto>> Create(ProductDto ProductDto)
        {
            if (ProductDto == null)
            {
                return ApiResponseHelper.CreateApiResponse<ProductDto>(HttpStatusCode.BadRequest);
            }

            var result = _mapper.Map<Product>(ProductDto);
            result.isActive = true;
            result.CreatedBy="user";
            result.CreatedOn=DateTime.Now;
            Product productDto = new Product();
            var response = await _repository.CreateAsync(result);

            return ApiResponseHelper.CreateApiResponse(_mapper.Map<ProductDto>(response), HttpStatusCode.OK);
        }

        public async Task<ApiResponse<ProductDto>> Update(ProductDto ProductDto)
        {
            if (ProductDto == null)
            {
                return ApiResponseHelper.CreateApiResponse<ProductDto>(HttpStatusCode.BadRequest);
            }

            var result = await _repository.GetByIdAsync(ProductDto.id);

            if (result == null)
            {
                return ApiResponseHelper.CreateApiResponse<ProductDto>(HttpStatusCode.BadRequest);
            }

            result.productName=ProductDto.productName;
            result.productCategory=ProductDto.productCategory;
            result.productDescription=ProductDto.productDescription;
            result.productPrice=ProductDto.productPrice;
            result.isActive=true;
            result.UpdatedBy="user";
            result.UpdatedOn=DateTime.Now;

            var response= await _repository.UpdateAsync(ProductDto.id, result);
            return ApiResponseHelper.CreateApiResponse(_mapper.Map<ProductDto>(response), HttpStatusCode.OK);
        }

        public async Task<ApiResponse<bool>> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return ApiResponseHelper.CreateApiResponse(false, HttpStatusCode.BadRequest);
            }

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                return ApiResponseHelper.CreateApiResponse(false, HttpStatusCode.BadRequest);
            }

            result.isActive = false;

            var response= await _repository.UpdateAsync(id,result);
           if(response!=null)
            return ApiResponseHelper.CreateApiResponse(true, HttpStatusCode.OK);
           else
                return ApiResponseHelper.CreateApiResponse(false, HttpStatusCode.OK);
        }
    }
}
