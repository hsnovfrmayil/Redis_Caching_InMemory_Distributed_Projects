﻿using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Distributed.Caching.Controllers;

public class ValueController :ControllerBase
{
    readonly IDistributedCache _distributedCache;

    public ValueController(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Set(string name,string surname)
    {
        await _distributedCache.SetStringAsync("name",name,options: new()
        {
            AbsoluteExpiration=DateTime.Now.AddSeconds(30),
            SlidingExpiration=TimeSpan.FromMicroseconds(5)
        });
        await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname), options: new()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(30),
            SlidingExpiration = TimeSpan.FromMicroseconds(5)
        });
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        var name=await _distributedCache.GetStringAsync ("name");
        var surnameBinary=await _distributedCache.GetAsync("surname");
        var surname = Encoding.UTF8.GetString(surnameBinary);
        return Ok(
            new
            {
                name,surname
            }
            );
    }
}

