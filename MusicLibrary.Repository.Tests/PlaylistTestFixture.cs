using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary.Data;
using MusicLibrary.DataAccess;
using MusicLibrary.DataAccess.Interfaces;
using NUnit.Framework;

namespace MusicLibrary.Repository.Tests
{
    [TestFixture]
    public class PlaylistTestFixture
    {
        private IDbConnection _connection;
        private IPlaylistRepository _playlistRepository;

        [OneTimeSetUp]
        public void PlaylistTestFixtureSetup()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            _playlistRepository = new PlaylistRepository(_connection);
        }

        [Test]
        public void GetAllPlaylistTest()
        {
            var res = _playlistRepository.GetAllPlaylists();
            Assert.NotNull(res);
            var collection = res as IList<Playlist> ?? res.ToList();
            Assert.IsNotEmpty(collection);
            Assert.AreEqual(2, collection.Count());
        }
    }
}
