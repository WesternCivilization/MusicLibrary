using MusicLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.DataAccess.Interfaces
{
    public interface ITrackRepository
    {
        int CreateTrack(Track track);
    }
}
