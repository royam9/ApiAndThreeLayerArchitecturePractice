using AutoMapper;
using ProjectN.Service.Dtos.Info;
using ProjectN.Service.Dtos.ResultModel;
using ThreeLayerArchitecture.Model;
using ThreeLayerArchitecture.Parameter;

namespace ThreeLayerArchitecture.Mappings;

public class ControllerMappings : Profile
{
    public ControllerMappings()
    {
        // Parameter -> Info
        this.CreateMap<CardParameter, CardInfo>();
        this.CreateMap<CardSearchParameter, CardSearchInfo>();

        // ResultModel -> ViewModel
        this.CreateMap<CardResultModel, CardViewModel>();
    }
}
