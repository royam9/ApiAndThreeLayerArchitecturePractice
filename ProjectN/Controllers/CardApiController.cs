using Microsoft.AspNetCore.Mvc;
using ProjectN.Models;
using ProjectN.Repository;

namespace ProjectN.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CardApiController : Controller
{
    /// <summary>
    /// 卡片資料操作
    /// </summary>
    private readonly CardRepository _cardRepository;

    public CardApiController()
    {
        this._cardRepository = new CardRepository();
    }

    /// <summary>
    /// 取得卡片列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetList()
    {
        return Ok(_cardRepository.GetList());
    }

    /// <summary>
    /// 取得卡片
    /// </summary>
    /// <param name="id">卡片id</param>
    /// <returns></returns>
    [HttpGet]
    [Route("getCard/{id}")]
    public IActionResult GetCard([FromRoute] int id)
    {
        var result = _cardRepository.GetCard(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// 取得卡片
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cost"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCardById([FromRoute] int id, [FromQuery] int? cost, [FromQuery] string? text)
    {
        var result = await _cardRepository.GetCardById(id, cost, text);

        if (result == null)
        {
            Response.StatusCode = 404;
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// 新增卡片
    /// </summary>
    /// <param name="parameter">參數</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddCard([FromBody] CardParameter parameter)
    {
        var result = await _cardRepository.AddCard(parameter);

        if (result > 0)
        {
            return Ok();
        }

        return BadRequest();
    }

    /// <summary>
    /// 修改卡片
    /// </summary>
    /// <param name="id">卡片id</param>
    /// <param name="parameter">參數</param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateCard([FromRoute] int id, [FromBody] CardParameter parameter)
    {
        var result = await _cardRepository.UpdateCard(id, parameter);

        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// 刪除卡片
    /// </summary>
    /// <param name="id">卡片id</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteCard([FromRoute] int id)
    {
        await _cardRepository.DeleteCard(id);

        return Ok();
    }
}
