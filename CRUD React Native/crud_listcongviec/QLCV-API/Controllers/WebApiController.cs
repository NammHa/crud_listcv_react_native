using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QLCV_API.Models;

namespace QLCV_API.Controllers
{
    public class WebApiController : ApiController
    {
        public QLCVEntities DatabaseObj = new QLCVEntities();        
        public string convertToJSON(Object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public JObject returnException(Exception ex)
        {
            return JObject.Parse("{ \"STATUS\":\"EXCEPTION\", \"MESSAGE\":\"" + ex.Message + "\", \"INNER EXCEPTION\":\"" +ex.InnerException+ "\"}");
        }
        public JObject returnError(string message)
        {
            return JObject.Parse("{ \"STATUS\":\"ERROR\", \"MESSAGE\":\"" + message + "\"}");
        }
        public JObject returnOK(Object obj)
        {
            string sObj = convertToJSON(obj);
            return JObject.Parse("{ \"STATUS\":\"OK\", \"DATA\":" + sObj + "}");
        }
        public JObject returnAdded()
        {
            return JObject.Parse("{ \"STATUS\":\"OK\", \"MESSAGE\":\"Thêm dữ liệu thành công\"}");
        }
        public JObject returnEdited()
        {
            return JObject.Parse("{ \"STATUS\":\"OK\", \"MESSAGE\":\"Sửa dữ liệu thành công\"}");
        }
        public JObject returnDeleted()
        {
            return JObject.Parse("{ \"STATUS\":\"OK\", \"MESSAGE\":\"Xóa dữ liệu thành công\"}");
        }
        public JObject returnNotExist()
        {
            return JObject.Parse("{ \"STATUS\":\"NODATA\", \"MESSAGE\":\"Không tồn tại dữ liệu\"}");
        }
        public JObject createObjectMessage(string message)
        {
            return JObject.Parse("{\"MESSAGE\":\"" + message + "\"}");
        }
        public string CheckValidationErrors(QLCVEntities ett)
        {
            string errors = string.Empty;
            var validationErrors = ett.GetValidationErrors()
                .Where(vr => !vr.IsValid)
                .SelectMany(vr => vr.ValidationErrors);

            foreach (var error in validationErrors)
            {
                errors += error.ErrorMessage + "\n";
            }

            return errors;
        }
        public string getValueJObject(string key, JObject jObject)
        {
            if (jObject[key] != null)
                return jObject[key].ToString();
            else
                return null;
        }
        


    }
}
