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
    private readonly string _connectString = "Server=Windows-PC-Jack\\SQLEXPRESS;TrustServerCertificate=True;Database=Newbie;Trusted_Connection=True;";

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
    public async Task<Card> GetCardById(int id, int? cost = null, string? text = null)
    {
        var sql = "SELECT TOP 1 * FROM Card WHERE Id = @id";

        var param = new DynamicParameters();
        param.Add("Id", id);

        if (cost.HasValue && cost >= 0)
        {
            sql += "AND Cost = @Cost";
            param.Add("Cost", cost);
        }

        if (!text.IsNullOrEmpty())
        {
            param.Add("Text", text, System.Data.DbType.AnsiString);
        }

        using (var conn = new SqlConnection(_connectString))
        {
            var result = await conn.QueryFirstOrDefaultAsync<Card>(sql, param);
            return result;
        }
    }

    /// <summary>
    /// 新增卡片
    /// </summary>
    /// <param name="parameter">卡片參數</param>
    /// <returns></returns>
    public async Task<int> AddCard(CardParameter parameter)
    {
        var sql = @"
                  INSERT INTO Card
                  (
                  [Name] 
                  ,[Description] 
                  ,[Attack] 
                  ,[Health] 
                  ,[Cost]
                  )
                  VALUES(
                  @Name, 
                  @Description, 
                  @Attack, 
                  @Health, 
                  @Cost);
                  SELECT SCOPE_IDENTITY();";

        using (var conn = new SqlConnection(_connectString))
        {
            await conn.OpenAsync();

            using (var transaction = await conn.BeginTransactionAsync())
            {
                var result = await conn.QueryFirstOrDefaultAsync<int>(sql, parameter, transaction);
                await transaction.CommitAsync();
                return result;
            }
        }
    }

    /// <summary>
    /// 修改卡片
    /// </summary>
    /// <param name="id">卡片id</param>
    /// <param name="parameter">卡片參數</param>
    /// <returns></returns>
    public async Task<bool> UpdateCard(int id, CardParameter parameter)
    {
        var sql = @"UPDATE Card SET 
                    [Name] = @Name, 
                    [Description] = @Description,                    
                    [Attack] = @Attack, 
                    [Health] = @Health, 
                    [Cost] = @Cost
                    WHERE Id = @Id";

        var param = new DynamicParameters(parameter);
        param.Add("Id", id, System.Data.DbType.Int32);

        using (var conn = new SqlConnection(_connectString))
        {
            using (var transaction = conn.BeginTransaction())
            {
                var result = await conn.ExecuteAsync(sql, param);

                transaction.Commit();

                return result > 0;
            }
        }
    }

    /// <summary>
    /// 刪除卡片
    /// </summary>
    /// <param name="id">卡片id</param>
    /// <returns></returns>
    public async Task DeleteCard(int id)
    {
        var sql = @"DELETE FROM Card WHERE Id = @id";

        var param = new DynamicParameters();
        param.Add("Id", id);

        using (var connection = new SqlConnection(_connectString))
        {
            await connection.ExecuteAsync(sql, param);
        }
    }
}
