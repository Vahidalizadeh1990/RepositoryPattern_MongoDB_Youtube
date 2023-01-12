using AutoMapper;
using MainApp.Data.MusicRepo;
using MainApp.DTO;
using MainApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MusicController : ControllerBase
{
    private readonly IMusicService _musicService;
    private readonly IMapper _mapper;

    public MusicController(IMusicService musicService, IMapper mapper)
    {
        _musicService = musicService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Gets()
    {
        var music = _musicService.List();
        if(music.Any())
        {
            return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<MusicDTO>>(music));
        }
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpGet("details")]
    public IActionResult Get(string id)
    {
        var music = _musicService.Details(id);
        if(music is not null)
        {
            return StatusCode(StatusCodes.Status200OK, _mapper.Map<MusicDTO>(music));
        }
        return StatusCode(StatusCodes.Status404NotFound);
    }

    [HttpDelete]
    public IActionResult Delete(string id)
    {
        var music = _musicService.DeleteMusic(id);
        if(music is true)
        {
            return StatusCode(StatusCodes.Status200OK);
        }
        return StatusCode(StatusCodes.Status400BadRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Post(MusicDTO musicDTO)
    {
        var mapModel = _mapper.Map<Music>(musicDTO);
        var result = await _musicService.AddMusic(mapModel);
        if(result is not null)
        {
            return StatusCode(StatusCodes.Status201Created, _mapper.Map<MusicDTO>(result));
        }
        return StatusCode(StatusCodes.Status400BadRequest);
    }

    [HttpPut]
    public async Task<IActionResult> Put(MusicDTO musicDTO)
    {
         var mapModel = _mapper.Map<Music>(musicDTO);
        var result = await _musicService.EditMusic(mapModel);
        if(result is not null)
        {
            return StatusCode(StatusCodes.Status200OK, _mapper.Map<MusicDTO>(result));
        }
        return StatusCode(StatusCodes.Status400BadRequest);
    }

}