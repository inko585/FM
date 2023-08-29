using AE.Graphics.Wpf.Basis;
using AE.Logging;
using FM.Common;
using FM.Common.Generic;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FM.ViewModels
{

    public class LiveViewModel : BaseViewModel
    {

        protected class PositionMatchPlayer
        {
            public PositionMatchPlayer(string posString, MatchPlayer player)
            {
                PositionString = posString;
                MatchPlayer = player;
            }

            public string PositionString { get; set; }
            public MatchPlayer MatchPlayer { get; set; }
        }

        public LiveViewModel(MatchResult mr)
        {
            Load(mr);
            TextBlocks = new ObservableCollection<TextBlock>();
            var playerColor = Game.Instance.PlayerClub.ClubColors.MainColor;
            var playerColor2 = Game.Instance.PlayerClub.ClubColors.SecondColor;
            ButtonColor = new SolidColorBrush(System.Windows.Media.Color.FromArgb(playerColor.A, playerColor.R, playerColor.G, playerColor.B));
            ButtonColor2 = new SolidColorBrush(System.Windows.Media.Color.FromArgb(playerColor2.A, playerColor2.R, playerColor2.G, playerColor2.B));
        }

        public string HomeGoals { get; set; }
        public string AwayGoals { get; set; }
        public ObservableCollection<TextBlock> TextBlocks { get; set; }

        private PositionMatchPlayer[,] PositionMatchPlayersHome = new PositionMatchPlayer[4, 3];
        private PositionMatchPlayer[,] PositionMatchPlayersAway = new PositionMatchPlayer[4, 3];

        public Club HomeClub { get; set; }
        public BitmapImage HomeDress { get; set; }
        public MatchPlayer GoalieHome => PositionMatchPlayersHome[0, 1]?.MatchPlayer;
        public MatchPlayer DefenderLeftHome => PositionMatchPlayersHome[1, 0]?.MatchPlayer;
        public MatchPlayer DefenderRightHome => PositionMatchPlayersHome[1, 2]?.MatchPlayer;
        public MatchPlayer DefenderCenterHome => PositionMatchPlayersHome[1, 1]?.MatchPlayer;
        public MatchPlayer MidfielderLeftHome => PositionMatchPlayersHome[2, 0]?.MatchPlayer;
        public MatchPlayer MidfielderRightHome => PositionMatchPlayersHome[2, 2]?.MatchPlayer;
        public MatchPlayer MidfielderCenterHome => PositionMatchPlayersHome[2, 1]?.MatchPlayer;
        public MatchPlayer StrikerLeftHome => PositionMatchPlayersHome[3, 0]?.MatchPlayer;
        public MatchPlayer StrikerRightHome => PositionMatchPlayersHome[3, 2]?.MatchPlayer;
        public MatchPlayer StrikerCenterHome => PositionMatchPlayersHome[3, 1]?.MatchPlayer;

        public Club AwayClub { get; set; }
        public BitmapImage AwayDress { get; set; }
        public MatchPlayer AwayAway => PositionMatchPlayersAway[0, 1]?.MatchPlayer;
        public MatchPlayer GoalieAway => PositionMatchPlayersAway[0, 1]?.MatchPlayer;
        public MatchPlayer DefenderLeftAway => PositionMatchPlayersAway[1, 0]?.MatchPlayer;
        public MatchPlayer DefenderRightAway => PositionMatchPlayersAway[1, 2]?.MatchPlayer;
        public MatchPlayer DefenderCenterAway => PositionMatchPlayersAway[1, 1]?.MatchPlayer;
        public MatchPlayer MidfielderLeftAway => PositionMatchPlayersAway[2, 0]?.MatchPlayer;
        public MatchPlayer MidfielderRightAway => PositionMatchPlayersAway[2, 2]?.MatchPlayer;
        public MatchPlayer MidfielderCenterAway => PositionMatchPlayersAway[2, 1]?.MatchPlayer;
        public MatchPlayer StrikerLeftAway => PositionMatchPlayersAway[3, 0]?.MatchPlayer;
        public MatchPlayer StrikerRightAway => PositionMatchPlayersAway[3, 2]?.MatchPlayer;
        public MatchPlayer StrikerCenterAway => PositionMatchPlayersAway[3, 1]?.MatchPlayer;

        public SolidColorBrush ButtonColor {get; set;}
        public SolidColorBrush ButtonColor2 { get; set; }

        private List<MatchEvent> MatchEvents { get; set; }
        public void Load(MatchResult mr)
        {
            HomeClub = mr.HomeClub;
            HomeDress = mr.HomeDress;
            AwayClub = mr.AwayClub;
            AwayDress = mr.AwayDress;
            HomeGoals = "0";
            AwayGoals = "0";
            PositionMatchPlayersHome[0, 1] = new PositionMatchPlayer(nameof(GoalieHome), mr.HomeGoalieStarter);
            PositionMatchPlayersAway[0, 1] = new PositionMatchPlayer(nameof(GoalieAway), mr.AwayGoalieStarter);
            FillRow("Home", "Defender", mr.HomeDefenderStarters);
            FillRow("Home", "Midfielder", mr.HomeMidFielderStarters);
            FillRow("Home", "Striker", mr.HomeStrikerStarters);
            FillRow("Away", "Defender", mr.AwayDefenderStarters);
            FillRow("Away", "Midfielder", mr.AwayMidFielderStarters);
            FillRow("Away", "Striker", mr.AwayStrikerStarters);

            MatchEvents = mr.MatchEvents;

        }

        public void StartMatch()
        {
            var curmin = 0;
            foreach (var e in MatchEvents)
            {
                Thread.Sleep((e.Buffer - curmin) * FootballPit.Match.TENSION);

                if (e.Substitution == null)
                {
                    foreach (var t in e.Text)
                    {

                        Thread.Sleep(FootballPit.Match.TENSION_GOAL_SHOT);


                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                            new Action(() => TextBlocks.Add(t)));
                    }

                    if (e.ScoreEvent != null)
                    {
                        HomeGoals = e.ScoreEvent.CurrentHomeGoals.ToString();
                        AwayGoals = e.ScoreEvent.CurrentAwayGoals.ToString();
                        NotifyPropertyChanged(nameof(HomeGoals));
                        NotifyPropertyChanged(nameof(AwayGoals));
                        Logger.RootLogger.Info("Goal by " + e.ScoreEvent.Scorer.Player.FullName);
                    }
                }
                else
                {
                    var matrix = e.Substitution.Club == HomeClub ? PositionMatchPlayersHome : PositionMatchPlayersAway;
                    SwitchPlayers(matrix, e.Substitution.In, e.Substitution.Out);
                    Logger.RootLogger.Info("Substitution " + e.Substitution.In.Player.FullName + " for " + e.Substitution.Out.Player.FullName);
                }

                curmin = e.Buffer;
            }
        }

        private void SwitchPlayers(PositionMatchPlayer[,] matrix, MatchPlayer playerIn, MatchPlayer playerOut)
        {

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j]?.MatchPlayer?.Player == playerOut.Player)
                    {
                        matrix[i, j].MatchPlayer = playerIn;
                        NotifyPropertyChanged(matrix[i, j].PositionString);
                        return;
                    }
                }
            }
        }
        private void FillRow(string home_away, string position, List<MatchPlayer> players)
        {
            var positionId = 0;
            var arr = home_away == "Home" ? PositionMatchPlayersHome : PositionMatchPlayersAway;
            switch (position)
            {
                case "Defender":
                    positionId = 1; break;
                case "Midfielder":
                    positionId = 2; break;
                case "Striker":
                    positionId = 3; break;
            }

            var playersOrderd = players.OrderByDescending(p => p.Player.SkillMax);
            if (players.Count() == 1)
            {
                arr[positionId, 1] = new PositionMatchPlayer(position + "Center" + home_away, players.Single());
            }
            if (players.Count() == 2)
            {
                var order = players.OrderBy(p => p.Player.FullName.GetHashCode());
                arr[positionId, 2] = new PositionMatchPlayer(position + "Right" + home_away, order.First());
                arr[positionId, 0] = new PositionMatchPlayer(position + "Left" + home_away, order.Last());
            }
            if (players.Count() == 3)
            {
                var max = players.OrderByDescending(pl => pl.Player.SkillMax).First();
                var order = players.Where(pl => pl != max).OrderBy(p => p.Player.FullName.GetHashCode());

                arr[positionId, 1] = new PositionMatchPlayer(position + "Center" + home_away, max);
                arr[positionId, 2] = new PositionMatchPlayer(position + "Right" + home_away, order.First());
                arr[positionId, 0] = new PositionMatchPlayer(position + "Left" + home_away, order.Last());
            }
        }

    }
}
