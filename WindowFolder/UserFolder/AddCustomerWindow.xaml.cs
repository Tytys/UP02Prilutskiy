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
using UP02Prilutskiy.WindowFolder.AdminFolder;

namespace UP02Prilutskiy.WindowFolder.UserFolder
{
    /// <summary>
    /// Логика взаимодействия для AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=K306PC14\SQLEXPRESS;
                                Initial Catalog=UP02Prilutskiy;
                                Integrated Security=True");
        SqlCommand SqlCommand;
        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Не введена фамилия");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTb.Text))
            {
                MBClass.ErrorMB("Не введено имя");
                PasswordTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(LastnameTb.Text))
            {
                MBClass.ErrorMB("Номер телефона не введен");
                LastnameTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(BirthTb.Text))
            {
                MBClass.ErrorMB("Дата рождения не введена");
                BirthTb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand = new SqlCommand("Insert Into dbo.[Customer] " +
                        "(LastNameCustomer, FirstNameCustomer, MiddleNameCustomer, NumberPhoneCustomer, EmailCustomer, DateOfBirthCustomer) " +
                        $"Values ('{LoginTb.Text}'," +
                        $"'{PasswordTb.Text}'," +
                        $"'{LastnameTb.Text}'," +
                        $"'{NameTb.Text}'," +
                        $"'{EmailTb.Text}'," +
                        $"'{BirthTb.Text}')",
                        sqlConnection);
                    SqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Заказчик {LoginTb.Text} " +
                        $"{PasswordTb.Text} успешно добавлен");
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
    

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuUserWindow().Show();
            Close();
        }
    }
}
