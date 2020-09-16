using Newtonsoft.Json.Linq;
using QLCV_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace QLCV_API.Controllers
{
    public class NguoiDungController : WebApiController
    {

        //[Authorize]
        [HttpGet]
        [Route("api/GetListNguoiDung")]
        public JObject getListND()
        {
            try
            {
                DatabaseObj.Configuration.ProxyCreationEnabled = false;
                List<TBL_NGUOI_DUNG> lnguoiDung = DatabaseObj.TBL_NGUOI_DUNG.AsNoTracking().ToList();
                var querry = from nd in lnguoiDung
                             where nd.isEnabled == true
                             select new
                             {
                                 nd.Id,
                                 nd.FullName,
                                 nd.FirstName,
                                 nd.LastName,
                                 nd.Tel,
                                 nd.isAdmin,
                                 nd.Email,
                                 nd.UserName,
                                 nd.Password,
                             };
                return returnOK(querry);
            }
            catch(Exception ex)
            {
                return returnException(ex);
            }
        }
    }
}
