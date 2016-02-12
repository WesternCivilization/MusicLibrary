using System.Collections.Generic;
using MusicLibrary.Data;
using MusicLibrary.DataAccess.Interfaces;

namespace MusicLibrary.DataAccess
{
    public class GenreRepository : IGenreRepository
    {
        public IEnumerable<Genre> GetAllGenres()
        {
            throw new System.NotImplementedException();
        }

        public Genre GetGenreByName(string genreName)
        {
            throw new System.NotImplementedException();
        }

        public void CreateGenre(Genre genre)
        {
            throw new System.NotImplementedException();
        }
    }
}