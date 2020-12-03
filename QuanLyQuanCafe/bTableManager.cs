using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = QuanLyQuanCafe.DTO.Menu;

namespace QuanLyQuanCafe
{
    public partial class bTableManager : Form
    {

        private Account loginAccount;
        public Account LoginAccount 
        { 
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public bTableManager(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;


            LoadListView();

            LoadCategory();

            LoadComboboxTable(cbSwitchTable);
        }

        #region Methor
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ") ";
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadFoodListCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }

        ImageList imgList;

        void LoadImageList()
        {
            imgList = new ImageList() { ImageSize = new Size(62, 62) };
            imgList.Images.Add(new Bitmap(Application.StartupPath + "\\Images\\bantrong.png"));
            imgList.Images.Add(new Bitmap(Application.StartupPath + "\\Images\\banconguoi.png"));
            imgList.Images.Add(new Bitmap(Application.StartupPath + "\\Images\\banhong.png"));
        }

        void LoadListView()
        {
            lsvShow.Items.Clear();

            LoadImageList();
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                lsvShow.LargeImageList = imgList;

                ListViewItem lv = new ListViewItem();                
                lv.Text = item.Name;
                lv.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        lv.ImageIndex = 0;
                        break;
                    case "Hỏng":
                        lv.ImageIndex = 2;
                        break;
                    default:
                        lv.ImageIndex = 1;
                        break;
                }

                lsvShow.Items.Add(lv);
                
            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;

            foreach(Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;

                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo cul = new CultureInfo("vi-VN");

            txbTotalPrice.Text = totalPrice.ToString("c", cul);
        }

        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }
        #endregion



        #region Events
        private void bTableManager_Load(object sender, EventArgs e)
        {

        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn mà bạn muốn.");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            if(idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }

            ShowBill(table.ID);

            LoadListView();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodListCategoryID(id); 
        }

        private void nmFoodCount_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int discount = (int)nmDiscount.Value;

            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idBill != - 1)
            {
                if(MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán hoá đơn cho {0} \n\nCộng tiền hàng: {1}đ \n\nGiảm giá: {2}% \n\nTổng tiền tất cả: {3}đ  ", table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                    ShowBill(table.ID);

                    LoadListView();
                }    
            }
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bAdmin ad = new bAdmin();
            ad.loginAccount = LoginAccount;
            ad.InsertFood += Ad_InsertFood;
            ad.DeleteFood += Ad_DeleteFood;
            ad.EditFood += Ad_EditFood;

            ad.InsertCategory += Ad_InsertCategory;
            ad.EditCategory += Ad_EditCategory;
            ad.DeleteFoodCategory += Ad_DeleteFoodCategory;

            ad.InsertTable += Ad_InsertTable;
            ad.EditTable += Ad_EditTable;
            ad.DeleteTable += Ad_Deletetable;

            ad.ShowDialog();
        }

        void Ad_DeleteFoodCategory(object sender, EventArgs e)
        {
            LoadCategory();
        }

        void Ad_EditCategory(object sender, EventArgs e)
        {
            LoadCategory();
        }

        void Ad_InsertCategory(object sender, EventArgs e)
        {
            LoadCategory();
        }

        void Ad_Deletetable(object sender, EventArgs e)
        {
            LoadListView();
        }

        void Ad_EditTable(object sender, EventArgs e)
        {
            LoadListView();
        }

        void Ad_InsertTable(object sender, EventArgs e)
        {
            LoadListView();
        }

        void Ad_EditFood(object sender, EventArgs e)
        {
            LoadFoodListCategoryID((cbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }
        }

        void Ad_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }
            LoadListView();
        }

        void Ad_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bAccountProfile ap = new bAccountProfile(LoginAccount);
            ap.UpdateAccount += Ap_UpdateAccount;
            ap.ShowDialog();
        }

        void Ap_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lsvShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvShow.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lsvShow.SelectedItems[0];
                if (selectedItem != null)
                {
                    Table tb = selectedItem.Tag as Table;
                    lsvBill.Tag = selectedItem.Tag;
                    if (tb != null)
                    {
                        int x = tb.ID;
                        ShowBill(x);
                    }
                }
            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cbSwitchTable.SelectedItem as Table).ID;

            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển {0} qua {1}", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);

                LoadListView();
            }    
        }
        #endregion
    }
}
