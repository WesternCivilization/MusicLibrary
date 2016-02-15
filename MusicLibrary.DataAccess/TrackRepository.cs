using MusicLibrary.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary.Data;
using System.Data;
using Dapper;

namespace MusicLibrary.DataAccess
{
    public class TrackRepository : ITrackRepository
    {
        private readonly IDbConnection _database;

        public TrackRepository(IDbConnection database)
        {
            _database = database;
        }


        public int CreateTrack(Track track)
        {
            var insertTrack = @"INSERT INTO dbo.Track VALUES(Name,Length,Number)
VALUES (@name,@length,@number); SELECT CAST(SCOPE_IDENTITY() as int;";

            return  _database.ExecuteScalar<int>(insertTrack, new { name = track.Name, length = track.Length, number = track.Number });

        }
    }
}
