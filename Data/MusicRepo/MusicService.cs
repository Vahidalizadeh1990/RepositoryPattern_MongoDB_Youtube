using MainApp.Models;
using MongoDB.Driver;

namespace MainApp.Data.MusicRepo;

public class MusicService : IMusicService
{
    private readonly IMongoCollection<Music> _musicCollection;
    public MusicService(IMongoDatabase mongoDatabase)
    {
        _musicCollection = mongoDatabase.GetCollection<Music>("music");
    }
    public async Task<Music> AddMusic(Music music)
    {
        if (music is not null)
        {
            music.Id = Guid.NewGuid().ToString();
            await _musicCollection.InsertOneAsync(music);
        }
        return music;
    }

    public Music Details(string id)
    {
        return _musicCollection.Find(m=>m.Id == id).FirstOrDefault();
    }
    public bool DeleteMusic(string id)
    {
        var music = Details(id);
        if(music is not null)
        {
            _musicCollection.DeleteOne(m=>m.Id==id);
            return true;
        }
        return false;
    }

    public async Task<Music> EditMusic(Music music)
    {
        var musicDetails = Details(music.Id);
        if(musicDetails is not null)
        {

            // Automapper or any mapping libraries or function is usefull to map these properties together.
            musicDetails.Title = music.Title;
            musicDetails.ReleaseDate = music.ReleaseDate;
            musicDetails.Artist = music.Artist;
            musicDetails.Rate = music.Rate;
            await _musicCollection.ReplaceOneAsync(m=>m.Id ==musicDetails.Id, music);
        }
        return musicDetails;
    }

    public IEnumerable<Music> List()
    {
        return _musicCollection.Find(_=>true).ToList();
    }
}
