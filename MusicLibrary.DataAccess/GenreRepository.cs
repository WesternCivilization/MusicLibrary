using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MusicLibrary.Data;
using MusicLibrary.DataAccess.Interfaces;

namespace MusicLibrary.DataAccess
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IDbConnection _connection;

        public GenreRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var sql = "SELECT NAME as GenreName FROM dbo.Genre";

            return _connection.Query<Genre>(sql);
        }

        public Genre GetGenreByName(string genreName)
        {
            var sql = "SELECT NAME as GenreName FROM dbo.Genre WHERE Name = @genreName";

            return _connection.Query<Genre>(sql,new {genreName=genreName}).Single();
        }

        public void CreateGenre(Genre genre)
        {
            var sql = "INSERT INTO  dbo.Genre (Name) VALUES (@genreName)";

            _connection.Execute(sql, new { genreName = genre.GenreName});
        }
    }
}

