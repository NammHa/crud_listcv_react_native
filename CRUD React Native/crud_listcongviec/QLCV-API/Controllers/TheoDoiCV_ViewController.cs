using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLCV_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace QLCV_API.Controllers
{
    public class TheoDoiCV_ViewController : WebApiController
    {

        //GET LIST
        
        [HttpGet]
        [Route("api/GetListCV")]
        public JObject getListCV()
        {
            try
            {
                DatabaseObj.Configuration.ProxyCreationEnabled = false;
                List<TBL_THEO_DOI_CV> lcongViec = DatabaseObj.TBL_THEO_DOI_CV.ToList();
                List<TBL_HE_THONG> lheThong = DatabaseObj.TBL_HE_THONG.AsNoTracking().ToList();
                List<TBL_HOP_DONG> lhopDong = DatabaseObj.TBL_HOP_DONG.AsNoTracking().ToList();
                List<TBL_NGUOI_DUNG> lnguoiDung = DatabaseObj.TBL_NGUOI_DUNG.AsNoTracking().ToList();
                var query = from cv in lcongViec
                            where cv.TT_XOA == false
                            join hd in lhopDong on cv.ID_HOP_DONG equals hd.ID into table1
                            from hd in table1.DefaultIfEmpty()
                            join ht in lheThong on cv.ID_HE_THONG equals ht.ID into table2
                            from ht in table2.DefaultIfEmpty()
                            join nd in lnguoiDung on cv.ID_NGUOI_CHU_TRI equals nd.Id into table3
                            from nd in table3.DefaultIfEmpty()
                            orderby cv.NGAY_BAT_DAU descending
                            select new
                            {
                                cv.ID,
                                cv.TEN_CONG_VIEC,
                                cv.ID_HE_THONG,
                                TEN = ht?.TEN ?? String.Empty,
                                cv.ID_HOP_DONG,
                                MA_HOP_DONG = hd?.MA_HOP_DONG ?? string.Empty,
                                cv.ID_NGUOI_CHU_TRI,
                                FullName = nd?.FullName ?? string.Empty,
                                cv.ID_NGUOI_PHOI_HOP,
                                cv.NGAY_BAT_DAU,
                                cv.NGAY_KET_THUC,
                                cv.ID_KET_QUA_CV,
                                cv.GHI_CHU,
                                cv.ID_NGUOI_TAO,
                                cv.NGAY_TAO,
                                cv.ID_NGUOI_SUA,
                                cv.NGAY_SUA,
                                cv.TT_XOA
                            };
                return returnOK(query);
            }
            catch(Exception ex)
            {
                return returnException(ex);
            }

        }


        //GET BY ID
        
        [HttpPost]
        [Route("api/GetCVById")]
        public JObject getByID([FromBody]JObject id)
        {
            try 
            {
                if(id["ID"] == null)
                    return returnNotExist();
                decimal cvID = decimal.Parse(id["ID"].ToString());
                List<TBL_THEO_DOI_CV> lcongViec = DatabaseObj.TBL_THEO_DOI_CV.ToList();
                List<TBL_HE_THONG> lheThong = DatabaseObj.TBL_HE_THONG.AsNoTracking().ToList();
                List<TBL_HOP_DONG> lhopDong = DatabaseObj.TBL_HOP_DONG.AsNoTracking().ToList();
                List<TBL_NGUOI_DUNG> lnguoiDung = DatabaseObj.TBL_NGUOI_DUNG.AsNoTracking().ToList();
                var query = from cv in lcongViec
                            where (cv.TT_XOA == false && cv.ID == cvID)
                            join hd in lhopDong on cv.ID_HOP_DONG equals hd.ID into table1
                            from hd in table1.DefaultIfEmpty()
                            join ht in lheThong on cv.ID_HE_THONG equals ht.ID into table2
                            from ht in table2.DefaultIfEmpty()
                            join nd in lnguoiDung on cv.ID_NGUOI_CHU_TRI equals nd.Id into table3
                            from nd in table3.DefaultIfEmpty()
                            orderby cv.NGAY_BAT_DAU descending
                            select new
                            {
                                cv.ID,
                                cv.TEN_CONG_VIEC,
                                cv.ID_HE_THONG,
                                TEN = ht?.TEN ?? String.Empty,
                                cv.ID_HOP_DONG,
                                MA_HOP_DONG = hd?.MA_HOP_DONG ?? string.Empty,
                                cv.ID_NGUOI_CHU_TRI,
                                FullName = nd?.FullName ?? string.Empty,
                                cv.ID_NGUOI_PHOI_HOP,
                                cv.NGAY_BAT_DAU,
                                cv.NGAY_KET_THUC,
                                cv.ID_KET_QUA_CV,
                                cv.GHI_CHU,
                                cv.ID_NGUOI_TAO,
                                cv.NGAY_TAO,
                                cv.ID_NGUOI_SUA,
                                cv.NGAY_SUA,
                                cv.TT_XOA
                            };
                if(!query.Any())
                    return returnNotExist();
                return returnOK(query);

            }
            catch (Exception ex)
            {
                return returnException(ex);
            }
        }



        //ADD New
        
        [HttpPost]
        [Route("api/AddNewCV")]
        public JObject addNewCV([FromBody]JObject value)
        {
            try
            {
                TBL_THEO_DOI_CV addObj = new TBL_THEO_DOI_CV();
                addObj.TEN_CONG_VIEC = value["TEN_CONG_VIEC"].ToString();
                addObj.ID_HE_THONG = int.Parse(value["ID_HE_THONG"].ToString());
                addObj.ID_HOP_DONG = int.Parse(value["ID_HOP_DONG"].ToString());
                addObj.ID_NGUOI_CHU_TRI = int.Parse(value["ID_NGUOI_CHU_TRI"].ToString());
                addObj.NGAY_BAT_DAU = DateTime.Parse(value["NGAY_BAT_DAU"].ToString());
                addObj.NGAY_KET_THUC = DateTime.Parse(value["NGAY_KET_THUC"].ToString());
                addObj.ID_KET_QUA_CV = int.Parse(value["ID_KET_QUA_CV"].ToString());
                addObj.ID_NGUOI_TAO = int.Parse(value["ID_NGUOI_TAO"].ToString());
                addObj.NGAY_TAO = DateTime.Now;
                addObj.ID_NGUOI_SUA = int.Parse(value["ID_NGUOI_TAO"].ToString());
                addObj.NGAY_SUA = DateTime.Now;
                addObj.TT_XOA = false;
                //addObj.ID_MUC_DO_CV = int.Parse(value["ID_MUC_DO_CV"].ToString());
                DatabaseObj.TBL_THEO_DOI_CV.Add(addObj);
                string Errors = CheckValidationErrors(DatabaseObj);
                if (!string.IsNullOrEmpty(Errors))
                {
                    return returnError(Errors);
                }
                DatabaseObj.SaveChanges();
                return returnAdded();

            }
            catch(Exception ex)
            {
                return returnException(ex);
            }
            
            

        }

        //Edit
        
        [HttpPost]
        [Route("api/EditCV")]
        public JObject EditCV([FromBody]JObject value)
        {
            try
            {
                if(value["ID"]==null)
                    return returnNotExist();
                int CVid = int.Parse(value["ID"].ToString());
                TBL_THEO_DOI_CV editObj = DatabaseObj.TBL_THEO_DOI_CV.Where(x => x.ID.Equals(CVid)).FirstOrDefault();
                if (editObj == null)
                    return returnNotExist();
                editObj.TEN_CONG_VIEC = value["TEN_CONG_VIEC"].ToString();
                editObj.ID_HE_THONG = int.Parse(value["ID_HE_THONG"].ToString());
                editObj.ID_HOP_DONG = int.Parse(value["ID_HOP_DONG"].ToString());
                editObj.ID_NGUOI_CHU_TRI = int.Parse(value["ID_NGUOI_CHU_TRI"].ToString());
                editObj.NGAY_BAT_DAU = DateTime.Parse(value["NGAY_BAT_DAU"].ToString());
                editObj.NGAY_KET_THUC = DateTime.Parse(value["NGAY_KET_THUC"].ToString());
                editObj.ID_KET_QUA_CV = int.Parse(value["ID_KET_QUA_CV"].ToString());
                editObj.ID_NGUOI_SUA = int.Parse(value["ID_NGUOI_SUA"].ToString());
                editObj.GHI_CHU = value["GHI_CHU"].ToString();
                editObj.NGAY_SUA = DateTime.Now;
                string Errors = CheckValidationErrors(DatabaseObj);
                if (!string.IsNullOrEmpty(Errors))
                {
                    return returnError(Errors);
                }
                DatabaseObj.SaveChanges();
                return returnEdited();

            }
            catch(Exception ex)
            {
                return returnException(ex);
            }
        }


        //Delete
        
        [HttpPost]
        [Route("api/DeleteCV")]
        public JObject DeleteCV([FromBody] JObject value)
        {
            try
            {
                if (value["ID"] == null)
                    return returnNotExist();
                int CVid = int.Parse(value["ID"].ToString());
                TBL_THEO_DOI_CV delObj = DatabaseObj.TBL_THEO_DOI_CV.Where(x => x.ID.Equals(CVid)).FirstOrDefault();
                if (delObj == null)
                    return returnNotExist();
                //delObj.ID_NGUOI_SUA = int.Parse(value["ID_NGUOI_SUA"].ToString());
                //delObj.NGAY_SUA = DateTime.Now;
                delObj.TT_XOA = true;
                string Errors = CheckValidationErrors(DatabaseObj);
                if (!string.IsNullOrEmpty(Errors))
                {
                    return returnError(Errors);
                }
                DatabaseObj.SaveChanges();
                return returnDeleted();

            }
            catch (Exception ex)
            {
                return returnException(ex);
            }
        }

    }
}
