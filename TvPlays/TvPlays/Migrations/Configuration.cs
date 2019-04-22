namespace TvPlays.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TvPlays.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TvPlays.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TvPlays.Models.ApplicationDbContext context)
        {

            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // -------- Falta Fazer a Seed Ate ter 5 a 10 Registos de cada Tabela e tudo Relacionado. Fazer as anotacoes nos Models para ter a certeza que trablahamos com aquele tipo de Variavel. (Regex)
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            //-----ADICIONA OS UTILIZADORES A BASE DE DADOS
            var utilizadores = new List<Utilizadores>
            {
                new Utilizadores {ID=1, Name="Ricardo Bernardes", BirthDate=new DateTime(), Email="ricado.1996@gmail.com", MobileNumber="915899344",
                    Description ="Sou alTa Player de Rocket League e sinceramente tou ao nivel de competicao professional"},
                new Utilizadores {ID=2, Name="Pedro Cruz", BirthDate=new DateTime(), Email="pedro.cruz@gmail.com", MobileNumber="915899399",
                    Description ="Nao pesco nada de nada disto"},
                new Utilizadores {ID=3, Name="Marco Mamelcio", BirthDate=new DateTime(), Email="marco.sushi@gmail.com", MobileNumber="915899789",
                    Description ="Ate sei jogar mas sou molengao"},
            };
            utilizadores.ForEach(uu => context.Utilizadores.AddOrUpdate(u => u.ID, uu));
            context.SaveChanges();

            //-----ADICIONA OS CLIPS A BASE DE DADOS
            var clips = new List<Clips>
            {
                new Clips {ID=1, DateClip=new DateTime(1996, 5, 24, 12, 0, 0), TitleClip="Biggest Comeback on Rocket League", PathClip="/biggestcomeback.mp4", UtilizadoresFK=1, TimeClip= new TimeSpan(00, 30, 33)}
            };
            clips.ForEach(cc => context.Clips.AddOrUpdate(c => c.ID, cc));
            context.SaveChanges();

            //-----ADICIONA OS COMENTARIOS A BASE DE DADOS
            var comments = new List<Comments>
            {
                new Comments {ID=1, ContComment="Alte Jogada Sim senhora", DateComment=new DateTime(2019, 4, 20), ClipsFK=1, UtilizadoresFK=2}
            };
            comments.ForEach(zz => context.Comments.AddOrUpdate(z => z.ID, zz));
            context.SaveChanges();

            //-----ADICIONA OS EMOJIS A BASE DE DADOS
            //var emojis = new List<Emojis>
            //{
            //    new Emojis {ID=1, PathEmoji="/caraCruz.png", ListUsers= { } },
            //    new Emojis {ID=2, PathEmoji="/caraSushi.png", ListUsers= { } },
            //    new Emojis {ID=3, PathEmoji="/caraHugo.png", ListUsers= { } },
            //    new Emojis {ID=1, PathEmoji="/caraPlaka.png", ListUsers= { } }
            //};
            //emojis.ForEach(ee => context.Emojis.AddOrUpdate(e => e.ID, ee));
            //context.SaveChanges();
        }
    }
}
