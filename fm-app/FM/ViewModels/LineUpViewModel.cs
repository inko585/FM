using AE.Graphics.Wpf.Basis;
using FM.Common.Generic;
using FM.Common.Season;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FM.ViewModels
{
    public class LineUpViewModel : BaseViewModel
    {

        public static LineUpViewModel Instance { get; set; }

        public LineUp LineUp => new LineUp(StartingPlayers.Select(p => p.Player).ToList(), null, null, SelectedTactic.TacticValue, SelectedTacklingIntensity.TacklingValue, SelectedShotFrequency.FrequencyValue);

        public LineUpViewModel()
        {
            Instance = this;
            StartingPlayers = new ObservableCollection<PlayerPositionContainer>();
            Bench = new ObservableCollection<PlayerPositionContainer>();
            Rest = new ObservableCollection<PlayerPositionContainer>();

            Season.OnSeasonChange += (o, e) => Subscribe();
            Subscribe();
            SubscribeToPlayerClub();
            StartingPlayers.CollectionChanged += (o, e) =>
            {
                NotifyPropertyChanged(nameof(SetPlayTaker));
            };

            Formations = new ObservableCollection<Formation>()
            {
                new Formation("2-1-2", 2, 1, 2),
                new Formation("1-2-2", 1, 2, 2),
                new Formation("2-2-1", 2, 2, 1),
                new Formation("1-3-1", 1, 3, 1)
            };

            Tactics = new ObservableCollection<Tactic>
            {
                new Tactic(Common.Generic.Tactic.Offensive, "Offensiv"),
                new Tactic(Common.Generic.Tactic.Defensive, "Defensiv")
            };

            ShotFrequencies = new ObservableCollection<ShotFrequency>
            {
                new ShotFrequency("Fernschüsse", Frequency.High),
                new ShotFrequency("Ausgeglichen", Frequency.Normal),
                new ShotFrequency("Ball ins Tor tragen", Frequency.Seldom)
            };

            TacklingIntensities = new ObservableCollection<TacklingIntensity>
            {
                new TacklingIntensity("Brutal", Tackling.Brutal),
                new TacklingIntensity("Ausgeglichen", Tackling.Normal),
                new TacklingIntensity("Sauber", Tackling.Clean)

            };

            var ph = PlayerClub.Coach.Philospophie;
            SelectedFormation = Formations.Single(f => f.Positions.SequenceEqual(ph.GetPreferredFormation()));
            SelectedShotFrequency = ShotFrequencies.Single(sf => sf.FrequencyValue == ph.GetPreferredShotFrequency());
            SelectedTacklingIntensity = TacklingIntensities.Single(ti => ti.TacklingValue == ph.GetPreferredTackling());
            SelectedTactic = Tactics.Single(t => t.TacticValue == ph.GetPreferredTactic());

        }

        public void Subscribe()
        {
            Season.CurrentSeason.OnSeasonProgress += (o, e) =>
            {
                NotifyPropertyChanged(nameof(OppositeTeamLineup));
                NotifyPropertyChanged(nameof(SetPlayTakerOpponent));
                foreach (var p in PlayerClub.Rooster)
                {
                    p.NotifyPropertyChanged(nameof(p.InjuryLabel));
                    p.NotifyPropertyChanged(nameof(p.InjuryTimeString));
                    p.NotifyPropertyChanged(nameof(p.MatchValues));
                }
            };
            NotifyPropertyChanged(nameof(OppositeTeamLineup));
            NotifyPropertyChanged(nameof(SetPlayTakerOpponent));
            foreach (var p in PlayerClub.Rooster)
            {
                p.NotifyPropertyChanged(nameof(p.InjuryLabel));
                p.NotifyPropertyChanged(nameof(p.InjuryTimeString));
                p.NotifyPropertyChanged(nameof(p.MatchValues));
            }
        }

        public void SubscribeToPlayerClub()
        {
            PlayerClub.Rooster.CollectionChanged += (o, e) =>
            {
                if (e.NewItems != null)
                {
                    var currentRest = Rest.Select(pc => pc.Player).ToList();


                    foreach (Player np in e.NewItems)
                    {
                        currentRest.Add(np);
                    }
                    AddRestPlayers(currentRest.Where(r => r != null));
                }

                if (e.OldItems != null)
                {

                    foreach (var r in Rest)
                    {
                        if (e.OldItems.Contains(r.Player))
                        {
                            r.Player = null;
                            break;
                        }
                    }

                    foreach (var b in Bench)
                    {
                        if (e.OldItems.Contains(b.Player))
                        {
                            var bestRestOption = Rest.OrderByDescending(r => r.Player?.ValueForCoach ?? 0).First();
                            b.Player = bestRestOption.Player;
                            bestRestOption.Player = null;
                            break;
                        }
                    }

                    foreach (var pc in StartingPlayers)
                    {
                        if (e.OldItems.Contains(pc.Player))
                        {
                            var benchOptions = Bench.Where(b => b.Player?.Position == pc.Player.Position).OrderByDescending(b => b.Player.ValueForCoach);
                            var bestRestOption = Rest.OrderByDescending(r => r.Player?.ValueForCoach ?? 0).First();
                            if (benchOptions.Any())
                            {
                                pc.Player = benchOptions.First().Player;
                                benchOptions.First().Player = bestRestOption.Player;
                                bestRestOption.Player = null;
                            }
                            else
                            {
                                pc.Player = bestRestOption.Player;
                                bestRestOption.Player = null;
                            }
                            break;
                        }
                    }



                    var restPlayers = Rest.Select(pc => pc.Player).Where(p => p != null).ToList();
                    AddRestPlayers(restPlayers);
                }
            };
        }

        public ObservableCollection<PlayerPositionContainer> StartingPlayers { get; set; }

        public ObservableCollection<PlayerPositionContainer> OppositeTeamLineup
        {
            get
            {
                if (Game.Instance.NextPlayerOpponent != null)
                {
                    return new ObservableCollection<PlayerPositionContainer>(Game.Instance.NextPlayerOpponent.StartingLineUp.Players.Select(p => new PlayerPositionContainer(p.Position, p, true)));
                }
                return new ObservableCollection<PlayerPositionContainer>();
            }
        }

        public ObservableCollection<PlayerPositionContainer> Bench { get; set; }

        public ObservableCollection<PlayerPositionContainer> Rest { get; set; }

        private Club PlayerClub => Game.Instance.PlayerClub;

        public ObservableCollection<Formation> Formations { get; set; }

        public ObservableCollection<Tactic> Tactics { get; set; }

        public ObservableCollection<ShotFrequency> ShotFrequencies { get; set; }

        public ObservableCollection<TacklingIntensity> TacklingIntensities { get; set; }

        public Player SetPlayTaker => StartingPlayers.Where(p => p.Position != Position.Goalie).OrderByDescending(p => p.Player.SetPlaySkill).FirstOrDefault()?.Player;

        public Player SetPlayTakerOpponent => Game.Instance.NextPlayerOpponent?.StartingLineUp?.FieldPlayers?.OrderByDescending(p => p.SetPlaySkill)?.First();

        private Formation selectedFormation;

        public Formation SelectedFormation
        {
            get { return selectedFormation; }
            set { selectedFormation = value; SetupLineup(); }
        }

        public Tactic SelectedTactic { get; set; }
        public ShotFrequency SelectedShotFrequency { get; set; }
        public TacklingIntensity SelectedTacklingIntensity { get; set; }

        public void SetupLineup()
        {
            var lineup = Coach.GetLineUpPlayers(PlayerClub.Rooster, p => p.ValueForCoach, SelectedFormation.Positions.ToArray());
            AddStartingPlayers(lineup);

            var goalie = PlayerClub.Rooster.Where(p => !StartingPlayers.Any(sl => sl.Player == p) && p.Position == Position.Goalie).OrderByDescending(p => p.ValueForCoach).FirstOrDefault();
            var restPlayers = PlayerClub.Rooster.Where(p => !StartingPlayers.Any(sl => sl.Player == p) && p.Position != Position.Goalie).OrderByDescending(p => p.ValueForCoach).Take(5);
            AddBenchPlayers(restPlayers.Append(goalie));
            restPlayers = PlayerClub.Rooster.Where(p => !StartingPlayers.Any(sl => sl.Player == p) && !Bench.Any(sl => sl.Player == p)).OrderBy(p => (int)p.Position);
            AddRestPlayers(restPlayers);

        }

        public void AddStartingPlayers(IEnumerable<Player> players)
        {
            StartingPlayers.Clear();
            foreach (var p in players.OrderBy(p => p.Position))
            {
                StartingPlayers.Add(new PlayerPositionContainer(p.Position, p));
            }
        }

        public void AddBenchPlayers(IEnumerable<Player> players)
        {
            Bench.Clear();
            var restPlayersOrdered = players.OrderBy(p => (int)p.Position);




            for (int i = 0; i < 6; i++)
            {
                if (restPlayersOrdered.Count() > i)
                {
                    Bench.Add(new PlayerPositionContainer(null, restPlayersOrdered.ElementAt(i)));
                } else
                {
                    Bench.Add(new PlayerPositionContainer(null, null));
                }
            }


        }

        public void AddRestPlayers(IEnumerable<Player> players)
        {
            Rest.Clear();
            var border = Math.Max(players.Count(), 6);
            for (int i = 0; i < border; i++)
            {
                if (players.Count() > i)
                {
                    Rest.Add(new PlayerPositionContainer(null, players.ElementAt(i)));
                }
                else
                {
                    Rest.Add(new PlayerPositionContainer(null, null));
                }
            }
        }
    }

    public class PlayerPositionContainer : BaseViewModel
    {

        private Player player;

        public Position? Position { get; set; }

        public bool IsOpponent { get; set; }


        public Player Player
        {
            get => player;
            set
            {
                player = value;
                NotifyPropertyChanged(nameof(Player));
                LineUpViewModel.Instance.NotifyPropertyChanged("SetPlayTaker");
            }
        }

        public PlayerPositionContainer(Position? position, Player player, bool isOpponent = false)
        {
            Position = position;
            Player = player;
            IsOpponent = isOpponent;
        }

        public SolidColorBrush Color
        {
            get
            {
                if (IsOpponent)
                {
                    return new SolidColorBrush(Colors.White);
                }
                if (Position.HasValue)
                {
                    switch (Position.Value)
                    {
                        case FM.Common.Generic.Position.Goalie: return new SolidColorBrush(Colors.LightGreen);
                        case FM.Common.Generic.Position.Defender: return new SolidColorBrush(Colors.Violet);
                        case FM.Common.Generic.Position.Midfielder: return new SolidColorBrush(Colors.Khaki);
                        case FM.Common.Generic.Position.Striker: return new SolidColorBrush(Colors.LightSkyBlue);
                    }
                }
                else
                {
                    return new SolidColorBrush(Colors.LightGray);
                }

                return null;
            }
        }
    }

    public class Formation
    {
        public Formation(string name, params int[] positions)
        {
            Name = name;
            Positions = positions;
        }

        public string Name { get; set; }
        public int[] Positions { get; set; }
    }

    public class Tactic
    {
        public Tactic(Common.Generic.Tactic tacticValue, string name)
        {
            TacticValue = tacticValue;
            Name = name;
        }

        public FM.Common.Generic.Tactic TacticValue { get; set; }
        public string Name { get; set; }
    }

    public class ShotFrequency
    {
        public ShotFrequency(string name, Frequency frequencyValue)
        {
            Name = name;
            FrequencyValue = frequencyValue;
        }

        public string Name { get; set; }
        public FM.Common.Generic.Frequency FrequencyValue { get; set; }
    }

    public class TacklingIntensity
    {
        public TacklingIntensity(string name, Tackling tacklingValue)
        {
            Name = name;
            TacklingValue = tacklingValue;
        }

        public string Name { get; set; }
        public FM.Common.Generic.Tackling TacklingValue { get; set; }
    }

}
