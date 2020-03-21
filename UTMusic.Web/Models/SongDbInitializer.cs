using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UTMusic.Web.Models
{
    public class SongDbInitializer : DropCreateDatabaseAlways<SongContext>
    {
        protected override void Seed(SongContext context)
        {
            context.Songs.AddRange(new List<Song>
            {
                new Song{
                    Id = 0,
                    Name ="Nothing Else Matters (1991)",
                    Author="Metallica",
                    Duration=new Time{ Minutes = 6, Seconds = 28 },
                    Filename="Metallica - Nothing Else Matters (1991)"
                },
                new Song{
                    Id = 1,
                    Name ="Back in Black",
                    Author="ACDC",
                    Duration=new Time{ Minutes = 4, Seconds = 14 },
                    Filename="ACDC - Back in Black"
                },
                new Song{
                    Id = 2,
                    Name ="Hell's Bells",
                    Author="ACDC",
                    Duration=new Time{ Minutes = 4, Seconds = 49 },
                    Filename="ACDC - Hell's Bells"
                },
                new Song{
                    Id = 3,
                    Name ="Do I Wanna Know",
                    Author="Arctic Monkeys",
                    Duration=new Time{ Minutes = 4, Seconds = 33 },
                    Filename="Arctic Monkeys - Do I Wanna Know"
                },
                new Song{
                    Id = 4,
                    Name ="Smoke on the Water",
                    Author="Deep Purple",
                    Duration=new Time{ Minutes = 5, Seconds = 42 },
                    Filename="Deep Purple - Smoke on the Water"
                },
                new Song{
                    Id = 5,
                    Name ="Way Down We Go",
                    Author="Kaleo",
                    Duration=new Time{ Minutes = 3, Seconds = 39 },
                    Filename="Kaleo - Way Down We Go"
                }
            });
            base.Seed(context);
        }
    }
}