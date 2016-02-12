using System.Collections.Generic;
using MusicLibrary.Data;

namespace MusicLibrary.DataAccess.Interfaces
{
    public interface IPlaylistRepository
    {
        void CreatePlaylist(Playlist playlist);

        IEnumerable<Playlist> GetAllPlaylists();
    }
}