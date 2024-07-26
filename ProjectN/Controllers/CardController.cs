using Microsoft.AspNetCore.Mvc;
using ProjectN.Models;
using System.Reflection.Metadata;

namespace ProjectN.Controllers;

[ApiController]
[Route("[Controller]")]
public class CardController : Controller
{
    /// <summary>
    /// 測試用集合
    /// </summary>
    private static List<Card> _cards = new();

    /// <summary>
    /// 取得所有卡片
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public List<Card> GetList()
    {
        return _cards;
    }

    /// <summary>
    /// 取得單張卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public Card GetCard([FromRoute] int id)
    {
        return _cards.FirstOrDefault(c => c.Id == id);
    }

    /// <summary>
    /// 新增卡片
    /// </summary>
    /// <param name="card">參數</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AddCard([FromBody] CardParameter card)
    {
        var newCard = new Card()
        {
            Id = _cards.Any() ? _cards.Max(c => c.Id) + 1 : 0,
            Name = card.Name,
            Description = card.Description
        };

        _cards.Add(newCard);

        return Ok();
    }

    /// <summary>
    /// 修改卡片
    /// </summary>
    /// <param name="id">CardId</param>
    /// <param name="cardParam">參數</param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateCard([FromRoute] int id, [FromBody] CardParameter cardParam)
    {
        var card = _cards.FirstOrDefault(c => c.Id == id);
        if (card == null)
        {
            return NotFound();
        }
        card.Name = cardParam.Name;
        card.Description = cardParam.Description;
        return Ok();
    }

    /// <summary>
    /// 刪除卡片
    /// </summary>
    /// <param name="id">CardId</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteCard([FromRoute] int id)
    {
        var card = _cards.FirstOrDefault(c => c.Id == id);

        if (card == null) 
        {
            return NotFound();
        }

        _cards.RemoveAll(card => card.Id == id);

        return Ok();
    }
}
