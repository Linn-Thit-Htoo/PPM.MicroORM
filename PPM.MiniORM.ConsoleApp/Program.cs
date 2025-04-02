namespace PPM.MiniORM.ConsoleApp;

public static class PPMMicroORM
{
    public static async Task Main(string[] args) { }

    /// <summary>
    /// Sync Query
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<T>? Query<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text
    )
    {
        try
        {
            connection.Open();
            using SqlCommand command = new(query, connection) { CommandType = commandType };

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

    /// <summary>
    /// Async Query
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <param name="cs"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<List<T>?> QueryAsync<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text,
        CancellationToken cs = default
    )
    {
        try
        {
            await connection.OpenAsync(cs);
            using SqlCommand command = new(query, connection) { CommandType = commandType };

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

    /// <summary>
    /// Sync
    /// Returns one element even if the sequence contains more than one element.
    /// Returns null if no record found
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static T? QueryFirstOrDefault<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text
    )
    {
        try
        {
            connection.Open();
            using SqlCommand command = new(query, connection) { CommandType = commandType };

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

    /// <summary>
    /// Async
    /// Returns one element even if the sequence contains more than one element.
    /// Returns null if no record found
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<T?> QueryFirstOrDefaultAsync<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text,
        CancellationToken cs = default
    )
    {
        try
        {
            await connection.OpenAsync(cs);
            using SqlCommand command = new(query, connection) { CommandType = commandType };

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

    /// <summary>
    /// Sync
    /// Returns one element even when the sequence contains more than one element.
    /// Throws error when there is no matching record.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public static T? QueryFirst<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text
    )
    {
        try
        {
            connection.Open();
            using SqlCommand command = new(query, connection) { CommandType = commandType };

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

    /// <summary>
    /// Async
    /// Returns one element even when the sequence contains more than one element.
    /// Throws error when there is no matching record.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public static async Task<T?> QueryFirstAsync<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text,
        CancellationToken cs = default
    )
    {
        try
        {
            await connection.OpenAsync(cs);
            using SqlCommand command = new(query, connection) { CommandType = commandType };

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

    /// <summary>
    /// Sync
    /// Ensures if there is only one element in the sequence.
    /// Throws error if there is no element or more than one element found.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public static T? Single<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text
    )
    {
        try
        {
            connection.Open();
            using SqlCommand command = new SqlCommand(query, connection)
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

            if (lst is not null && lst.Count == 1)
            {
                return lst.FirstOrDefault();
            }

            throw new InvalidOperationException(
                "No element found or more than one element in the sequence."
            );
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Async
    /// Ensures if there is only one element in the sequence.
    /// Throws error if there is no element or more than one element found.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public async static Task<T?> SingleAsync<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text,
        CancellationToken cs = default
    )
    {
        try
        {
            await connection.OpenAsync(cs);

            using SqlCommand command = new SqlCommand(query, connection)
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

            if (lst is not null && lst.Count == 1)
            {
                return lst.FirstOrDefault();
            }

            throw new InvalidOperationException(
                "No element found or more than one element in the sequence."
            );
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Sync
    /// Returns null if there is no element in the sequence.
    /// Throws error when there is more than one element in the sequence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public static T? SingleOrDefault<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text
    )
    {
        try
        {
            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection)
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

            if (lst is not null && lst.Count > 1)
            {
                throw new InvalidOperationException(
                    "There is mroe than one element in the sequence."
                );
            }

            return lst is not null && lst.Count == 1 ? lst.FirstOrDefault() : default;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Async
    /// Returns null if there is no element in the sequence.
    /// Throws error when there is more than one element in the sequence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public static async Task<T?> SingleOrDefaultAsync<T>(
        this SqlConnection connection,
        string query,
        List<SqlParameter>? parameters = null,
        CommandType commandType = CommandType.Text,
        CancellationToken cs = default
    )
    {
        try
        {
            await connection.OpenAsync(cs);

            using SqlCommand command = new SqlCommand(query, connection)
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

            if (lst is not null && lst.Count > 1)
            {
                throw new InvalidOperationException(
                    "There is mroe than one element in the sequence."
                );
            }

            return lst is not null && lst.Count == 1 ? lst.FirstOrDefault() : default;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Execute Sync
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int Execute(
        this SqlConnection connection,
        string query,
        List<SqlParameter> parameters
    )
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

    /// <summary>
    /// Execute Async
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="cs"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<int> ExecuteAsync(
        this SqlConnection connection,
        string query,
        List<SqlParameter> parameters,
        CancellationToken cs = default
    )
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
