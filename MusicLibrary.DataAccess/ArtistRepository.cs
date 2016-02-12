using System.Collections.Generic;
using MusicLibrary.DataAccess.Interfaces;

namespace MusicLibrary.DataAccess
{
    public class ArtistRepository : IArtistRepository
    {
        public IEnumerable<Data.Artist> GetAllArtists()
        {
            throw new System.NotImplementedException();
        }

        public Data.Artist GetArtistByName(string artistName)
        {
            throw new System.NotImplementedException();
        }

        public void CreateArtist(Data.Artist artist)
        {
            throw new System.NotImplementedException();
        }
    }
}