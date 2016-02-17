using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicLibrary.Data;

namespace MusicLibrary.Models
{
    public class SaveAlbumViewModel
    {

        public string AlbumName { get; set; }

        public string ArtistName { get; set; }

        public int ReleaseYear { get; set; }


        public IEnumerable<TrackViewModel> Tracks { get; set; } 
    }
}