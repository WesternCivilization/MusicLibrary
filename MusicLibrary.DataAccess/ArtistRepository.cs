using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MusicLibrary.Data;
using MusicLibrary.DataAccess.Interfaces;

namespace MusicLibrary.DataAccess
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IDbConnection _connection;

        public ArtistRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Data.Artist> GetAllArtists()
        {
            var sql = "SELECT Id, NAME as ArtistName FROM dbo.Artist";

            return _connection.Query<Artist>(sql);
        }

        public Data.Artist GetArtistByName(string artistName)
        {
            var sql = "SELECT Id, NAME as ArtistName FROM dbo.Artist WHERE Name=@artistName";

            return _connection.Query<Artist>(sql, new { artistName = artistName }).Single();
        }

        public int CreateArtist(Data.Artist artist)
        {
            var sql = "INSERT INTO  dbo.Artist (Name) VALUES (@artistName); SELECT CAST(SCOPE_IDENTITY() as int";

            return _connection.ExecuteScalar<int>(sql, new {artistName = artist.ArtistName});
        }
    }
}