namespace PPM.MiniORM.TestConsoleApp;

public class BlogQuery
{
    public static string BlogListQuery { get; } =
        @"SELECT BlogId, BlogTitle, BlogAuthor, BlogContent, IsDeleted
FROM Tbl_Blog WHERE IsDeleted = @IsDeleted";

    public static string GetBlogByIdQuery { get; } =
        @"SELECT BlogId, BlogTitle, BlogAuthor, BlogContent, IsDeleted
FROM Tbl_Blog WHERE BlogId = @BlogId AND IsDeleted = @IsDeleted";

    public static string AddBlogQuery { get; } = @"INSERT INTO Tbl_Blog (BlogTitle, BlogAuthor, BlogContent)
VALUES (@BlogTitle, @BlogAuthor, @BlogContent)";
}
