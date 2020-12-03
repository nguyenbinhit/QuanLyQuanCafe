using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class bAdmin : Form
    {
        BindingSource foodList = new BindingSource();

        BindingSource accountList = new BindingSource();

        BindingSource categoryList = new BindingSource();

        BindingSource tableList = new BindingSource();

        BindingSource typeAccountList = new BindingSource();

        public Account loginAccount;

        public bAdmin()
        {
            InitializeComponent();

            Loading();
        }

        #region methods
        void Loading()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            dtgvCategory.DataSource = categoryList;
            dtgvTable.DataSource = tableList;
            dtgvTypeAccount.DataSource = typeAccountList;
            
            LoadDateTimePikerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

            LoadListFood();
            AddFoodBinding();

            LoadListFoodCategory();
            AddCategoryBinding();

            LoadListTable();
            AddTableBinding();

            LoadCategoryIntoCombobox(cbFoodCategory);
            LoadTypeAccountIntoCombobox(cbTypeAccount);


            LoadAccount();
            AddAccountBinding();

            LoadTypeAccount();
            AddTypeAccountBinding();
        }

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }

        List<Category> SearchFoodCategoryByName(string name)
        {
            List<Category> listCategory = CategoryDAO.Instance.SearchFoodCategoryByName(name);

            return listCategory;
        }

        List<Table> SearchTableByNameByStatus(string name)
        {
            List<Table> listTable = TableDAO.Instance.SearchTableByName(name);

            return listTable;
        }

        void LoadDateTimePikerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
            dtgvFood.Columns["Id"].HeaderText = "Mã sản phẩm";
            dtgvFood.Columns["Name"].HeaderText = "Tên sản phẩm";
            dtgvFood.Columns["Name1"].HeaderText = "Mã danh mục";
            dtgvFood.Columns["Price"].HeaderText = "Giá sản phẩm";
        }

        void AddFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void LoadListFoodCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
            dtgvCategory.Columns["Id"].HeaderText = "Mã danh mục";
            dtgvCategory.Columns["Name"].HeaderText = "Tên danh mục";
        }

        void AddCategoryBinding()
        {
            txbCategory.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbNameCategory.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        void LoadListTable()
        {
            tableList.DataSource = TableDAO.Instance.LoadTableList();
            dtgvTable.Columns["Id"].HeaderText = "Mã bàn";
            dtgvTable.Columns["Name"].HeaderText = "Tên bàn";
            dtgvTable.Columns["Status"].HeaderText = "Trạng thái";
        }

        void AddTableBinding()
        {
            txbIDTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbStatusTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }

        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
            dtgvAccount.Columns["UserName"].HeaderText = "Tên đăng nhập";
            dtgvAccount.Columns["DisplayName"].HeaderText = "Tên hiển thị";
            dtgvAccount.Columns["Name"].HeaderText = "Chức vụ";
        }

        void LoadTypeAccount()
        {
            typeAccountList.DataSource = TypeAccountDAO.Instance.GetListTypeAccount();
            dtgvTypeAccount.Columns["ID"].HeaderText = "Mã chức vụ";
            dtgvTypeAccount.Columns["Name"].HeaderText = "Tên chức vụ";
        }

        void AddTypeAccountBinding()
        {
            txbIDTypeAccount.DataBindings.Add(new Binding("Text", dtgvTypeAccount.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbNameTypeAccount.DataBindings.Add(new Binding("Text", dtgvTypeAccount.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        void LoadTypeAccountIntoCombobox(ComboBox cb)
        {
            cb.DataSource = TypeAccountDAO.Instance.GetListTypeAccount();
            cb.DisplayMember = "Name";
        }

        void AddAccount(string userName, string displayName, int type)
        {
            if(AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công.");
            }
            else
            {
                MessageBox.Show("Thêm mới tài khoản thất bại.");
            }
            LoadAccount();
        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.EditAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công.");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại.");
            }
            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if(loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Không thể xoá tài khoản đang đăng nhập này được...");
                return;
            }    
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xoá tài khoản thành công.");
            }
            else
            {
                MessageBox.Show("Xoá tài khoản thất bại.");
            }
            LoadAccount();
        }

        void ResetPassWord(string userName)
        {
            if (AccountDAO.Instance.ResetPassWord(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công.");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại.");
            }
        }
        #endregion


        //Sử lý sự kiện khi click
        #region events 

        //xem hoá đơn (Bill)
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }


        //xem thêm sửa xoá tìm kiếm Food
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;

                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch
            {

            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm mới đồ uống thành công.");
                LoadListFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi thêm mới đồ uống !!!");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.EditFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa đồ uống thành công.");
                LoadListFood();
                if (editFood != null)
                {
                    editFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi sửa đồ uống !!!");
            }
        }

        private void btnDelêtFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xoá đồ uống thành công.");
                LoadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi xoá đồ uống !!!");
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler editFood;
        public event EventHandler EditFood
        {
            add { editFood += value; }
            remove { editFood -= value; }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }


        //xem thêm sửa xoá tìm kiếm CategoryFood
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListFoodCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbNameCategory.Text;

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm mới danh mục đồ uống thành công.");
                LoadListFoodCategory();
                if (insertCategory != null)
                {
                    insertCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi thêm mới danh mục đồ uống !!!");
            }
        }
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategory.Text);
            string name = txbNameCategory.Text;

            if (CategoryDAO.Instance.EditFoodCategory(id, name))
            {
                MessageBox.Show("Sửa danh mục đồ uống thành công.");
                LoadListFoodCategory();
                if (editCategory != null)
                {
                    editCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi sửa danh mục đồ uống !!!");
            }
        }
        private event EventHandler editCategory;
        public event EventHandler EditCategory
        {
            add { editCategory += value; }
            remove { editCategory -= value; }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategory.Text);

            if (CategoryDAO.Instance.DeleteFoodCategory(id))
            {
                MessageBox.Show("Xoá danh mục đồ uống thành công.");
                LoadListFoodCategory();
                if (deleteFoodCategory != null)
                {
                    deleteFoodCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi xoá danh mục đồ uống!!!");
            }
        }
        private event EventHandler deleteFoodCategory;
        public event EventHandler DeleteFoodCategory
        {
            add { deleteFoodCategory += value; }
            remove { deleteFoodCategory -= value; }
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            categoryList.DataSource = SearchFoodCategoryByName(txbSearchCategory.Text);
        }


        //xem thêm sửa xoá tìm kiếm Table
        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;

            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm bàn thành công.");
                LoadListTable();
                if (insertTable != null)
                {
                    insertTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi thêm bàn !!!");
            }
        }
        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDTable.Text);
            string name = txbTableName.Text;
            string status = txbStatusTable.Text;

            if (TableDAO.Instance.EditTable(id, name, status))
            {
                MessageBox.Show("Sửa bàn thành công.");
                LoadListTable();
                if (editTable != null)
                {
                    editTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi sửa bàn !!!");
            }
        }
        private event EventHandler editTable;
        public event EventHandler EditTable
        {
            add { editTable += value; }
            remove { editTable -= value; }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDTable.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xoá bàn thành công.");
                LoadListTable();
                if (deleteTable != null)
                {
                    deleteTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi bàn!!!");
            }
        }
        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }

        private void btnSearchTable_Click(object sender, EventArgs e)
        {
            tableList.DataSource = SearchTableByNameByStatus(txbSearchTable.Text);
        }


        //xem thêm sửa xoá Account
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (cbTypeAccount.SelectedItem as TypeAccount).ID;

            AddAccount(userName, displayName, type);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (cbTypeAccount.SelectedItem as TypeAccount).ID;

            EditAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            ResetPassWord(userName);
        }



        //Xem thêm sửa xoá TypeAccount
        private void btnShowTypeAccount_Click(object sender, EventArgs e)
        {
            LoadTypeAccount();
        }

        private void txbUserName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvTypeAccount.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvTypeAccount.SelectedCells[0].OwningRow.Cells["Type"].Value;

                    TypeAccount typeAccount = TypeAccountDAO.Instance.GetTypeAccountByID(id);

                    cbTypeAccount.SelectedItem = typeAccount;

                    int index = -1;
                    int i = 0;

                    foreach (TypeAccount item in cbTypeAccount.Items)
                    {
                        if (item.ID == typeAccount.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbTypeAccount.SelectedIndex = index;
                }
            }
            catch
            {

            }
        }

        private void btnAddTypeAccount_Click(object sender, EventArgs e)
        {
            string name = txbNameTypeAccount.Text;

            if (TypeAccountDAO.Instance.InsertTypeAccount(name))
            {
                MessageBox.Show("Thêm mới chức vụ thành công.");
                LoadTypeAccount();
                if (insertTypeAccount != null)
                {
                    insertTypeAccount(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi thêm mới chức vụ !!!");
            }
        }
        private event EventHandler insertTypeAccount;
        public event EventHandler InsertTypeAccount
        {
            add { insertTypeAccount += value; }
            remove { insertTypeAccount -= value; }
        }

        private void btnEditTypeAccount_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDTypeAccount.Text);
            string name = txbNameTypeAccount.Text;

            if (TypeAccountDAO.Instance.EditTypeAccount(id, name))
            {
                MessageBox.Show("Sửa chức vụ thành công.");
                LoadTypeAccount();
                if (editTypeAccount != null)
                {
                    editTypeAccount(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi sảy ra khi sửa chức vụ !!!");
            }
        }
        private event EventHandler editTypeAccount;
        public event EventHandler EditTypeAccount
        {
            add { editTypeAccount += value; }
            remove { editTypeAccount -= value; }
        }

        private void btnDeleteTypeAccount_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDTypeAccount.Text);

            if (TypeAccountDAO.Instance.DeleteTypeAccount(id))
            {
                MessageBox.Show("Xoá chức vụ thành công.");
                LoadTypeAccount();
                if (deleteTypeAccount != null)
                {
                    deleteTypeAccount(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng xoá tài khoản đang chứa chức vụ rồi hãy xoá chức vụ!!!");
            }
        }
        private event EventHandler deleteTypeAccount;
        public event EventHandler DeleteTypeAccount
        {
            add { deleteTypeAccount += value; }
            remove { deleteTypeAccount -= value; }
        }
        #endregion
    }
}
