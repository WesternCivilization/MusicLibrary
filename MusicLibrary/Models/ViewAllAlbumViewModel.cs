using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicLibrary.Models
{
    public class ViewAllAlbumViewModel
    {
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }

        public int ReleaseYear { get; set; }


        public int AlbumLength { get; set; }

        public IEnumerable<TrackViewModel> Tracks { get; set; } 
    }
}