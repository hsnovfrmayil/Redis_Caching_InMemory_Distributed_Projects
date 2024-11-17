using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers;


[ApiController]
[Route("[controller]")]
public class ValueController :ControllerBase
{
    readonly IMemoryCache _memoryCache;

    public ValueController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }


    //[HttpGet("set/{name}")]
    //public void SetName (string name)
    //{
    //    _memoryCache.Set("name", name);
    //}


    //[HttpGet("[action]")] 
    //public string GetName()
    //{
    //    if(_memoryCache.TryGetValue<string>("name",out string name))
    //    {
    //        return name.Substring(3);
    //    }
    //    return "";
    //}


    [HttpGet("[action]")]
    public void SetDate()
    {
        _memoryCache.Set<DateTime>("data", DateTime.UtcNow, options: new()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(30),
            SlidingExpiration = TimeSpan.FromMicroseconds(5)
        });
    }

    [HttpGet("[action]")]
    public DateTime GetDate()
    {
        return _memoryCache.Get<DateTime>("date");
    }
}

