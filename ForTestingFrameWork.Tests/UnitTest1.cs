using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using System.Collections.Generic;
using Logging;

namespace ForTestingFrameWork.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (ShimsContext.Create())
            {
                var expectedResult = new List<Album>();
                // Arrange:
                // Detour DateTime.Now to return a fixed date:
                Logging.Fakes.ShimMusic.AllInstances.GetAlbumsByArtistString =
                (customer, options) =>
                {
                    expectedResult.AddRange(this.GetOtherAlbums());
                    return expectedResult;
                };
                ForTestingFrameWork.Fakes.ShimService1.AllInstances.GetOtherAlbums =
                (result) =>
                {
                    return this.GetOtherAlbums();
                    
                };
                // Instantiate the component under test:
                var componentUnderTest = new Service1();

                // Act:
                var actualResult = componentUnderTest.GetAlbumsByArtist("artist");

                // Assert: 
                // This will always be true if the component is working:
                CollectionAssert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public void TestForAddition()
        {
            using (ShimsContext.Create())
            {
                var expectedResult = 5;
                // Arrange:
                // Detour DateTime.Now to return a fixed date:
                Logging.Fakes.ShimMusic.AllInstances.AddInt32Int32 =
                (x,y,z) =>
                {
                    return z+y; 
                };
                
                var service = new Service1();

                // Act:
                var actualResult = service.AddTwoNumbers(1,2);

                // Assert: 
                Assert.AreEqual(expectedResult, actualResult);
            }
        }


        private List<Album> GetOtherAlbums()
        {
            Random ran = new Random();
            return new List<Album>()
            {
                new Album()
                {
                    AlbumID = ran.Next(),
                    AlbumName = ran.NextDouble().ToString(),
                    Artist = ran.NextDouble().ToString()
                }
            };
        }
    }
}
