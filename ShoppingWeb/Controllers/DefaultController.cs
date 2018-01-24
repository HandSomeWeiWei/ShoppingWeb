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
            string Strcn = "Data Source=.;Initial Catalog=ShoppingMall;User ID=sa;Password=sa";
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

            string result = JsonConvert.SerializeObject(dt);


            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}