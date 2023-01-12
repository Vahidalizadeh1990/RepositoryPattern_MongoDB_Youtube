using AutoMapper;
using MainApp.DTO;
using MainApp.Models;

namespace MainApp.MapModel;

public class MusicMapper : Profile 
{
    public MusicMapper()
    {
        // Source ==> Target
        CreateMap<Music, MusicDTO>();
        CreateMap<MusicDTO, Music>();
    }
}