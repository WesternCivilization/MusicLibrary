using System.Collections.Generic;
using System.Linq;
using Moq;
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
                AlbumName = "Joe's Garage",
                Artist = new Artist
                {
                    ArtistName  = "Frank Zappa"
                }
            };

            Assert.AreEqual("Frank Zappa - Joe's Garage", album.DisplayName());
        }

        [Test]
        public void TestDisplayNameValidArtistEmptyAlbumName()
        {
            var album = new Album
            {
                AlbumName = null,
                Artist = new Artist
                {
                    ArtistName = "Frank Zappa"
                }
            };

            Assert.AreEqual("Frank Zappa", album.DisplayName());
        }

        [Test]
        public void TestDisplayNameEmptyArtistValidAlbumName()
        {
            var album = new Album
            {
                AlbumName = "Joe's Garage",
                Artist = null
            };

            Assert.AreEqual("Joe's Garage", album.DisplayName());
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


        [Test]
        public void TestTrackCountEmpty()
        {
            var album = new Album();

            Assert.False(album.TracksCountValid());
        }


        [Test]
        public void TestTrackCountZero()
        {
            var album = new Album
            {
                Tracks = new List<Track>()
            };

            Assert.False(album.TracksCountValid());
        }

        [Test]
        public void TestTrackCountOneAboveMax()
        {


            int numberOfElements = 101;
            var mockAlbum = Mock.Of<Album>(x => x.Tracks
                                           == Enumerable.Repeat(Mock.Of<Track>(), numberOfElements).ToList());

            Assert.False(mockAlbum.TracksCountValid());
        }


        [Test]
        public void TestTrackCountOneLessMax()
        {


            int numberOfElements = 99;
            var mockAlbum = Mock.Of<Album>(x => x.Tracks
                                           == Enumerable.Repeat(Mock.Of<Track>(), numberOfElements).ToList());

            Assert.True(mockAlbum.TracksCountValid());
        }


        [Test]
        public void TestTrackLengthEmpty()
        {
            var album = new Album();

            Assert.AreEqual(0,album.GetLength());
        }


        [Test]
        public void TestTrackLengthZero()
        {
            var album = new Album
            {
                Tracks = new List<Track>()
            };

            Assert.AreEqual(0, album.GetLength());
        }

        [Test]
        public void TestTrackLengthGreaterThanZero()
        {
            int numberOfElements = 5;
            var mockAlbum = Mock.Of<Album>(x => x.Tracks
                                           == Enumerable.Repeat(Mock.Of<Track>(i=>i.Length==1), numberOfElements).ToList());

  
            Assert.AreEqual(5, mockAlbum.GetLength());
        }


    }
}