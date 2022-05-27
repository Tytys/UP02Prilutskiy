using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UP02Prilutskiy.ClassFolder
{
    class DGClass
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=K306PC14\SQLEXPRESS;
                                Initial Catalog=UP02Prilutskiy;
                                Integrated Security=True");
        SqlDataAdapter dataAdapter;
        DataGrid dataGrid;
        DataTable dataTable;

        public DGClass(DataGrid dataGrid)
        {
            this.dataGrid=dataGrid;
        }

        public void LoadDG(string sqlCommand)
        {
            try
            {
                sqlConnection.Open();
                //работа с БД на sql-команды и подключения
                dataAdapter=new SqlDataAdapter(sqlCommand,sqlConnection);
                //обьявляется новая пустая виртуальная таблица
                dataTable=new DataTable();
                //заполняется виртуальная таблица на основе
                //sql-комнады и подключения
                dataAdapter.Fill(dataTable);
                //из виртуальной таблицы данные загружаются
                //в DataGrid
                dataGrid.ItemsSource=dataTable.DefaultView;
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
