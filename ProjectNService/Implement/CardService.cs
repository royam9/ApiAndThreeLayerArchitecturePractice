using AutoMapper;
using ProjectN.Common.Interface;
using ProjectN.Repository.Dtos.Condition;
using ProjectN.Repository.Dtos.DataModel;
using ProjectN.Repository.Implement;
using ProjectN.Repository.Interface;
using ProjectN.Service.Dtos.Info;
using ProjectN.Service.Dtos.ResultModel;
using ProjectN.Service.Interface;
using ProjectN.Service.Mappings;

namespace ProjectN.Service.Implement;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;
    private readonly IMapper _mapper;

    public CardService(ICardRepository cardRepository,
        IMapperService mapperService)
    {
        _cardRepository = cardRepository;
        _mapper = mapperService.CreateMapper();
    }

    /// <summary>
    /// 查詢卡片列表
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CardResultModel>> GetList(CardSearchInfo info)
    {
        var condition = _mapper.Map<CardSearchInfo, CardSearchCondition>(info);
        var data = await _cardRepository.GetList(condition);

        var result = _mapper.Map<IEnumerable<CardDataModel>, IEnumerable<CardResultModel>>(data);
        return result;
    }

    /// <summary>
    /// 查詢卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <returns></returns>
    public async Task<CardResultModel> Get(int id)
    {
        var data = await _cardRepository.Get(id);
        var result = _mapper.Map<CardDataModel, CardResultModel>(data);
        return result;
    }

    /// <summary>
    /// 新增卡片
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public async Task<bool> Insert(CardInfo info)
    {
        var condition = _mapper.Map<CardInfo, CardCondition>(info);
        bool sucess = await _cardRepository.Insert(condition);
        return sucess;
    }

    /// <summary>
    /// 更新卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <param name="info"></param>
    /// <returns></returns>
    public async Task<bool> Update(int id, CardInfo info)
    {
        var condition = _mapper.Map<CardInfo, CardCondition>(info);
        bool sucess = await _cardRepository.Update(id, condition);
        return sucess;
    }

    /// <summary>
    /// 刪除卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <returns></returns>
    public async Task<bool> Delete(int id)
    {
        bool sucess = await _cardRepository.Delete(id);
        return sucess;
    }
}
