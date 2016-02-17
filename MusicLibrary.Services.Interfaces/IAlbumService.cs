using MusicLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Services.Interfaces
{
    public interface IAlbumService
    {
        IEnumerable<Album> GetAllAlbums();

        IEnumerable<Album> GetNewAlbums();

        IEnumerable<Album> GetAlbumByArtist(string artistName);

        Album AddNewAlbum(Album album);
    }
}
