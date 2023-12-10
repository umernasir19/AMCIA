using FL_ACME.Models.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace FL_ACME.Utilities
{
    public static class Utility
    {
        
            private static string _BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        private static string _uplaodlocation = ConfigurationManager.AppSettings["FileUpload"];
        public static T InteractToAPI<T>(string apiUri, bool Post = false, object formBody = null)
            {

                var client = new RestClient(_BaseUrl + apiUri);
                RestRequest request = null;
                if (Post == false)
                    request = new RestRequest(Method.GET);
                else
                    request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");
                request.Timeout = 5 * 60 * 1000;
                if (formBody != null)
                {
                    var json = JsonConvert.SerializeObject(formBody);
                    request.AddParameter("application/json", json, ParameterType.RequestBody);
                }

                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<T>(response.Content);
            
        }

        public static ResponseClass SaveFiles(HttpPostedFileBase objfiles)
        {
            ResponseClass objresponseclass = new ResponseClass();
            if (objfiles != null)
            {
                try
                {
                    if (Directory.Exists(HttpContext.Current.Server.MapPath( _uplaodlocation)))
                    {
                        string filename= System.IO.Path.GetFileName(System.IO.Path.GetRandomFileName() + objfiles.FileName);
                        
                        objfiles.SaveAs(HttpContext.Current.Server.MapPath(_uplaodlocation + filename));
                        string filepath = _uplaodlocation + filename;
                        objresponseclass.Status = true;
                        objresponseclass.Message = filepath;
                        return objresponseclass;
                    }
                    else
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(_uplaodlocation));
                        string filename = System.IO.Path.GetFileName(System.IO.Path.GetRandomFileName() + objfiles.FileName);

                        objfiles.SaveAs(HttpContext.Current.Server.MapPath(_uplaodlocation + filename));
                        string filepath = _uplaodlocation + filename;
                        objresponseclass.Status = true;
                        objresponseclass.Message = filepath;
                        return objresponseclass;

                    }

                }
                catch (Exception ex)
                {
                    objresponseclass.Status = false;
                    objresponseclass.Message = ex.Message;
                    return objresponseclass;
                }
            }
            else
            {
                objresponseclass.Status = false;
                objresponseclass.Message = "File Not found";
                return objresponseclass;
            }
        }
    }
}