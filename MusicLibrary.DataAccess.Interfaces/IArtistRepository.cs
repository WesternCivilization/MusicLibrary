using System.Collections.Generic;
using MusicLibrary.Data;

namespace MusicLibrary.DataAccess.Interfaces
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAllArtists();

        Artist GetArtistByName(string artistName);

        void CreateArtist(Artist artist);
    }
}