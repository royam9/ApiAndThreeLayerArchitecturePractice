using Dapper;
using Microsoft.Data.SqlClient;
using ProjectN.Repository.Dtos.Condition;
using ProjectN.Repository.Dtos.DataModel;
using ProjectN.Repository.Interface;

namespace ProjectN.Repository.Implement;

public class CardRepository : ICardRepository
{
    /// <summary>
    /// 連線字串
    /// </summary>
    // private readonly string _connectionString = "Server=Windows-PC-Jack\\SQLEXPRESS;TrustServerCertificate=True;Database=Newbie;Trusted_Connection=True;";

    private readonly string _connectionString;

    public CardRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// 查詢卡片列表
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<CardDataModel>> GetList(CardSearchCondition info)
    {
        string sql = "SELECT * FROM Card";

        List<string> sqlQuery = new();
        var parameter = new DynamicParameters();

        if (info.MaxCost.HasValue)
        {
            sqlQuery.Add("Cost <= @MaxCost");
            parameter.Add("MaxCost", info.MaxCost);
        }

        if (info.MinCost.HasValue)
        {
            sqlQuery.Add("Cost >= @MinCost");
            parameter.Add("MinCost", info.MinCost);
        }

        if (info.MaxHealth.HasValue)
        {
            sqlQuery.Add("Health <= @MaxHealth");
            parameter.Add("MaxHealth", info.MaxHealth);
        }

        if (info.MinHealth.HasValue)
        {
            sqlQuery.Add("Health >= @MinHealth");
            parameter.Add("MinHealth", info.MinHealth);
        }

        if (info.MaxAttack.HasValue)
        {
            sqlQuery.Add("Attack <= @MaxAttack");
            parameter.Add("MaxAttack", info.MaxAttack);
        }

        if (info.MinAttack.HasValue)
        {
            sqlQuery.Add("Attack >= @MinAttack");
            parameter.Add("MinAttack", info.MinAttack);
        }

        if (!string.IsNullOrWhiteSpace(info.Name))
        {
            sqlQuery.Add("Name LIKE @Name");
            parameter.Add("Name", $"%{info.Name}%");
        }

        if (sqlQuery.Any())
        {
            sql += $" WHERE {string.Join(" AND ", sqlQuery)}";
        }

        using (var conn = new SqlConnection(_connectionString))
        {
            var result = await conn.QueryAsync<CardDataModel>(sql, parameter);
            return result;
        }
    }

    /// <summary>
    /// 查詢卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <returns></returns>
    public async Task<CardDataModel> Get(int id)
    {
        var sql = "SELECT * FROM Card WHERE Id = @Id";
        var param = new DynamicParameters();

        param.Add("Id", id, System.Data.DbType.Int32);

        using (var conn = new SqlConnection(_connectionString))
        {
            var result = await conn.QueryFirstOrDefaultAsync<CardDataModel>(sql, param);
            return result;
        }
    }

    /// <summary>
    /// 新增卡片
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public async Task<bool> Insert(CardCondition info)
    {
        var sql = "INSERT INTO Card([Name], [Description], [Attack], [Health], [Cost])" +
            "VALUES(@Name, @Description, @Attack, @Health, @Cost);" +
            "SELECT @@IDENTITY;";

        var param = new DynamicParameters(info);

        using (var conn = new SqlConnection(_connectionString))
        {
            var result = await conn.QueryFirstOrDefaultAsync<int>(sql, param);
            return result > 0;
        }
    }

    /// <summary>
    /// 更新卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public async Task<bool> Update(int id, CardCondition info)
    {
        var sql = @"
                    UPDATE Card
                    SET Name = @Name
                        Description = @Description
                        Attack = @Attack
                        Health = @Health
                        Cost = @Cost
                    WHERE Id = @Id";

        var param = new DynamicParameters(info);
        param.Add("Id", id, System.Data.DbType.Int32);

        using (var conn = new SqlConnection(_connectionString))
        {
            var result = await conn.ExecuteAsync(sql, param);

            return result > 0;
        }
    }

    /// <summary>
    /// 刪除卡片
    /// </summary>
    /// <param name="id">卡片編號</param>
    /// <returns></returns>
    public async Task<bool> Delete(int id)
    {
        var sql = @"DELETE FROM Card
                    WHERE Id = @Id";

        var param = new DynamicParameters();
        param.Add("Id", id);

        using (var conn = new SqlConnection(_connectionString))
        {
            var result = await conn.ExecuteAsync(sql, param);
            return result > 0;
        }
    }
}
