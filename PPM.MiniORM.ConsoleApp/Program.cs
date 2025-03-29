using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

public static class PPMMicroORM
{
    public static void Main(string[] args)
    {
        
    }

    public static List<T> Query<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text)
    {
        try
        {
            connection.Open();
            SqlCommand command = new(query, connection);

            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new(command);
            DataTable dt = new();

            adapter.Fill(dt);
            connection.Close();
            string jsonStr = JsonConvert.SerializeObject(dt);

            return JsonConvert.DeserializeObject<List<T>>(jsonStr)!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public static async Task<List<T>> QueryAsync<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cs = default)
    {
        try
        {
            await connection.OpenAsync(cs);
            SqlCommand command = new(query, connection);

            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new(command);
            DataTable dt = new();

            adapter.Fill(dt);
            await connection.CloseAsync();
            string jsonStr = JsonConvert.SerializeObject(dt);

            return JsonConvert.DeserializeObject<List<T>>(jsonStr)!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static T QueryFirstOrDefault<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text)
    {
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new();
            adapter.Fill(dt);

            connection.Close();
            string jsonStr = JsonConvert.SerializeObject(dt, Formatting.Indented);

            return JsonConvert.DeserializeObject<List<T>>(jsonStr)!.FirstOrDefault()!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<T> QueryFirstOrDefaultAsync<T>(this SqlConnection connection, string query, List<SqlParameter>? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cs = default)
    {
        try
        {
            await connection.OpenAsync(cs);
            SqlCommand command = new SqlCommand(query, connection);
            if (parameters is not null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new();
            adapter.Fill(dt);

            await connection.CloseAsync();
            string jsonStr = JsonConvert.SerializeObject(dt, Formatting.Indented);

            return JsonConvert.DeserializeObject<List<T>>(jsonStr)!.FirstOrDefault()!;
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