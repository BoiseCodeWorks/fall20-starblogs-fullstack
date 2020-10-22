using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using StarBlogs.Models;
using StarBlogs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StarBlogs.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly BlogsService _bs;
    public ProfilesController(ProfilesService ps, BlogsService bs)
    {
      _ps = ps;
      _bs = bs;
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ps.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    //gets all the blogs for a profile
    [HttpGet("{id}/blogs")]
    public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        Profile queryProfile = _ps.GetProfileEmailById(id);
        return Ok(_bs.GetAllByCreatorEmail(queryProfile.Email, userInfo?.Email));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}