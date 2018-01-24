using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWeb.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            string search = Request["input"].ToString();
            string Strcn = "Data Source=172.20.10.2;Initial Catalog=ShoppingMall;User ID=sa;Password=sa";
            string sql = "SELECT *  FROM[ShoppingMall].[dbo].[Product]";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Strcn))
            {
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(comm))
                    {
                        conn.Open();
                        adapter.Fill(dt);
                    }
                }
            }

            
            var r=dt.Rows.Cast<DataRow>().Where(z => z["Name"].ToString().IndexOf(search) >-1).CopyToDataTable();

            string result = JsonConvert.SerializeObject(r);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}