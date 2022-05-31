using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using UP02Prilutskiy.ClassFolder;

namespace UP02Prilutskiy.WindowFolder.UserFolder
{
    /// <summary>
    /// Логика взаимодействия для EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=K306PC14\SQLEXPRESS;
                                Initial Catalog=UP02Prilutskiy;
                                Integrated Security=True");
        SqlCommand SqlCommand;
        SqlDataReader dataReader;
        public EditCustomerWindow()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuUserWindow().ShowDialog();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[Customer] " +
                    $"Set LastNameCustomer ='{LoginTb.Text}'," +
                    $"FirstNameCustomer='{PasswordTb.Text}'," +
                    $"MiddleNameCustomer='{NameTb.Text}'," +
                    $"NumberPhoneCustomer='{LastnameTb.Text}'," +
                    $"EmailCustomer='{EmailTb.Text}'," +
                    $"DateOfBirthCustomer='{BirthTb.Text}' " +
                    $"Where IdCustomer='{VariableClass.CustomerId}'",
                    sqlConnection);
                SqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Данные пользователя " +
                    $"{LoginTb.Text} {PasswordTb.Text} " +
                    $"успешно отредактированы");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand = new SqlCommand("Select * from dbo.[Customer] " +
                    $"Where idCustomer='{VariableClass.CustomerId}'",
                    sqlConnection);
                dataReader = SqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                PasswordTb.Text = dataReader[2].ToString();
                NameTb.Text = dataReader[4].ToString();
                LastnameTb.Text = dataReader[3].ToString();
                EmailTb.Text = dataReader[5].ToString();
                BirthTb.Text = dataReader[6].ToString();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
