using Microsoft.AspNetCore.Mvc;
using ThreeLayerArchitecture.Model;
using ThreeLayerArchitecture.Parameter;

namespace ThreeLayerArchitecture.Controller;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    /// <summary>
    /// 查詢卡片列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    public IEnumerable<CardViewModel> GetList(
        [FromQuery] CardSearchParameter parameter)
    {
        // 查詢卡片的一些操作
    }

    /// <summary>
    /// 查詢卡片
    /// </summary>     
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CardViewModel), 200)]
    [Route("{id}")]
    public CardViewModel Get(
        [FromRoute] int id)
    {
        // 查詢指定 ID 的卡片的一些操作
    }

    /// <summary>
    /// 新增卡片
    /// </summary>
    /// <param name="parameter">卡片參數</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Insert(
        [FromBody] CardParameter parameter)
    {
        // 新增卡片的一些操作
    }

    /// <summary>
    /// 更新卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <param name="parameter">卡片參數</param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(
        [FromRoute] int id,
        [FromBody] CardParameter parameter)
    {
        // 更新卡片的一些操作
    }

    /// <summary>
    /// 刪除卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(
        [FromRoute] int id)
    {
        // 刪除卡片的一些操作
    }
}