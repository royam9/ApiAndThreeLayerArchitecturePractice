using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using ProjectN.Models;

namespace ProjectN.Repository;

/// <summary>
/// 卡片資料操作
/// </summary>
public class CardRepository
{
    /// <summary>
    /// 連線字串
    /// </summary>
    private readonly string _connectString = @"Server=(LocalDB)\MSSQLLocalDB;Database=Newbie;Trusted_Connection=True;";

    /// <summary>
    /// 查詢卡片列表
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Card> GetList()
    {
        using (var conn = new SqlConnection(_connectString))
        {
            var result = conn.Query<Card>("SELECT * FROM Card");
            return result;
        }
    }

    /// <summary>
    /// 取得卡片
    /// </summary>
    /// <param name="id">卡片id</param>
    /// <returns></returns>
    public Card GetCard(int id)
    {
        using (var conn = new SqlConnection(_connectString))
        {
            var result = conn.QueryFirstOrDefault<Card>("SELECT TOP 1 * FROM Card WHERE Id = @id",
                new
                {
                    Id = id
                });

            return result;
        }
    }

    /// <summary>
    /// 取得卡片-以參數拆分
    /// </summary>
    /// <param name="id">卡片id</param>
    /// <param name="cost">花費</param>
    /// <returns></returns>
    public async Task<Card> GetCardById(int id, int? cost, string? text)
    {
        var sql = "SELECT TOP 1 * FROM Card WHERE Id = @id";

        var param = new DynamicParameters();
        param.Add("Id", id);

        if (cost.HasValue && cost > 1)
        {
            sql += "AND Cost = @Cost";
            param.Add("Cost", cost);
        }

        if (!text.IsNullOrEmpty())
        {
            param.Add("Text", text, System.Data.DbType.AnsiString);
        }

        using (var conn  = new SqlConnection())
        {
            var result = await conn.QueryFirstOrDefaultAsync<Card>(sql, param);

            return result;
        }
    }
}
