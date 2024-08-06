using AutoMapper;
using ProjectN.Repository.Dtos.Condition;
using ProjectN.Repository.Dtos.DataModel;
using ProjectN.Service.Dtos.Info;
using ProjectN.Service.Dtos.ResultModel;

namespace ProjectN.Service.Mappings;

public class ServiceMappings : Profile
{
    public ServiceMappings()
    {
        // Info -> Condition
        this.CreateMap<CardInfo, CardCondition>();
        this.CreateMap<CardSearchInfo, CardSearchCondition>();

        // DataModel -> ResultModel
        this.CreateMap<CardDataModel, CardResultModel>();
    }
}
