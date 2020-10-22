using System;
using System.Collections.Generic;
using System.Linq;
using StarBlogs.Models;
using StarBlogs.Repositories;

namespace StarBlogs.Services
{
  public class BlogsService
  {

    private readonly BlogsRepository _repo;

    public BlogsService(BlogsRepository repo)
    {
      _repo = repo;
    }

    internal Blog Create(Blog newBlog)
    {
      newBlog.Id = _repo.Create(newBlog);
      return newBlog;
    }

    internal IEnumerable<Blog> GetAll(string userEmail)
    {
      //takes in email, gets all blogs, finds blogs where creator is userEmail passed
      //OR published is true
      IEnumerable<Blog> blogs = _repo.GetAll();
      return blogs.ToList().FindAll(b => b.CreatorEmail == userEmail || b.Published);
    }

    internal Blog Edit(Blog editBlog, string userEmail)
    {
      Blog original = _repo.GetById(editBlog.Id);
      if (original == null) { throw new Exception("Invalid Id"); }
      if (original.CreatorEmail != userEmail) { throw new Exception("Access Denied... This is not yours"); }
      editBlog.Name = editBlog.Name == null ? original.Name : editBlog.Name;
      editBlog.Description = editBlog.Description == null ? original.Description : editBlog.Description;
      editBlog.Img = editBlog.Img == null ? original.Img : editBlog.Img;

      return _repo.Edit(editBlog);
    }

    internal Blog GetById(string userEmail, int id)
    {
      Blog original = _repo.GetById(id);
      if (original == null) { throw new Exception("Invalid Id"); }
      if (original.CreatorEmail != userEmail && original.Published == false) { throw new Exception("Access Denied... This is not yours"); }
      return original;
    }

    internal object Delete(int id, string userEmail)
    {
      Blog original = _repo.GetById(id);
      if (original == null) { throw new Exception("Invalid Id"); }
      if (original.CreatorEmail != userEmail) { throw new Exception("Access Denied... This is not yours"); }
      _repo.Remove(id);
      return "succesfully delorted";

    }

    internal IEnumerable<Blog> GetAllByCreatorEmail(string queryProfileEmail, string email)
    {
      return _repo.GetByCreatorEmail(queryProfileEmail).ToList().FindAll(b => b.CreatorEmail == email || b.Published);
    }

  }
}