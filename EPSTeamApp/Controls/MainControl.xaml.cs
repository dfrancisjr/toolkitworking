using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
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
using DataTable = System.Data.DataTable;

namespace EPSTeamApp.Controls
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        string Default;
        SQLiteConnection mConn;
        SQLiteDataAdapter mAdapter;
        DataTable mTable;
        SQLiteCommand mCmd;
        string editId, editTitle, editType, editResources, editEnteredDate, editCompleted, editDescription, editReminder;
        //SQLiteDataReader reader;
        object item;

        public MainControl()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            Default = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            try
            {
                using (mConn = new SQLiteConnection(Default))
                {
                    mConn.Open();

                    {
                        mCmd = new SQLiteCommand("SELECT Id, Title, Type, Resource, Entered, Completed, Reminder, Description FROM Tasks order by Id", mConn);

                        mCmd.ExecuteNonQuery();
                        mAdapter = new SQLiteDataAdapter(mCmd);
                        mTable = new DataTable("Tasks");
                        mAdapter.Fill(mTable);
                        mConn.Close();
                        TasksDataGrid.ItemsSource = mTable.DefaultView;
                        mAdapter.Update(mTable);

                        this.TasksDataGrid.Columns[0].Header = "Id";
                        this.TasksDataGrid.Columns[1].Header = "Title";
                        this.TasksDataGrid.Columns[2].Header = "Type";
                        this.TasksDataGrid.Columns[3].Header = "Resource";
                        this.TasksDataGrid.Columns[4].Header = "Entered";
                        this.TasksDataGrid.Columns[5].Header = "Completed";
                        this.TasksDataGrid.Columns[6].Header = "Reminder";
                        this.TasksDataGrid.Columns[7].Header = "Description";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex);
            }

        }

        private void InputSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            InputTasksForm.Visibility = System.Windows.Visibility.Collapsed;
            //Do Something Here
            Default = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            try
            {
                using (mConn = new SQLiteConnection(Default))
                {
                    mConn.Open();
                    //Insert Command
                    mCmd = new SQLiteCommand("insert into Tasks (Title, Type, Resource, Entered, Completed, Reminder, Description) " +
                        "values('" + this.TitleTextbox.Text.ToString() + "','" + this.TypetDropDown.Text.ToString() + "','" + this.ResourcesTextbox.Text.ToString() + "','" + this.EnteredTextbox.Text.ToString() + "','" + this.CompletedTextbox.Text.ToString() + "','" + this.ReminderDate.Text.ToString() + "','" + this.DescriptionTextbox.Text.ToString() + "')", mConn);
                    mCmd.ExecuteNonQuery();
                    mCmd = null;
                    //Select Command
                    mCmd = new SQLiteCommand("SELECT Id, Title, Type, Resource, Entered, Completed, Reminder, Description FROM Tasks order by Id", mConn);
                    mCmd.ExecuteNonQuery();
                    mAdapter = new SQLiteDataAdapter(mCmd);
                    mTable = new DataTable("Tasks");

                    mAdapter.Fill(mTable);
                    mConn.Close();
                    TasksDataGrid.ItemsSource = mTable.DefaultView;
                    mAdapter.Update(mTable);
                    this.TasksDataGrid.Columns[0].Header = "Id";
                    this.TasksDataGrid.Columns[1].Header = "Title";
                    this.TasksDataGrid.Columns[2].Header = "Type";
                    this.TasksDataGrid.Columns[3].Header = "Resource";
                    this.TasksDataGrid.Columns[4].Header = "Entered";
                    this.TasksDataGrid.Columns[5].Header = "Completed";
                    this.TasksDataGrid.Columns[6].Header = "Reminder";
                    this.TasksDataGrid.Columns[7].Header = "Description";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex);
            }


            //ENd Here
            //IdTextBox.Text = String.Empty;
            TitleTextbox.Text = String.Empty;
            TypetDropDown.Text = String.Empty;
            ResourcesTextbox.Text = String.Empty;
            EnteredTextbox.Text = String.Empty;
            CompletedTextbox.Text = String.Empty;
            DescriptionTextbox.Text = String.Empty;
            ReminderDate.Text = String.Empty;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RowView.Visibility = System.Windows.Visibility.Visible;

        }

        private void CloseView_Click(object sender, RoutedEventArgs e)
        {
            RowView.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Delete_Tasks_Click(object sender, RoutedEventArgs e)
        {
            Default = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            try
            {
                using (mConn = new SQLiteConnection(Default))
                {
                    mConn.Open();
                    //Insert Command
                    mCmd = new SQLiteCommand("delete from Tasks where id = " + Int32.Parse(editId), mConn);
                    mCmd.ExecuteNonQuery();
                    mCmd = null;
                    //Select Command
                    mCmd = new SQLiteCommand("SELECT Id, Title, Type, Resource, Entered, Completed, Reminder, Description FROM Tasks order by Id", mConn);
                    mCmd.ExecuteNonQuery();
                    mAdapter = new SQLiteDataAdapter(mCmd);
                    mTable = new DataTable("Tasks");

                    mAdapter.Fill(mTable);
                    mConn.Close();
                    TasksDataGrid.ItemsSource = mTable.DefaultView;
                    mAdapter.Update(mTable);
                    this.TasksDataGrid.Columns[0].Header = "Id";
                    this.TasksDataGrid.Columns[1].Header = "Title";
                    this.TasksDataGrid.Columns[2].Header = "Type";
                    this.TasksDataGrid.Columns[3].Header = "Resource";
                    this.TasksDataGrid.Columns[4].Header = "Entered";
                    this.TasksDataGrid.Columns[5].Header = "Completed";
                    this.TasksDataGrid.Columns[6].Header = "Reminder";
                    this.TasksDataGrid.Columns[7].Header = "Description";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex);
            }
        }


        private void EditSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!EditIdTextBox.Text.Equals(editId) || !EditTitleBox.Text.Equals(editTitle) || !EditTypetDropDown.Text.Equals(editType) || !EditResourcesBox.Text.Equals(editResources) || !EditEnteredDateBox.Text.Equals(editEnteredDate) || !EditCompletedBox.Text.Equals(editCompleted) || !EditReminderDate.Text.Equals(editReminder) || !EditDescriptionBox.Text.Equals(editDescription))
            {
                EditTasksForm.Visibility = System.Windows.Visibility.Collapsed;
                try
                {
                    using (mConn = new SQLiteConnection(Default))
                    {
                        mConn.Open();
                        //Insert Command
                        mCmd = new SQLiteCommand("update Tasks SET Title ='" + this.EditTitleBox.Text.ToString() + "', Type ='" + this.EditTypetDropDown.Text.ToString() + "', Resource ='" + this.EditResourcesBox.Text.ToString() + "', Started ='" + this.EditEnteredDateBox.Text.ToString() + "', Completed ='" + this.EditCompletedBox.Text.ToString() + "', Reminder ='" + this.EditReminderDate.Text.ToString() + "', Description ='" + this.EditDescriptionBox.Text.ToString() + "' where id =" + Int32.Parse(this.EditIdTextBox.Text.ToString()), mConn);
                        mCmd.ExecuteNonQuery();
                        mCmd = null;
                        //Select Command
                        mCmd = new SQLiteCommand("SELECT Id, Title, Type, Resource, Entered, Completed, Reminder, Description FROM Tasks order by Id", mConn);
                        mCmd.ExecuteNonQuery();
                        mAdapter = new SQLiteDataAdapter(mCmd);
                        mTable = new System.Data.DataTable("Tasks");

                        mAdapter.Fill(mTable);
                        mConn.Close();
                        TasksDataGrid.ItemsSource = mTable.DefaultView;
                        //mAdapter.Update(mTable);
                        this.TasksDataGrid.Columns[0].Header = "Id";
                        this.TasksDataGrid.Columns[1].Header = "Title";
                        this.TasksDataGrid.Columns[2].Header = "Type";
                        this.TasksDataGrid.Columns[3].Header = "Resource";
                        this.TasksDataGrid.Columns[4].Header = "Entered";
                        this.TasksDataGrid.Columns[5].Header = "Completed";
                        this.TasksDataGrid.Columns[6].Header = "Reminder";
                        this.TasksDataGrid.Columns[7].Header = "Description";

                        //mConn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Message : " + ex);
                }
                //EditIdTextBox.Text = String.Empty;
                EditTitleBox.Text = String.Empty;
                EditTypetDropDown.Text = String.Empty;
                EditResourcesBox.Text = String.Empty;
                EditEnteredDateBox.Text = String.Empty;
                EditCompletedBox.Text = String.Empty;
                EditReminderDate.Text = String.Empty;
                EditDescriptionBox.Text = String.Empty;
                editId = String.Empty;
                editTitle = String.Empty;
                editType = String.Empty;
                editResources = String.Empty;
                editEnteredDate = String.Empty;
                editCompleted = String.Empty;
                editReminder = String.Empty;
                editDescription = String.Empty;
            }
            else
            {
                MessageBox.Show("No Entry Changed.");
            }
        }

        private void Add_Tasks_Click(object sender, RoutedEventArgs e)
        {
            InputTasksForm.Visibility = System.Windows.Visibility.Visible;
            IdTextBox.Visibility = System.Windows.Visibility.Collapsed;
            IdTextBlock.Visibility = System.Windows.Visibility.Collapsed;
            CompletedTextBlock.Visibility = System.Windows.Visibility.Collapsed;
            CompletedTextbox.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void InputCancelButton_Click(object sender, RoutedEventArgs e)
        {
            InputTasksForm.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void EditCancelButton_Click(object sender, RoutedEventArgs e)
        {
            EditTasksForm.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Edit_Tasks_Click(object sender, RoutedEventArgs e)
        {
            EditTasksForm.Visibility = System.Windows.Visibility.Visible;
            EditIdTextBox.Text = editId;
            EditTitleBox.Text = editTitle;
            EditTypetDropDown.Text = editType;
            EditResourcesBox.Text = editResources;
            EditEnteredDateBox.Text = editEnteredDate;
            EditCompletedBox.Text = editCompleted;
            EditReminderDate.Text = editReminder;
            EditDescriptionBox.Text = editDescription;
        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        private void TasksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.TasksDataGrid.SelectedItem != null)
                {
                    item = this.TasksDataGrid.SelectedItem;
                    editId = (this.TasksDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    this.EditIdTextBox.Text = editId;
                    editTitle = (this.TasksDataGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    this.EditTitleBox.Text = editTitle;
                    editType = (this.TasksDataGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                    this.EditTypetDropDown.Text = editType;
                    editResources = (this.TasksDataGrid.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                    this.EditResourcesBox.Text = editResources;
                    editEnteredDate = (this.TasksDataGrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                    this.EditEnteredDateBox.Text = editEnteredDate;
                    editCompleted = (this.TasksDataGrid.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                    this.EditCompletedBox.Text = editCompleted;
                    editReminder = (this.TasksDataGrid.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;
                    this.EditReminderDate.Text = editReminder;
                    editDescription = (this.TasksDataGrid.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;
                    this.EditDescriptionBox.Text = editDescription;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Message:" + exp);
            }
        }

        private void Create_Report_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
