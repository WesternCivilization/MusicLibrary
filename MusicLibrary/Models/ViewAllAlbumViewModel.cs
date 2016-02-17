using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicLibrary.Data;

namespace MusicLibrary.Models
{
    public class ViewAllAlbumViewModel
    {
        public ViewAllAlbumViewModel(Album album)
        {
            AlbumName = album.DisplayName();
            AlbumLength = album.GetLength();
            ReleaseYear = album.ReleaseYear;
            Tracks = album.Tracks.Select(f => new TrackViewModel(f));
        }

        public string AlbumName { get; set; }

        public int ReleaseYear { get; set; }


        public int AlbumLength { get; set; }

        public IEnumerable<TrackViewModel> Tracks { get; set; } 
    }
}