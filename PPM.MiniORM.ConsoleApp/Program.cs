using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using PPM.MiniORM.ConsoleApp;
using System.Data;
using System.Linq;

public class BlogModel
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
    public string BlogAuthor { get; set; }
    public string BlogContent { get; set; }
    public bool IsDeleted { get; set; }
}

public static class PPMMicroORM
{
    public static async Task Main(string[] args)
    {
        try
        {
            string query = @"SELECT BlogId, BlogTitle, BlogAuthor, BlogContent, IsDeleted FROM
Tbl_Blog WHERE BlogId = @BlogId AND IsDeleted = @IsDeleted";
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@BlogId", 100),
                new SqlParameter("@IsDeleted", false)
            };

            using var db = new SqlConnection("Server=.;Database=BhonePyae;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");
            var item = await db.QueryFirstOrDefaultAsync<BlogModel>(query, parameters);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static List<T>? Query<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text)
    {
        try
        {
            connection.Open();

            SqlCommand command = new(query, connection)
            {
                CommandType = commandType
            };

            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new(command);
            DataTable dt = new();

            adapter.Fill(dt);
            connection.Close();
            string jsonStr = dt.ToJson();

            return jsonStr.ToObject<List<T>>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<List<T>?> QueryAsync<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cs = default)
    {
        try
        {
            await connection.OpenAsync(cs);

            SqlCommand command = new(query, connection)
            {
                CommandType = commandType
            };

            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new(command);
            DataTable dt = new();

            adapter.Fill(dt);
            await connection.CloseAsync();
            string jsonStr = dt.ToJson();

            return jsonStr.ToObject<List<T>>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // default value will be null if no record found
    public static T? QueryFirstOrDefault<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text)
    {
        try
        {
            connection.Open();
            SqlCommand command = new(query, connection)
            {
                CommandType = commandType
            };
            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new();
            adapter.Fill(dt);

            connection.Close();
            string jsonStr = dt.ToJson();
            var lst = jsonStr.ToObject<List<T>>();

            return lst is not null && lst.Count > 0 ? lst.FirstOrDefault() : default;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // default value will be null if no record found
    public static async Task<T?> QueryFirstOrDefaultAsync<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cs = default)
    {
        try
        {
            await connection.OpenAsync(cs);
            SqlCommand command = new(query, connection)
            {
                CommandType = commandType
            };

            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new();
            adapter.Fill(dt);

            await connection.CloseAsync();
            string jsonStr = dt.ToJson();
            var lst = jsonStr.ToObject<List<T>>();

            return lst is not null && lst.Count > 0 ? lst.FirstOrDefault() : default;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // throws error if no record found
    public static T? QueryFirst<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text)
    {
        try
        {
            connection.Open();
            SqlCommand command = new(query, connection)
            {
                CommandType = commandType
            };

            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new();
            adapter.Fill(dt);

            connection.Close();
            string jsonStr = dt.ToJson();
            var lst = jsonStr.ToObject<List<T>>();

            if (lst is not null && lst.Count > 0 && lst.FirstOrDefault() is not null)
            {
                return lst.FirstOrDefault();
            }

            throw new InvalidOperationException("No record found.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // throws error if no record found
    public static async Task<T?> QueryFirstAsync<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cs = default)
    {
        try
        {
            await connection.OpenAsync(cs);
            SqlCommand command = new(query, connection)
            {
                CommandType = commandType
            };

            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new();
            adapter.Fill(dt);

            connection.Close();
            string jsonStr = dt.ToJson();
            var lst = jsonStr.ToObject<List<T>>();

            if (lst is not null && lst.Count > 0 && lst.FirstOrDefault() is not null)
            {
                return lst.FirstOrDefault();
            }

            throw new InvalidOperationException("No record found.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // throws error if there is no element or more than one element
    public static T? Single<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text)
    {
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection) { CommandType = commandType };
            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new();
            adapter.Fill(dt);

            connection.Close();
            string jsonStr = dt.ToJson();
            var lst = jsonStr.ToObject<List<T>>();

            if (lst is not null && lst.Count == 1)
            {
                return lst.FirstOrDefault();
            }

            throw new InvalidOperationException("No element found or more than one element.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // throws error if there is no element or more than one element
    public async static Task<T?> SingleAsync<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cs = default)
    {
        try
        {
            await connection.OpenAsync(cs);

            SqlCommand command = new SqlCommand(query, connection) { CommandType = commandType };
            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new();
            adapter.Fill(dt);

            await connection.CloseAsync();
            string jsonStr = dt.ToJson();
            var lst = jsonStr.ToObject<List<T>>();

            if (lst is not null && lst.Count == 1)
            {
                return lst.FirstOrDefault();
            }

            throw new InvalidOperationException("No element found or more than one element.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static int Execute(this SqlConnection connection, string query, List<SqlParameter> parameters)
    {
        try
        {
            connection.Open();

            SqlCommand command = new(query, connection);
            command.Parameters.AddRange(parameters.ToArray());
            int result = command.ExecuteNonQuery();
            connection.Close();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<int> ExecuteAsync(this SqlConnection connection, string query, List<SqlParameter> parameters, CancellationToken cs = default)
    {
        try
        {
            await connection.OpenAsync(cs);

            SqlCommand command = new(query, connection);
            command.Parameters.AddRange(parameters.ToArray());
            int result = await command.ExecuteNonQueryAsync(cs);
            await connection.CloseAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}