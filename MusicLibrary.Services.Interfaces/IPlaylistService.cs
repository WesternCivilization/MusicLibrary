using System.Collections.Generic;
using MusicLibrary.Data;

namespace MusicLibrary.Services.Interfaces
{
    public interface IPlaylistService
    {
        IEnumerable<Playlist> GetAllPlaylists();

        IEnumerable<Track> GetTracksInPlaylist(Playlist playlist);
        void CreatePlaylist(Playlist playlist);
    }
}