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


        private readonly string _baseQuery = @"SELECT 
album.Name as AlbumName, artist.Name as ArtistName, genre.Name as GenreName,
album.Year as ReleaseYear, track.Length, track.Name, track.TrackNumber as Number
FROM dbo.Album album INNER JOIN dbo.Artist artist
ON album.ArtistId = artist.Id
INNER JOIN dbo.Genre genre
ON album.GenreId = genre.Id
INNER JOIN dbo.xrAlbumTrack xr ON
album.Id = xr.AlbumId
INNER JOIN dbo.Track track ON
xr.TrackId = track.Id";

        public AlbumRepository(IDbConnection database)
        {
            _database = database;
        }



        public Album GetAlbumByName(string name)
        {
            var sql = _baseQuery + " WHERE album.Name=@albumName";

            Album album = null;
            IList<Track> tracks = new List<Track>();
            foreach (var d in  _database.Query(sql, new {albumName = name}))
            {
                if (album == null)
                {
                    album = new Album();
                    album.AlbumName = d.AlbumName;
                    album.ReleaseYear = d.ReleaseYear;
                    album.Genre = new Genre {GenreName = d.GenreName};
                    album.Artist = new Artist {AristName = d.ArtistName};
                    album.Tracks = new List<Track>();
                }

                tracks.Add(new Track
                {
                    Length = d.Length,
                    Name = d.Name,
                    Number = d.Number
                });


            }

            album.Tracks = tracks;

            return album;
          
        }

        public IEnumerable<Album> FindAllAlbumsByGenre(Data.Genre genre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Artist> FindAllAlbumsByArtist(Data.Artist artist)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            throw new NotImplementedException();
        }

        public void CreateAlbum(Album album)
        {
            throw new NotImplementedException();
        }
    }
}
