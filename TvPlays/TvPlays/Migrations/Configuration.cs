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
            //-----ADICIONA OS UTILIZADORES A BASE DE DADOS
            var utilizadores = new List<Utilizadores>
            {
                new Utilizadores {ID=1, Name="Ricardo Bernardes", BirthDate=new DateTime(1995,7,25), Email="ricado.1996@gmail.com", MobileNumber="915899344",
                    Description ="Sou alTa Player de Rocket League e sinceramente tou ao nivel de competicao professional", Sexo = "M", ListClips = {}, ListEmojis = {} },
                new Utilizadores {ID=2, Name="Pedro Cruz", BirthDate=new DateTime(1995,7,25), Email="pedro.cruz@gmail.com", MobileNumber="915899399",
                    Description ="Nao pesco nada de nada disto", Sexo = "M", ListClips = {}, ListEmojis = {} },
                new Utilizadores {ID=3, Name="Marco Mamelcio", BirthDate = new DateTime(1995,7,25), Email="marco.sushi@gmail.com", MobileNumber="915899789",
                    Description ="Ate sei jogar mas sou molengao", Sexo = "M", ListClips = {}, ListEmojis = {} },
                new Utilizadores {ID=4, Name="Hugo Costa", BirthDate=new DateTime(1995,7,25), Email="hugo.costa@gmail.com", MobileNumber="912345678",
                    Description ="Eu nem jogo so para nao falar com os meus colegas de casa", Sexo = "M", ListClips = {}, ListEmojis = {} },
                new Utilizadores {ID=5, Name="Diogo Plaka", BirthDate = new DateTime(1995,7,25), Email="diogo.plaka@gmail.com", MobileNumber="923456789",
                    Description ="Eu penso q sou melhor q o Kadito apesar de todos dizerem q nao", Sexo = "M", ListClips = {}, ListEmojis = {} },
                new Utilizadores {ID=6, Name="André Carvalho", BirthDate = new DateTime(1995,7,25), Email="andré.carvalho@gmail.com", MobileNumber="934567891",
                    Description ="Sou grande génio a programar(CSS não é bem programar xD)", Sexo = "M", ListClips = {}, ListEmojis = {} }
            }; 
            utilizadores.ForEach(uu => context.Utilizadores.AddOrUpdate(u => u.ID, uu));
            context.SaveChanges();

            //-----ADICIONA OS CLIPS A BASE DE DADOS
            var clips = new List<Clips>
            {
                new Clips {ID=1, DateClip=new DateTime(1996, 5, 24, 12, 0, 0), TitleClip="Biggest Play on Rocket League", PathClip="/biggestplay.mp4", UserFK=1, TimeClip= new TimeSpan(00, 4, 33)},
                new Clips {ID=2, DateClip=new DateTime(1996, 5, 24, 12, 0, 0), TitleClip="Anuncio TvPlays", PathClip="/TvPlaysAD.mp4", UserFK=2, TimeClip= new TimeSpan(00, 3, 05)},
                new Clips {ID=3, DateClip=new DateTime(1996, 5, 24, 12, 0, 0), TitleClip="Avaliem o meu jogo", PathClip="/rocketReview.mp4", UserFK=3, TimeClip= new TimeSpan(00, 5, 10)},
                new Clips {ID=4, DateClip=new DateTime(1996, 5, 24, 12, 0, 0), TitleClip="BIG AWP SHOT by ZorlaRusky", PathClip="/bigshotRusky.mp4", UserFK=4, TimeClip= new TimeSpan(00, 2, 45)}
            };
            clips.ForEach(cc => context.Clips.AddOrUpdate(c => c.ID, cc));
            context.SaveChanges();

            //-----ADICIONA OS COMENTARIOS A BASE DE DADOS
            var comments = new List<Comments>
            {
                //Clip1
                new Comments {ID=1, ContComment="Alta jogada Sim senhora", DateComment=new DateTime(2019, 4, 20), ClipsFK=1, UtilizadoresFK=2},
                new Comments {ID=2, ContComment="Concordo", DateComment=new DateTime(2019, 4, 20), ClipsFK=1, UtilizadoresFK=2},
                new Comments {ID=3, ContComment="Muito fraco, tens de melhorar o posicionamento!!", DateComment=new DateTime(2019, 4, 20), ClipsFK=1, UtilizadoresFK=2},
                new Comments {ID=4, ContComment="Continua, vais melhorar com o tempo", DateComment=new DateTime(2019, 4, 20), ClipsFK=1, UtilizadoresFK=2},
                //Clip2
                new Comments {ID=5, ContComment="Já viram a nova plataforma de video que estes meninos fizeram?", DateComment=new DateTime(2019, 4, 20), ClipsFK=2, UtilizadoresFK=2},
                new Comments {ID=6, ContComment="Alguem lhes dê um 20!! GRANDES ALUNOS E GRANDE PROJETO!!!!! Os meus Parabéns! Bravo!!!!!!!!", DateComment=new DateTime(2019, 4, 20), ClipsFK=2, UtilizadoresFK=2},
                new Comments {ID=7, ContComment="Só de pensar no facto que devido a um jovem pensar que joga tanto um jogo pode sair uma ideia milionária e com tanto potencial, pena nao se ter lembrado disto mais cedo", DateComment=new DateTime(2019, 4, 20), ClipsFK=2, UtilizadoresFK=2},
                //Clip3
                new Comments {ID=8, ContComment="Este gajo joga tão pior que eu!", DateComment=new DateTime(2019, 4, 20), ClipsFK=3, UtilizadoresFK=2},
                new Comments {ID=9, ContComment="O @RRdoTinder parece que faz de proposito para perder", DateComment=new DateTime(2019, 4, 20), ClipsFK=3, UtilizadoresFK=2},
                new Comments {ID=10, ContComment="Que falta de entusiasmo", DateComment=new DateTime(2019, 4, 20), ClipsFK=3, UtilizadoresFK=2},
                new Comments {ID=11, ContComment="Mas ao menos, fala e diverte-nos, ja o @PapoSeco.....", DateComment=new DateTime(2019, 4, 20), ClipsFK=3, UtilizadoresFK=2},
                new Comments {ID=12, ContComment="Alguem o ouviu falar durante o jogo??", DateComment=new DateTime(2019, 4, 20), ClipsFK=3, UtilizadoresFK=2},
                new Comments {ID=13, ContComment="Neps.. Parece que saiu sem dizer nada a ninguem", DateComment=new DateTime(2019, 4, 20), ClipsFK=3, UtilizadoresFK=2},
                //Clip4
                new Comments {ID=14, ContComment="Mais um que pensa que é o Fallen", DateComment=new DateTime(2019, 4, 20), ClipsFK=4, UtilizadoresFK=2},
                new Comments {ID=15, ContComment="Larga a AWP, pode ser que mates mais de faca", DateComment=new DateTime(2019, 4, 20), ClipsFK=4, UtilizadoresFK=2},
                new Comments {ID=16, ContComment="RUSSOS e CS dá nisto.", DateComment=new DateTime(2019, 4, 20), ClipsFK=4, UtilizadoresFK=2}
            };
            comments.ForEach(zz => context.Comments.AddOrUpdate(z => z.ID, zz));
            context.SaveChanges();

            //-----ADICIONA OS EMOJIS A BASE DE DADOS
            var emojis = new List<Emojis>
            {
                new Emojis {ID=1, PathEmoji="/caraCruz.png", ListUsers= { } },
                new Emojis {ID=2, PathEmoji="/caraSushi.png", ListUsers= { } },
                new Emojis {ID=3, PathEmoji="/caraHugo.png", ListUsers= { } },
                new Emojis {ID=4, PathEmoji="/caraKado.png", ListUsers= { } },
                new Emojis {ID=5, PathEmoji="/kappa.png", ListUsers= { } },
                new Emojis {ID=6, PathEmoji="/smile.png", ListUsers= { } },
                new Emojis {ID=7, PathEmoji="/sunglasses.png", ListUsers= { } },
                new Emojis {ID=8, PathEmoji="/sick.png", ListUsers= { } },
                new Emojis {ID=9, PathEmoji="/awesome.png", ListUsers= { } },
                new Emojis {ID=10, PathEmoji="/insane.png", ListUsers= { } },
                new Emojis {ID=11, PathEmoji="/shroud.png", ListUsers= { } },
                new Emojis {ID=12, PathEmoji="/asmonSmash.png", ListUsers= { } },
                new Emojis {ID=13, PathEmoji="/clap.png", ListUsers= { } },
                new Emojis {ID=14, PathEmoji="/fail.png", ListUsers= { } }
            };
            emojis.ForEach(ee => context.Emojis.AddOrUpdate(e => e.ID, ee));
            context.SaveChanges();
        }
    }
}
