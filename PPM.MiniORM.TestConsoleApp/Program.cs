using Microsoft.Data.SqlClient;
using PPM.MiniORM.ConsoleApp;
using PPM.MiniORM.TestConsoleApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        CustomService customService =
            new(
                "Server=.;Database=BhonePyae;User ID=sa;Password=sasa@123;TrustServerCertificate=True;"
            );
        await customService.Run();
    }
}

public class CustomService
{
    private readonly string _connectionString;

    public CustomService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task Run()
    {
        //await GetBlogById(6);
        //await AddBlogAsync("PPM.MicroORM", "PPM.MicroORM", "PPM.MicroORM");
        await GetBlogsAsync();
    }

    private async Task GetBlogsAsync()
    {
        try
        {
            string query = BlogQuery.BlogListQuery;
            var parameters = new List<SqlParameter>() { new("@IsDeleted", false) };

            using var db = new SqlConnection(_connectionString);
            var lst = await db.QueryAsync<BlogModel>(query, parameters);

            if (lst is not null)
            {
                foreach (var item in lst)
                {
                    Console.WriteLine($"Blog Id: {item.BlogId}");
                    Console.WriteLine($"Blog Title: {item.BlogTitle}");
                    Console.WriteLine("---------------------------------");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task GetBlogById(int id)
    {
        try
        {
            string query = BlogQuery.GetBlogByIdQuery;
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@BlogId", id),
                new SqlParameter("@IsDeleted", false)
            };

            using var db = new SqlConnection(_connectionString);
            var item = await db.QueryFirstOrDefaultAsync<BlogModel>(query, parameters);

            if (item is not null)
            {
                Console.WriteLine($"Blog Id: {item.BlogId}");
                Console.WriteLine($"Blog Title: {item.BlogTitle}");
                Console.WriteLine("---------------------------------");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task AddBlogAsync(string blogTitle, string blogAuthor, string blogContent)
    {
        try
        {
            string query = BlogQuery.AddBlogQuery;

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@BlogTitle", blogTitle),
                new SqlParameter("@BlogAuthor", blogAuthor),
                new SqlParameter("@BlogContent", blogContent)
            };

            using var db = new SqlConnection(_connectionString);
            int result = await db.ExecuteAsync(query, parameters);

            Console.WriteLine(result > 0 ? "Saving Successful." : "Saving Fail.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

public class BlogModel
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
    public string BlogAuthor { get; set; }
    public string BlogContent { get; set; }
    public bool IsDeleted { get; set; }
}
