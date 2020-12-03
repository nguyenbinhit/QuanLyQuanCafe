using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        internal static FoodDAO Instance 
        { 
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; } 
            private set => instance = value; 
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();

            string query = "SELECT * FROM Food WHERE IdCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

        //public List<Food> GetListFood()
        //{
        //    List<Food> list = new List<Food>();

        //    string query = "SELECT Food.ID, Food.Name, FoodCategory.Name, Food.Price FROM Food INNER JOIN FoodCategory ON Food.IdCategory = FoodCategory.Id";

        //    DataTable data = DataProvider.Instance.ExecuteQuery(query);

        //    foreach (DataRow item in data.Rows)
        //    {
        //        Food food = new Food(item);

        //        list.Add(food);
        //    }

        //    return list;
        //}

        public DataTable GetListFood()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Food.ID, Food.Name, FoodCategory.Name, Food.Price FROM Food INNER JOIN FoodCategory ON Food.IdCategory = FoodCategory.Id");
        }

        public bool InsertFood(string name, int id, float price)
        {
            string query = string.Format("INSERT dbo.Food ( Name, IdCategory, Price ) VALUES( N'{0}', {1}, {2} )", name, id, price);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool EditFood(int id, string name, int idCategory, float price)
        {
            string query = string.Format("UPDATE dbo.Food SET Name = N'{0}', IdCategory = {1}, Price = {2} WHERE id = {3} ", name, idCategory, price, id);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteFood(int id)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(id);

            string query = string.Format("DELETE Food WHERE Id = {0} ", id);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();

            string query = string.Format("SELECT * FROM dbo.Food WHERE dbo.fuConvertToUnsign1(Name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);

                list.Add(food);
            }

            return list;
        }
    }
}
