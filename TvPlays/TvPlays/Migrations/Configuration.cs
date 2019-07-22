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
                    Description ="Sou alTa Player de Rocket League e sinceramente tou ao nivel de competicao professional", Sexo = "M", ListClips = {} },
                new Utilizadores {ID=2, Name="Pedro Cruz", BirthDate=new DateTime(1995,7,25), Email="pedro.cruz@gmail.com", MobileNumber="915899399",
                    Description ="Nao pesco nada de nada disto", Sexo = "M", ListClips = {}},
                new Utilizadores {ID=3, Name="Marco Mamelcio", BirthDate = new DateTime(1995,7,25), Email="marco.sushi@gmail.com", MobileNumber="915899789",
                    Description ="Ate sei jogar mas sou molengao", Sexo = "M", ListClips = {}},
                new Utilizadores {ID=4, Name="Hugo Costa", BirthDate=new DateTime(1995,7,25), Email="hugo.costa@gmail.com", MobileNumber="912345678",
                    Description ="Eu nem jogo so para nao falar com os meus colegas de casa", Sexo = "M", ListClips = {}},
                new Utilizadores {ID=5, Name="Diogo Plaka", BirthDate = new DateTime(1995,7,25), Email="diogo.plaka@gmail.com", MobileNumber="923456789",
                    Description ="Eu penso q sou melhor q o Kadito apesar de todos dizerem q nao", Sexo = "M", ListClips = {} },
                new Utilizadores {ID=6, Name="André Carvalho", BirthDate = new DateTime(1995,7,25), Email="andré.carvalho@gmail.com", MobileNumber="934567891",
                    Description ="Sou grande génio a programar(CSS não é bem programar xD)", Sexo = "M", ListClips = {} }
            }; 
            utilizadores.ForEach(uu => context.Utilizadores.AddOrUpdate(u => u.ID, uu));
            context.SaveChanges();

            //-----ADICIONA OS CLIPS A BASE DE DADOS
            var clips = new List<Clips>
            {
                new Clips {ID=1, DateClip=new DateTime(1996, 5, 24, 12, 0, 0), TitleClip="Biggest Play on Rocket League", PathClip="biggestplay.mp4", UserFK=1},
                new Clips {ID=2, DateClip=new DateTime(1996, 5, 24, 12, 0, 0), TitleClip="Anuncio TvPlays", PathClip="TvPlaysAD.mp4", UserFK=2 },
                new Clips {ID=3, DateClip=new DateTime(1996, 5, 24, 12, 0, 0), TitleClip="Avaliem o meu jogo", PathClip="rocketReview.mp4", UserFK=3},
                new Clips {ID=4, DateClip=new DateTime(1996, 5, 24, 12, 0, 0), TitleClip="BIG AWP SHOT by ZorlaRusky", PathClip="bigshotRusky.mp4", UserFK=4}
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
            var categories = new List<Categories>
            {
                new Categories {ID=1, Name="Action", PathToCategory = "action.jpg" ,ListClips= { } },
                new Categories {ID=2, Name="FPS", PathToCategory = "fps.jpg", ListClips= { } },
                new Categories {ID=3, Name="Adventure", PathToCategory = "adv.jpg", ListClips= { } },
                new Categories {ID=4, Name="RPG", PathToCategory = "rpg.jpg", ListClips= { } },
                new Categories {ID=5, Name="Simulation", PathToCategory = "simu.jpg", ListClips= { } },
                new Categories {ID=6, Name="Strategy", PathToCategory = "strat.jpg", ListClips= { } },
                new Categories {ID=7, Name="Sports", PathToCategory = "sports.jpg", ListClips= { } },
                new Categories {ID=8, Name="MMO", PathToCategory = "mmo.jpg", ListClips= { } },
                new Categories {ID=9, Name="RTS", PathToCategory = "rts.jpg", ListClips= { } },
                new Categories {ID=10, Name="Fantasy", PathToCategory = "fantasy.jpg", ListClips= { } },
                new Categories {ID=11, Name="3D", PathToCategory = "3d.jpg", ListClips= { } },
                new Categories {ID=12, Name="Puzzle", PathToCategory = "puzzle.jpg", ListClips= { } },
                new Categories {ID=13, Name="Educational", PathToCategory = "educa.jpg", ListClips= { } }
            };
            categories.ForEach(ee => context.Categories.AddOrUpdate(e => e.ID, ee));
            context.SaveChanges();
        }
    }
}
