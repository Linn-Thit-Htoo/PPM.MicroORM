namespace PPM.MiniORM.TestConsoleApp;

public class BlogQuery
{
    public static string BlogListQuery { get; } = @"SELECT BlogId, BlogTitle, BlogAuthor, BlogContent, IsDeleted
FROM Tbl_Blog WHERE IsDeleted = @IsDeleted";

    public static string GetBlogByIdQuery { get; } = @"SELECT BlogId, BlogTitle, BlogAuthor, BlogContent, IsDeleted
FROM Tbl_Blog WHERE BlogId = @BlogId AND IsDeleted = @IsDeleted";
}
