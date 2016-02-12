using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary.Data;

namespace MusicLibrary.DataAccess.Interfaces
{
    public interface IAlbumRepository
    {
        Album GetAlbumByName(string name);

        IEnumerable<Album> FindAllAlbumsByGenre(Genre genre);

        IEnumerable<Artist> FindAllAlbumsByArtist(Artist artist);

        IEnumerable<Album> GetAllAlbums();

        void CreateAlbum(Album album);

    }

    public interface IPlaylistRepository
    {
        void CreatePlaylist(Playlist playlist);

        IEnumerable<Playlist> GetAllPlaylists();
    }
}
