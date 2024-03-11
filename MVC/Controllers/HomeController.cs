using MVC.Models;
using MVC.Models.ViewModel;
using MVC.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                using (AccountDataService ds = new AccountDataService())
                {
                    ViewBag.AccountTable = await ds.GetAccountTable();
                }
            }
            catch
            {
                //log
            }
            return View();
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="accountView">前端輸入帳號資訊</param>
        /// <returns></returns>
        public async Task<JObject> AddOrUpdateAccount(AccountViewModel accountView)
        {
            try
            {
                JObject res = new JObject();
                if (ModelState.IsValid)
                {
                    using (AccountDataService ds = new AccountDataService())
                    {
                        Account account = new Account()
                        {
                            Name = accountView.Name,
                            Age = accountView.Age,
                            Birthday = accountView.Birthday
                        };
                        if (accountView.SN != null) account.SN = (int)accountView.SN;
                        if (await ds.CreateAccount(account))
                        {
                            res.Add("Message", "Success");
                            return res;
                        }
                        else
                        {
                            res.Add("Message", "Unexpected error");
                            return res;
                        }
                    }
                }
                else
                {
                    string message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).First();
                    res.Add("Message", message);
                    return res;
                }
            }
            catch
            {
                //log
                return new JObject() { "Message", "Unexpected error" };
            }
        }

        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <param name="sn">帳號編號</param>
        /// <returns></returns>
        public async Task<JObject> DeleteAccount(int sn)
        {
            try
            {
                JObject res = new JObject();
                using (AccountDataService ds = new AccountDataService())
                {
                    if (await ds.DeleteAccount(sn))
                    {
                        res.Add("Message", "Success");
                        return res;
                    }
                    else
                    {
                        res.Add("Message", "Unexpected error");
                        return res;
                    }
                }
            }
            catch
            {
                //log
                return new JObject() { "Message", "Unexpected error" };
            }
        }
    }
}