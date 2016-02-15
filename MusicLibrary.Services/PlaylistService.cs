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
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public IEnumerable<Playlist> GetAllPlaylists()
        {
            return _playlistRepository.GetAllPlaylists();
        }

        public IEnumerable<Track> GetTracksInPlaylist(Playlist playlist)
        {
            return _playlistRepository.GetAllPlaylists().SelectMany(p => p.Tracks);
        }

        public void CreatePlaylist(Playlist playlist)
        {
            if (playlist.IsPlaylistTooBig())
                throw new Exception("Too Big");

            _playlistRepository.CreatePlaylist(playlist);
        }
    }
}
