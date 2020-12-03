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
    public class TypeAccountDAO
    {
        private static TypeAccountDAO instance;

        public static TypeAccountDAO Instance
        {
            get { if (instance == null) instance = new TypeAccountDAO(); return TypeAccountDAO.instance; }
            private set => instance = value;
        }

        private TypeAccountDAO() { }

        public List<TypeAccount> GetListTypeAccount()
        {
            List<TypeAccount> list = new List<TypeAccount>();

            string query = "SELECT * FROM TypeAccount";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                TypeAccount ta = new TypeAccount(item);

                list.Add(ta);
            }

            return list;
        }

        public TypeAccount GetTypeAccountByID(int id)
        {
            TypeAccount typeAccount = null;

            string query = "SELECT * FROM TypeAccount WHERE Id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                typeAccount = new TypeAccount(item);

                return typeAccount;
            }

            return typeAccount;
        }

        public bool InsertTypeAccount(string name)
        {
            string query = string.Format("INSERT dbo.TypeAccount ( Name ) VALUES( N'{0}' )", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool EditTypeAccount(int id, string name)
        {
            string query = string.Format("UPDATE dbo.TypeAccount SET Name = N'{0}' WHERE id = {1} ", name, id);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteTypeAccount(int id)
        {
            try
            {
                string query = string.Format("DELETE TypeAccount WHERE Id = {0} ", id);

                int result = DataProvider.Instance.ExecuteNonQuery(query);

                return result > 0;
            }
            catch
            {
                MessageBox.Show("Đang tồn tại tài khoản có chức vụ này. Bạn không thể xoá.");
            }
            return false;
        }
    }
}
