using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UTMusic.Data.Entities
{
    public class SongDbInitializer : DropCreateDatabaseAlways<SongContext>
    {
        protected override void Seed(SongContext context)
        {
            var songs = new List<Song>
            {
                new Song{
                    Name = "Metallica - Nothing Else Matters (1991)"
                },
                new Song{
                    Name = "ACDC - Back in Black"
                },
                new Song{
                    Name = "ACDC - Hell's Bells"
                },
                new Song{
                    Name = "Arctic Monkeys - Do I Wanna Know"
                },
                new Song{
                    Name = "Deep Purple - Smoke on the Water"
                },
                new Song{
                    Name = "Kaleo - Way Down We Go"
                }
            };
            context.Songs.AddRange(songs);
            base.Seed(context);
        }
    }
}