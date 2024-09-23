using FM.Common;
using FM.Common.Generic;
using FM.ViewModels;
using System;
using System.Collections;
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
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;

namespace FM.Views
{

    public partial class LineUpControl : UserControl
    {
        public LineUpControl()
        {
            DataContext = new LineUpViewModel();
            InitializeComponent();
        }

        private PlayerPositionContainer startItem;
        private UIElement ghostItem;
        private Point startPoint;

        private void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            var tmp= GetDragOverItem(sender, e);
            if (tmp?.Player != null && !(tmp?.IsOpponent ?? false))
            {
                startItem = tmp;
            }
            if (startItem != null)
            {
                startPoint = e.GetPosition(GhostContainer);
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && startItem != null)
            {
                var mousePoint = Mouse.GetPosition(GhostContainer);
                if (ghostItem == null)
                {
                    var dX = Math.Abs(mousePoint.X - startPoint.X);
                    var dY = Math.Abs(mousePoint.Y - startPoint.Y);
                    if (dX >= 5 || dY >= 5)
                    {
                        ghostItem = CreateGhostItem(startItem);
                        GhostContainer.Children.Add(ghostItem);
                    }
                }

                    if (ghostItem != null)
                    {
                        Canvas.SetLeft(ghostItem, mousePoint.X + 5);
                        Canvas.SetTop(ghostItem, mousePoint.Y + 5);
                    }

                    var targetItem = GetDragOverItem(sender, e);

                    if (startItem != null && targetItem != null && startItem != targetItem)
                    {
                        if (!IsDropAllowed(startItem, targetItem))
                        {
                            Mouse.SetCursor(Cursors.No);
                        }
                    }
                
            }
        }

            private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
            {
                if (startItem != null)
                {
                    var targetItem = GetDragOverItem(sender, e);

                    if (targetItem != null && IsDropAllowed(startItem, targetItem))
                    {
                        var tmp = targetItem.Player;
                        targetItem.Player = startItem.Player;
                        startItem.Player = tmp;
                    }

                    GhostContainer.Children.Remove(ghostItem);
                    ghostItem = null;
                    startItem = null;
                }
            }

            private PlayerPositionContainer GetDragOverItem(object sender, MouseEventArgs e)
            {
                UIElement uIElement = sender as UIElement;

                if (uIElement != null)
                {
                    var uiElement = VisualTreeHelper.HitTest(uIElement, e.GetPosition(uIElement))?.VisualHit as UIElement;
                    if (uiElement != null)
                    {
                        var parent = VisualTreeHelper.GetParent(uiElement);

                        while (parent != null)
                        {
                            if (parent is ListBoxItem listBoxItem)
                            {
                                return listBoxItem.DataContext as PlayerPositionContainer;
                            }
                            parent = VisualTreeHelper.GetParent(parent);
                        }
                    }
                }
                return null;


            }

            private bool IsDropAllowed(PlayerPositionContainer source, PlayerPositionContainer target)
            {
                return !target.IsOpponent && (!target.Position.HasValue || target.Position == source.Player.Position) && (!source.Position.HasValue || source.Position == target.Player?.Position);
            }

            //private void ListBox_PreviewDragOver(object sender, DragEventArgs e)
            //{
            //    if (!e.Data.GetDataPresent(DataFormats.Serializable) || sender == e.Source)
            //    {
            //        e.Effects = DragDropEffects.None;
            //        e.Handled = true;
            //    }

            //    var targetItem = GetDragOverItem(sender, e);
            //    if (targetItem != null)
            //    {
            //        if (IsDropAllowed(startItem, targetItem))
            //        {
            //            e.Effects = DragDropEffects.Move;
            //        }
            //        else
            //        {
            //            e.Effects = DragDropEffects.None;
            //        }

            //        e.Handled = true;
            //    }

            //}



            //private void ListBox_Drop(object sender, DragEventArgs e)
            //{
            //    var targetItem = GetDragOverItem(sender, e);
            //    if (IsDropAllowed(startItem, targetItem))
            //    {
            //        this.RemoveVisualChild(ghostItem);
            //        var tmp = targetItem.Player;
            //        targetItem.Player = startItem.Player;
            //    }

            //}


            //private PlayerPositionContainer GetDragOverItem(object sender, DragEventArgs e)
            //{
            //    var listBox = sender as ListBox;
            //    var result = VisualTreeHelper.HitTest(listBox, e.GetPosition(listBox));

            //    if (result != null && result.VisualHit is FrameworkElement targetElement)
            //    {
            //        return targetElement.DataContext as PlayerPositionContainer;
            //    }

            //    return null;
            //}



            private UIElement CreateGhostItem(PlayerPositionContainer item)
            {
                // Create a Border as the dragged item visual
                var border = new Border
                {
                    Width = 200,
                    Height = 40,
                    Background = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    BorderThickness = new Thickness(1),

                    Padding = new Thickness(5),
                    Child = new TextBlock
                    {
                        Text = item.Player.PositionStringShort + " " + item.Player.DressNumberString + " " + item.Player.FullName,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    }
                };

                return border;
            }


            private void Grid_MouseLeave(object sender, MouseEventArgs e)
            {
                if (ghostItem != null)
                {
                    GhostContainer.Children.Remove(ghostItem);
                    ghostItem = null;
                }

                if (startItem != null)
                {
                    startItem = null;
                }
            }

        private bool click = false;
        private void Player_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            click = true;
        }


        private void Player_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (click)
            {
                var p = (sender as Label).DataContext as PlayerPositionContainer;

                var pView = new PlayerWindow(p.Player);
                pView.ShowDialog();
            }
            click = false;
        }


        private void Player_MouseLeave(object sender, MouseEventArgs e)
        {
            click = false;
        }
    }
}
