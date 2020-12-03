using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance 
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set => instance = value; 
        }

        public static int TableWidth = 100;
        public static int TableHeight = 100;

        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("USP_SwitchTable @idTable1 , @idTable2 ", new object[] { id1 , id2 });
        }

        public bool InsertTable(string name)
        {
            string query = string.Format("INSERT dbo.TableFood ( Name ) VALUES( N'{0}' )", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool EditTable(int id, string name, string status)
        {
            string query = string.Format("UPDATE dbo.TableFood SET Name = N'{0}', Status = N'{1}' WHERE id = {2} ", name, status, id);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteTable(int id)
        {
            try
            {
                string query = string.Format("DELETE TableFood WHERE Id = {0} ", id);

                int result = DataProvider.Instance.ExecuteNonQuery(query);

                return result > 0;
            }
            catch
            {
                MessageBox.Show("Thanh toán có bàn này hiện không thể xoá!");
            }
            return false;
        }

        public List<Table> SearchTableByName(string name)
        {
            List<Table> list = new List<Table>();

            string query = string.Format("SELECT * FROM dbo.TableFood WHERE dbo.fuConvertToUnsign1(Name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            string queryy = string.Format("SELECT * FROM dbo.TableFood WHERE dbo.fuConvertToUnsign1(Status) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            DataTable dataa = DataProvider.Instance.ExecuteQuery(queryy);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);

                list.Add(table);
            }

            foreach (DataRow item in dataa.Rows)
            {
                Table tablee = new Table(item);

                list.Add(tablee);
            }


            return list;
        }
    }
}
