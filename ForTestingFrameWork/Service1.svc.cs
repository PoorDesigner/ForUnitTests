using Logging;
using Misc;
using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace ForTestingFrameWork
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public List<Album> GetAlbumsByArtist(string name)
        {
            var Container = new UnityContainer();
            Container.RegisterType<IMusic, Music>(new ContainerControlledLifetimeManager());
            var Depe = Container.Resolve<IMusic>();
            var albums = Depe.GetAlbumsByArtist(name);
            var logsFromOtherApp = GetOtherAlbums();
            albums.AddRange(logsFromOtherApp);
            return albums;
        }

        private List<Album> GetOtherAlbums()
        {
            return new List<Album>()
            {
                new Album()
                {
                    AlbumID = 2,
                    AlbumName = "Album2",
                    Artist = "Test"
                }
            };
        }
    }
}
