using System.Collections.Generic;
using System.Linq;

namespace MusicLibrary.Data
{
    public class Album
    {
        public string AlbumName { get; set; }

        public Artist Artist { get; set; }

        public int ReleaseYear { get; set; }

        public Genre Genre { get; set; }

        public IEnumerable<Track> Tracks { get; set; }


        public int GetLength()
        {
            return Tracks.Sum(f => f.Length);
        }

        public string DisplayName()
        {
            return $"{Artist.AristName} - {AlbumName}";
        }
    }
}
