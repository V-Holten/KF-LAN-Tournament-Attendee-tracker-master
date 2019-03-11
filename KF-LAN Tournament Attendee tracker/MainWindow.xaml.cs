using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static KF_LAN_Tournament_Attendee_tracker.Controller;

namespace KF_LAN_Tournament_Attendee_tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller controller = new Controller();
        public MainWindow()
        {
            InitializeComponent();
            controller.GetAttendees(this);
        }

        private void AddAttendeeButton_Click(object sender, RoutedEventArgs e)
        {
            controller.AddAttendees(NameBox.Text, TableBox.Text, SeatBox.Text, TournamentBox.Text, TeamBox.Text);
            ListOfAttendees.Items.Add(new Attendee
            {
                Name = NameBox.Text,
                Table = TableBox.Text,
                Seat = SeatBox.Text,
                Team = TeamBox.Text,
                Tournament = TournamentBox.Text
            });

            NameBox.Text = "Name";
            TableBox.Text = "Table";
            SeatBox.Text = "Seat";
            TournamentBox.Text = "Tournament";
            TeamBox.Text = "Team";
        }

        private void DeleteAttendeeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Attendee attendee = (Attendee)ListOfAttendees.SelectedItems[0];
                controller.DeleteAttendee(attendee.Name);
                ListOfAttendees.Items.RemoveAt(ListOfAttendees.SelectedIndex);
            }
            catch (System.ArgumentOutOfRangeException) { }
        }
        private void Box_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box.Text == box.Tag.ToString())
            {
                box.Text = "";
            }
        }

        private void Box_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box.Text == "")
            {
                box.Text = box.Tag.ToString();
            }
        }

        GridViewColumnHeader lastHeaderClicked = null;
        ListSortDirection lastDirection = ListSortDirection.Ascending;

        private void NameHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            ListSortDirection direction;
            if (column != lastHeaderClicked)
            {
                direction = ListSortDirection.Ascending;
            }
            else
            {
                if (lastDirection == ListSortDirection.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                }
            }

            string header = column.Tag.ToString();
            ListOfAttendees.Items.SortDescriptions.Clear();
            ListOfAttendees.Items.SortDescriptions.Add(new SortDescription(header, direction));

            lastDirection = direction;
            lastHeaderClicked = column;
        }
    }
}
