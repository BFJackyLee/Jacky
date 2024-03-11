using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.Model;

namespace WebForm
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var db = new HankDBEntities())
                {
                    List<Account> accountList = db.Account.ToList();
                    if (accountList.Count == 0)
                    {
                        DataGrid.Visible = false;
                    }
                    else
                    {
                        DataGrid.DataSource = accountList;
                        DataGrid.DataBind();
                    }
                }
            }
        }

        protected void AddOrUpdate_Click(object sender, EventArgs e)
        {
            Account account = new Account();
            if (!string.IsNullOrEmpty(SN.Value) && int.TryParse(SN.Value, out int sn)) account.SN = sn;
            if (!string.IsNullOrEmpty(Name.Value)) account.Name = Name.Value;
            else return;
            if (!string.IsNullOrEmpty(Age.Value) && int.TryParse(Age.Value, out int age)) account.Age = age;
            else return;
            if (!string.IsNullOrEmpty(Birthday.Value) && DateTime.TryParse(Birthday.Value, out DateTime birthday)) account.Birthday = birthday;
            else return;
            using (var db = new HankDBEntities())
            {
                db.Account.AddOrUpdate(account);
                db.SaveChanges();
            }
            Response.Redirect(Request.Url.AbsoluteUri, true);
        }

        protected void DataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (int.TryParse(e.Item.Cells[0].Text, out int sn))
            {
                using (var db = new HankDBEntities())
                {
                    Account account = db.Account.Find(sn);
                    switch (e.CommandName)
                    {
                        case "Edit":
                            SN.Value = account.SN.ToString();
                            Name.Value = account.Name.ToString();
                            Age.Value = account.Age.ToString();
                            Birthday.Value = account.Birthday.ToString("yyyy-MM-dd");
                            AddOrUpdate.Text = "修改帳號";
                            break;
                        case "Delete":
                            db.Account.Remove(account);
                            db.SaveChanges();
                            Response.Redirect(Request.Url.AbsoluteUri, true);
                            break;
                    }
                }

            }
        }
    }
}