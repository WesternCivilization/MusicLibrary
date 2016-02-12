using NUnit.Framework;

namespace MusicLibrary.Data.Tests
{
    [TestFixture]
    public class AlbumTestFixture
    {
        [Test]
        public void TestDisplayNameValidArtistAndAlbumName()
        {
            var album = new Album
            {
                AlbumName = "Holy Diver",
                Artist = new Artist
                {
                    AristName  = "Dio"
                }
            };

            Assert.AreEqual("Dio - Holy Diver", album.DisplayName());
        }

        [Test]
        public void TestDisplayNameValidArtistEmptyAlbumName()
        {
            var album = new Album
            {
                AlbumName = null,
                Artist = new Artist
                {
                    AristName = "Dio"
                }
            };

            Assert.AreEqual("Dio", album.DisplayName());
        }

        [Test]
        public void TestDisplayNameEmptyArtistValidAlbumName()
        {
            var album = new Album
            {
                AlbumName = "Holy Diver",
                Artist = null
            };

            Assert.AreEqual("Holy Diver", album.DisplayName());
        }

        [Test]
        public void TestDisplayNameEmptyArtistEmptyAlbumName()
        {
            var album = new Album
            {
                AlbumName = null,
                Artist = null
            };

            Assert.IsNull(album.DisplayName());
        }
    }
}