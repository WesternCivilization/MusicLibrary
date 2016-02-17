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
        private readonly ITrackRepository _trackRepos;

        private readonly string _baseAlbumQuery = @"SELECT 
album.Id as Id, album.Name as AlbumName, artist.Name as ArtistName, genre.Name as GenreName,
album.Year as ReleaseYear
FROM dbo.Album album INNER JOIN dbo.Artist artist
ON album.ArtistId = artist.Id
INNER JOIN dbo.Genre genre
ON album.GenreId = genre.Id";

        private readonly string _baseTrackQuery = @"SELECT 
track.Id, track.Name, track.TrackNumber as Number, track.Length
FROM dbo.Track track INNER JOIN dbo.xrAlbumTrack xr
ON xr.TrackId = track.Id";

        public AlbumRepository(IDbConnection database, ITrackRepository trackRepos)
        {
            _database = database;
            _trackRepos = trackRepos;

        }



        private IEnumerable<Track> GetTracksForAlbum(int albumId)
        {
            var sql = _baseTrackQuery + " WHERE xr.AlbumId=@albumId";

            return _database.Query<Track>(sql, new {albumId = albumId});
        }

        public Album GetAlbumByName(string name)
        {
            var sql = _baseAlbumQuery + " WHERE album.Name=@albumName";

            var res = _database.Query(sql, new {albumName = name}).Single();

            var album = new Album()
            {
                AlbumName = res.AlbumName,
                ReleaseYear = res.ReleaseYear,
                Genre = new Genre {GenreName = res.GenreName},
                Artist = new Artist {ArtistName = res.ArtistName},
                Tracks = GetTracksForAlbum(res.Id)
            };


            return album;

        }

        public IEnumerable<Album> FindAllAlbumsByGenre(Data.Genre genre)
        {
            var sql = _baseAlbumQuery + " WHERE genre.Name=@genre";

            var results = _database.Query(sql, new { genre = genre.GenreName });

            return results.Select(res => new Album()
            {
                AlbumName = res.AlbumName,
                ReleaseYear = res.ReleaseYear,
                Genre = new Genre { GenreName = res.GenreName },
                Artist = new Artist { ArtistName = res.ArtistName },
                Tracks = GetTracksForAlbum(res.Id)
            });

        }

        public IEnumerable<Album> FindAllAlbumsByArtist(Data.Artist artist)
        {
            var sql = _baseAlbumQuery + " WHERE artist.Name=@artist";

            var results = _database.Query(sql, new { artist = artist.ArtistName });

            return results.Select(res => new Album()
            {
                AlbumName = res.AlbumName,
                ReleaseYear = res.ReleaseYear,
                Genre = new Genre { GenreName = res.GenreName },
                Artist = new Artist { ArtistName = res.ArtistName },
                Tracks = GetTracksForAlbum(res.Id)
            });

        }

        public IEnumerable<Album> GetAllAlbums()
        {

            var results = _database.Query(_baseAlbumQuery);

            return results.Select(res => new Album()
            {
                AlbumName = res.AlbumName,
                ReleaseYear = res.ReleaseYear,
                Genre = new Genre { GenreName = res.GenreName },
                Artist = new Artist { ArtistName = res.ArtistName },
                Tracks = GetTracksForAlbum(res.Id)
            });
        }

        public int CreateAlbum(Album album)
        {
            var insert = @"INSERT INTO dbo.Album VALUES(Name,Year,ArtistId,GenreId)
VALUES (@albumName,@year,@artistId,@genreId); SELECT CAST(SCOPE_IDENTITY() as int;";

            var albumId = _database.ExecuteScalar<int>(insert, new { albumName = album.AlbumName, year = album.ReleaseYear, artistId = album.Artist.Id, genreId = album.Genre.Id });

            foreach (var t in album.Tracks)
            {
                CreateTrack(albumId, t);
            }

            return albumId;
        }

        private void CreateTrack(int albumId, Track track)
        {
            var trackId = _trackRepos.CreateTrack(track);

            var insertXr = @"INSERT INTO dbo.xrAlbumTrack VALUES(AlbumId,TrackId)
VALUES (@albumId,@trackId)";

            _database.Execute(insertXr, new { albumId = albumId, trackId=trackId });

        }
    }
}
