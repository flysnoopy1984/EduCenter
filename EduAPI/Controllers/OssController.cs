using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliyun.OSS;
using Aliyun.OSS.Common;
using EduCenterModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OssController : ControllerBase
    {
        string endpoint = "http://oss-cn-shanghai.aliyuncs.com";
        //"http://oss.iqianba.cn";
        string accessKeyId = "LTAI4Fqe1MbLtXgmNA3Sxoaa";
        string accessKeySecret = "YKedXRPAN7LhLnZ0VPbQtkVynxOjxP";
        string bucketName = "iqianba";

        public OssController()
        {

        }
        [HttpGet]
        public ResultNormal TestGetObjUrl()
        {
            ResultNormal r = new ResultNormal();
            var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            var uri = client.GeneratePresignedUri(bucketName, "CG0A1838.JPG");
            r.SuccessMsg = uri.ToString();
            return r;
        //    var obj = client.GetObject(bucketName, "CG0A1838.JPG");
            
        } 
        [HttpGet]
        public ResultList<string> TestGetImage()
        {
          
            ResultList<string> result = new ResultList<string>();
            result.List = new List<string>();
            try
            {
                var conf = new ClientConfiguration();
              
                conf.IsCname = true;

                var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
                var obj = client.GetObject(bucketName, "CG0A1838.JPG");
                using (var requestStream = obj.Content)
                {
                    byte[] buf = new byte[1024];
                    var fs = System.IO.File.Open("d:\\1.jpg", FileMode.OpenOrCreate);
                    var len = 0;
                    // 通过输入流将文件的内容读取到文件或者内存中。
                    while ((len = requestStream.Read(buf, 0, 1024)) != 0)
                    {
                        fs.Write(buf, 0, len);
                    }
                    fs.Close();
                }
                Console.WriteLine("Get object succeeded");
             
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return result; ;
        }

      

      
    }
}