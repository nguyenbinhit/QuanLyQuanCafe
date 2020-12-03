﻿using QuanLyQuanCafe.DTO; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set => instance = value; 
        }

        private AccountDAO() {}

        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {userName, passWord});

            return result.Rows.Count > 0;
        }

        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_UpdateAccount @userName , @displayName , @password , @newPassword ", new object[] { userName, displayName, pass, newPass });

            return result > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Account WHERE UserName = '" + userName + "'");

            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Account.UserName, Account.DisplayName, TypeAccount.Name FROM dbo.Account INNER JOIN dbo.TypeAccount ON Account.Type = TypeAccount.Id");
        }

        public bool InsertAccount(string name, string displayName, int type)
        {
            string query = string.Format("INSERT dbo.Account ( UserName, DisplayName, Type ) VALUES( N'{0}', N'{1}', {2} )", name, displayName, type);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool EditAccount(string name, string displayName, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}' ", name, displayName, type);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteAccount(string name)
        {
            string query = string.Format("DELETE Account WHERE UserName = N'{0}' ", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ResetPassWord(string name)
        {
            string query = string.Format("UPDATE Account SET PassWord = N'0' WHERE UserName = N'{0}' ", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
