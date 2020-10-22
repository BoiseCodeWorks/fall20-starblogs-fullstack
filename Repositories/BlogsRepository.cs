using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using StarBlogs.Models;

namespace StarBlogs.Repositories
{
  public class BlogsRepository
  {
    private readonly IDbConnection _db;

    private readonly string populateCreator = @"SELECT 
    blog.*,
    profile.* 
    FROM blogs blog 
    JOIN profiles profile on blog.creatorEmail = profile.email ";

    public BlogsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(Blog newBlog)
    {
      string sql = @"
        INSERT INTO Blogs
        (name, description, creatorEmail, img, published )
        VALUES
        (@Name, @Description, @CreatorEmail, @Img, @Published);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newBlog);
    }

    internal IEnumerable<Blog> GetAll()
    {
    //SELECT 
    // blog.*,
    // profile.* 
    // FROM blogs blog 
    // JOIN profiles profile on blog.creatorEmail = profile.email
   
        string sql = populateCreator;
        return _db.Query<Blog, Profile, Blog>(sql, (blog, profile) => {blog.Creator = profile; return blog;}, splitOn: "id");
    }

    internal Blog GetById(int id)
    {
    //SELECT 
    // blog.*,
    // profile.* 
    // FROM blogs blog 
    // JOIN profiles profile on blog.creatorEmail = profile.email
    // WHERE blog.id = @id;
        string sql = populateCreator + "WHERE blog.id = @id";
        return _db.Query<Blog, Profile, Blog>(sql, (blog, profile) => {blog.Creator = profile; return blog;}, new {id}, splitOn: "id").FirstOrDefault();

    }

    internal IEnumerable<Blog> GetByCreatorEmail(string queryProfileEmail)
    {
        string sql = populateCreator + "WHERE creatorEmail = @queryProfileEmail;";
       return _db.Query<Blog, Profile, Blog>(sql, (blog, profile) => {blog.Creator = profile; return blog;}, new {queryProfileEmail}, splitOn: "id");
    }

    internal Blog Edit(Blog editBlog)
    {
        string sql = @"
            UPDATE blogs
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            published = @Published
            WHERE id = @Id;";
            _db.Execute(sql, editBlog);
            return editBlog;
    }

    internal void Remove(int id)
    {
        string sql = "DELETE FROM blogs WHERE id = @id";
        _db.Execute(sql, new {id});
    }
  }
}