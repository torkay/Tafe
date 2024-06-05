using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using static GroupForge.FileManager;

namespace GroupForge
{
    public partial class MainWindow : Window
    {
        private List<Data.TeamMember> members = new List<Data.TeamMember>();
        private string FilePath = "C:\\Users\\Tor\\Source\\Repos\\Tafe\\ICTPRG430\\GroupForge\\GroupForge\\Storage\\members.json";

        public MainWindow()
        {
            InitializeComponent();
            members = FileManager.ReadFromFile(FilePath);
            UpdateTable();
        }

        private void savebtn_click(object sender, RoutedEventArgs e)
        {
            if (IsFormFilledCorrectly())
            {
                string firstName = first_name.Text;
                string lastName = last_name.Text;
                string jobTitle = job_title.Text;
                string emailAddress = email_address.Text;
                string contactNumber = contact_number.Text;

                Data.TeamMember teamMember = new Data.TeamMember
                {
                    First = firstName,
                    Last = lastName,
                    JobTitle = jobTitle,
                    Email = emailAddress,
                    ContactNumber = contactNumber
                };

                members.Add(teamMember);

                UpdateTable();
                ClearEntries();
                FileManager.SaveToFile(members, FilePath);
            }
        }

        private bool IsFormFilledCorrectly()
        {
            if (string.IsNullOrWhiteSpace(first_name.Text))
            {
                MessageBox.Show("First name input blank.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(last_name.Text))
            {
                MessageBox.Show("Last name input blank.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(job_title.Text))
            {
                MessageBox.Show("Job title input blank.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(email_address.Text))
            {
                MessageBox.Show("Email address input blank.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(contact_number.Text))
            {
                MessageBox.Show("Contact number input blank.");
                return false;
            }

            return true;
        }

        private void UpdateTable()
        {
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = members;
        }

        private void ClearEntries()
        {
            first_name.Text = "";
            last_name.Text = "";
            job_title.Text = "";
            email_address.Text = "";
            contact_number.Text = "";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes before closing?", "Save Changes", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                FileManager.SaveToFile(members, FilePath);
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true; // Cancel the window closing
            }
            // If result is No, do nothing and allow the window to close
        }
    }
}
