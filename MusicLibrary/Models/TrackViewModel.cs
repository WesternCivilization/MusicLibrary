using MusicLibrary.Data;

namespace MusicLibrary.Models
{
    public class TrackViewModel
    {
        public  TrackViewModel(Track track)
        {
            TrackName = track.Name;
            TrackNumber = track.Number;
            TrackLength = track.Length;
        }

        public string TrackName { get; set; }
        public int TrackLength { get; set; }

        public int TrackNumber { get; set; }

        public Track ToTrack()
        {
            return new Track
            {
                Length = TrackLength,
                Name = TrackName,
                Number = TrackNumber
            };
        }
    }
}