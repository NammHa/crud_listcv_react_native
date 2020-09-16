using Newtonsoft.Json.Linq;
using QLCV_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLCV_API.Controllers
{
    public class HT_HDController : WebApiController
    {
        //[Authorize]
        [HttpGet]
        [Route("api/GetListHT")]
        public JObject getListHT()
        {
            try
            {
                DatabaseObj.Configuration.ProxyCreationEnabled = false;
                List<TBL_HE_THONG> lhethong = DatabaseObj.TBL_HE_THONG.AsNoTracking().ToList();
                var querry = from ht in lhethong
                             where ht.TT_XOA == false
                             select new { 
                             ht.ID,
                             ht.MA_HE_THONG,
                             ht.STT,
                             ht.TT_XOA,
                             ht.ID_NGUOI_TAO,
                             ht.NGAY_TAO,
                             ht.ID_NGUOI_SUA,
                             ht.NGAY_SUA,
                             ht.ID_HOP_DONG,
                             ht.TEN,
                             ht.MO_TA,
                             ht.NGAY_KH_GUI_YC,
                             ht.NGAY_KH_YC_HOAN_THANH,
                             ht.ID_KH_KY_HD,
                             ht.ID_KH_NSD,
                             ht.DO_UU_TIEN,
                             ht.NGAY_BAT_DAU_LAM,
                             ht.NGAY_NGHIEM_THU_TT,
                             ht.QUY,
                             ht.MAN_DAY,
                             ht.ID_TRANG_THAI_DA,
                             ht.ID_PRODUCT_OWNER,
                             ht.ID_PM,
                             ht.ID_NGUOI_THUC_HIEN,
                             ht.ID_NGUOI_PHOI_HOP,
                             ht.ID_LOAI_TRIEN_KHAI_DA,
                             ht.ID_PHAM_VI_DA,
                             ht.ID_TIEN_DO,
                             ht.TT_DBHD,
                             ht.TBL_HOP_DONG
                             };
                return returnOK(querry);
            }
            catch(Exception ex)
            {
                return returnException(ex);
            }
        }

        //[Authorize]
        [HttpGet]
        [Route("api/GetListHD")]
        public JObject getListHD()
        {
            try
            {
                DatabaseObj.Configuration.ProxyCreationEnabled = false;
                List<TBL_HOP_DONG> lhopdong = DatabaseObj.TBL_HOP_DONG.AsNoTracking().ToList();
                var querry = from hd in lhopdong
                             where hd.TT_XOA == false
                             select new
                             {
                                 hd.ID,
                                 hd.MA_HOP_DONG,
                                 hd.SO_HOP_DONG,
                                 hd.TEN,
                                 hd.MO_TA,
                                 hd.STT,
                                 hd.TT_XOA,
                                 hd.ID_NGUOI_TAO,
                                 hd.NGAY_TAO,
                                 hd.ID_NGUOI_SUA,
                                 hd.NGAY_SUA,
                                 hd.TBL_HE_THONG
                             };
                return returnOK(querry);
            }
            catch (Exception ex)
            {
                return returnException(ex);
            }
        }
    }
}
