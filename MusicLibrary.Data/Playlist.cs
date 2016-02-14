using System.Collections.Generic;
using System.Linq;

namespace MusicLibrary.Data
{
    public class Playlist
    {
        public string PlaylistName { get; set; }

        public IEnumerable<Track> Tracks { get; set; }

        public int GetLength()
        {
            return Tracks.Sum(f => f.Length);
        }

        public bool IsPlaylistTooBig()
        {
            return GetLength() > 100;
        }
    }
}