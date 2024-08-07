using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectN.Common.Interface;
using ProjectN.Service.Dtos.Info;
using ProjectN.Service.Dtos.ResultModel;
using ProjectN.Service.Implement;
using ProjectN.Service.Interface;
using ThreeLayerArchitecture.Mappings;
using ThreeLayerArchitecture.Model;
using ThreeLayerArchitecture.Parameter;

namespace ThreeLayerArchitecture.Controller;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICardService _cardService;

    public CardController(IMapperService mapperService,
        ICardService cardService)
    {        
        _mapper = mapperService.CreateMapper();
        _cardService = cardService;
    }

    /// <summary>
    /// 查詢卡片列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    public async Task<IEnumerable<CardViewModel>> GetList(
        [FromQuery] CardSearchParameter parameter)
    {
        var info = _mapper.Map<CardSearchParameter, CardSearchInfo>(parameter);

        var data = await _cardService.GetList(info);

        var result = _mapper.Map<IEnumerable<CardResultModel>, IEnumerable<CardViewModel>>(data);

        return result;
    }

    /// <summary>
    /// 查詢卡片
    /// </summary>     
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CardViewModel), 200)]
    [Route("{id}")]
    public async Task<CardViewModel> Get(
        [FromRoute] int id)
    {
        var data = await _cardService.Get(id);

        var result = _mapper.Map<CardResultModel, CardViewModel>(data);

        return result;
    }

    /// <summary>
    /// 新增卡片
    /// </summary>
    /// <param name="parameter">卡片參數</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Insert(
        [FromBody] CardParameter parameter)
    {
        var info = this._mapper.Map<
            CardParameter,
            CardInfo>(parameter);

        bool IsInsertSucess = await _cardService.Insert(info);

        if (IsInsertSucess)
        {
            return Ok();
        }

        return StatusCode(500);
    }

    /// <summary>
    /// 更新卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <param name="parameter">卡片參數</param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] CardParameter parameter)
    {
        var info = this._mapper.Map<CardParameter, CardInfo>(parameter);
        bool IsSucess = await _cardService.Update(id, info);

        if (IsSucess)
        {
            return Ok();
        }

        return StatusCode(500);
    }

    /// <summary>
    /// 刪除卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        bool IsSucess = await _cardService.Delete(id);

        if (IsSucess)
        {
            return Ok();
        }

        return StatusCode(500);
    }
}