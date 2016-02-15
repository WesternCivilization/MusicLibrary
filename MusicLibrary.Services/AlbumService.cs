using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary.Data;
using MusicLibrary.DataAccess.Interfaces;
using MusicLibrary.Services.Interfaces;
using System.Transactions;

namespace MusicLibrary.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IGenreRepository _genreRepository;

        public AlbumService(IAlbumRepository albumRepository, IArtistRepository artistRepository, IGenreRepository genreRepository)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _genreRepository = genreRepository;


        }

        public IEnumerable<Album> GetAllAlbums()
        {
            return _albumRepository.GetAllAlbums();
        }

        public IEnumerable<Album> GetNewAlbums()
        {
            //ALbums within the last year
            var oneYearAgo = DateTime.Now.AddYears(-1);



            return _albumRepository.GetAllAlbums().Where(f => f.ReleaseYear >= oneYearAgo.Year);
        }


        public IEnumerable<Album> GetAlbumByArtist(string artistName)
        {
           
            var artist = _artistRepository.GetArtistByName(artistName);


            return _albumRepository.FindAllAlbumsByArtist(artist);
        }

        public void AddNewAlbum(Album album)
        {
            using (var scope = new TransactionScope())
            {

                if (!DoesArtistExist(album.Artist))
                {
                    album.Artist.Id =  _artistRepository.CreateArtist(album.Artist);
                }

                if (!DoesGenreExist(album.Genre))
                {
                    album.Genre.Id = _genreRepository.CreateGenre(album.Genre);
                }

                if (!album.TracksCountValid())
                    return;


                _albumRepository.CreateAlbum(album);

                scope.Complete();
            }
        }

        private bool DoesGenreExist(Genre genre)
        {

            try
            {
                _genreRepository.GetGenreByName(genre.GenreName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DoesArtistExist(Artist artist)
        {

            try
            {
               _artistRepository.GetArtistByName(artist.ArtistName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
