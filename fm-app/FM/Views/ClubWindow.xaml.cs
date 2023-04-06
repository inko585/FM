﻿using AE.Graphics.Wpf;
using FM.Common.Generic;
using FM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FM.Views
{
    /// <summary>
    /// Interaction logic for ClubWindow.xaml
    /// </summary>
    public partial class ClubWindow : AEWindow
    {
        public ClubWindow(Club c)
        {
            DataContext = new ClubViewModel(c);
            InitializeComponent();
        }


        private void TransferNameLabel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var t = (sender as Label).DataContext as Transfer;

            var pView = new PlayerWindow(t.Player);
            pView.ShowDialog();
        }

        private void NameLabel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = (sender as Label).DataContext as Player;

            var pView = new PlayerWindow(p);
            pView.ShowDialog();
        }

        private void From_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var t = (sender as Label).DataContext as Transfer;

            var cView = new ClubWindow(t.From);
            cView.ShowDialog();
        }
        private void To_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var t = (sender as Label).DataContext as Transfer;

            var cView = new ClubWindow(t.To);
            cView.ShowDialog();
        }


    }
}
