using AutoMapper;
using Division2ReconService.Data;
using Division2ReconService.Models;

namespace Division2ReconService.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerResponseDto>().ReverseMap();

            CreateMap<Customer, ProcessResponseDto>().ReverseMap();
        }
    }
}
