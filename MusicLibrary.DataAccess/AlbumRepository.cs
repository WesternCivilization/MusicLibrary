using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MusicLibrary.Data;
using MusicLibrary.DataAccess.Interfaces;

namespace MusicLibrary.DataAccess
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IDbConnection _database;


        private readonly string _baseAlbumQuery = @"SELECT 
album.Id as Id, album.Name as AlbumName, artist.Name as ArtistName, genre.Name as GenreName,
album.Year as ReleaseYear
FROM dbo.Album album INNER JOIN dbo.Artist artist
ON album.ArtistId = artist.Id
INNER JOIN dbo.Genre genre
ON album.GenreId = genre.Id";

        private readonly string _baseTrackQuery = @"SELECT 
track.Name, track.TrackNumber, track.Length
FROM dbo.Track track INNER JOIN dbo.xrAlbumTrack xr
ON xr.TrackId = track.Id";

        public AlbumRepository(IDbConnection database)
        {
            _database = database;
        }



        private IEnumerable<Track> GetTracksForAlbum(int albumId)
        {
            var sql = _baseTrackQuery + " WHERE xr.AlbumId=@albumId";

            return _database.Query<Track>(sql, new {albumId = albumId});
        } 

        public Album GetAlbumByName(string name)
        {
            var sql = _baseAlbumQuery + " WHERE album.Name=@albumName";

            var album = _database.Query<Album>(sql, new {albumName = name}).Single();

            album.Tracks = GetTracksForAlbum(album.Id);
            return album;
          
        }

        public IEnumerable<Album> FindAllAlbumsByGenre(Data.Genre genre)
        {
            var sql = _baseAlbumQuery + " WHERE genre.Name=@genre";

            var albums = _database.Query<Album>(sql, new { genre = genre.GenreName });

            foreach (var a in albums)
            {
                a.Tracks = GetTracksForAlbum(a.Id);
                yield return a;
            }

        }

        public IEnumerable<Album> FindAllAlbumsByArtist(Data.Artist artist)
        {
            var sql = _baseAlbumQuery + " WHERE artist.Name=@artist";

            var albums = _database.Query<Album>(sql, new { artist = artist.AristName });

            foreach (var a in albums)
            {
                a.Tracks = GetTracksForAlbum(a.Id);
                yield return a;
            }

        }

        public IEnumerable<Album> GetAllAlbums()
        {

            var albums = _database.Query<Album>(_baseAlbumQuery);

            foreach (var a in albums)
            {
                a.Tracks = GetTracksForAlbum(a.Id);
                yield return a;
            }
        }

        public void CreateAlbum(Album album)
        {
            throw new NotImplementedException();
        }
    }
}
