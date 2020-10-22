using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarBlogs.Models;
using StarBlogs.Services;

namespace StarBlogs.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BlogsController : ControllerBase
  {
    private readonly BlogsService _serv;

    public BlogsController(BlogsService serv)
    {
      _serv = serv;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blog>>> Get()
    {
      try
      {
        //good way to handle private bool on return, get user email and pass to serv
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_serv.GetAll(userInfo?.Email));
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Blog>>> GetById(int id)
    {
      try
      {
        //good way to handle private bool on return, get user email and pass to serv
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_serv.GetById(userInfo?.Email, id));
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Blog>> Post([FromBody] Blog newBlog)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newBlog.CreatorEmail = userInfo.Email;
        Blog created = _serv.Create(newBlog);
        created.Creator = userInfo;
        return Ok(created);
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Blog>> Edit(int id, [FromBody] Blog editBlog)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editBlog.CreatorEmail = userInfo.Email;
        editBlog.Creator = userInfo;
        editBlog.Id = id;

        return Ok(_serv.Edit(editBlog, userInfo.Email));
      }
      catch (System.Exception error)
      {

        return BadRequest(error.Message);

      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_serv.Delete(id, userInfo.Email));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);

      }
    }


  }
}