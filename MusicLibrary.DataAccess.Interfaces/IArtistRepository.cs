using System.Collections.Generic;
using MusicLibrary.Data;

namespace MusicLibrary.DataAccess.Interfaces
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAllArtists();

        Artist GetArtistByName(string artistName);

        int CreateArtist(Artist artist);
    }
}