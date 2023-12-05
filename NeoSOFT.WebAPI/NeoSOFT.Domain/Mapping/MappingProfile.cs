using AutoMapper;
using NeoSOFT.Domain.DTO;
using NeoSOFT.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSOFT.Domain.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        { 
            CreateMap<Product,ProductDto>().ReverseMap();
        }
    }
}
