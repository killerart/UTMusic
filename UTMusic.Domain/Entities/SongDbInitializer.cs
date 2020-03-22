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
                    Duration = DateTime.Parse("00:06:28"),
                    Name = "Metallica - Nothing Else Matters (1991)"
                },
                new Song{
                    Duration = DateTime.Parse("00:04:14"),
                    Name = "ACDC - Back in Black"
                },
                new Song{
                    Duration = DateTime.Parse("00:04:49"),
                    Name = "ACDC - Hell's Bells"
                },
                new Song{
                    Duration = DateTime.Parse("00:04:33"),
                    Name = "Arctic Monkeys - Do I Wanna Know"
                },
                new Song{
                    Duration = DateTime.Parse("00:05:42"),
                    Name = "Deep Purple - Smoke on the Water"
                },
                new Song{
                    Duration = DateTime.Parse("00:03:39"),
                    Name = "Kaleo - Way Down We Go"
                }
            };
            context.Songs.AddRange(songs);
            base.Seed(context);
        }
    }
}