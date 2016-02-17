using System.Collections.Generic;
using System.Linq;

namespace MusicLibrary.Data
{
    public class Album
    {

        public  const int MaxTrackCount = 100;

        public string AlbumName { get; set; }

        public Artist Artist { get; set; }

        public int ReleaseYear { get; set; }

        public Genre Genre { get; set; }

        public IEnumerable<Track> Tracks { get; set; }

        public int Id { get; set; }

        public bool TracksCountValid()
        {
            if (Tracks == null)
                return false;

            var trackCount = Tracks.Count();
            if (trackCount > 0 && trackCount < MaxTrackCount)
                return true;

            return false;
        }

        public int GetLength()
        {
            if (Tracks == null)
                return 0;

            return Tracks.Sum(f => f.Length);
        }

        public string DisplayName()
        {
            if (string.IsNullOrEmpty(AlbumName))
                return Artist?.ArtistName;

            if (string.IsNullOrEmpty(Artist?.ArtistName))
                return AlbumName;

            return $"{Artist.ArtistName} - {AlbumName}";
        }
    }
}
