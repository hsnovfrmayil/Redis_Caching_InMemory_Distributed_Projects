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


    [HttpGet("set/{name}")]
    public void Set(string name)
    {
        _memoryCache.Set("name", name);
    }


    [HttpGet("[action]")] 
    public string Get()
    {
        if(_memoryCache.TryGetValue<string>("name",out string name))
        {
            return name.Substring(3);
        }
        return "";
    }
}

