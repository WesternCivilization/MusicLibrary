using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary.Data;
using MusicLibrary.DataAccess.Interfaces;
using MusicLibrary.Services.Interfaces;

namespace MusicLibrary.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
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
    }
}
