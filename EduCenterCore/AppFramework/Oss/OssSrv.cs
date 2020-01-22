using Aliyun.OSS;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EduCenterCore.AppFramework.Oss
{
    public class OssSrv
    {
        string _endpoint = "http://oss-cn-shanghai.aliyuncs.com";
        //"http://oss.iqianba.cn";
        string _accessKeyId = "LTAI4Fqe1MbLtXgmNA3Sxoaa";
        string _accessKeySecret = "YKedXRPAN7LhLnZ0VPbQtkVynxOjxP";
        string _bucketPrivate = "iqianba";
        string _bucketPublic = "iqianba-public";
        string _publicHost = "iqianba-public.oss-cn-shanghai.aliyuncs.com/";




        public string UploadRequestFile(IFormFile file,string path = "",string fileName="",bool isPrivate= false)
        {
            try
            {
                if(string.IsNullOrEmpty(fileName))
                    fileName = file.FileName;
                var client = new OssClient(_endpoint, _accessKeyId, _accessKeySecret);
                var objName = string.IsNullOrEmpty(path) ? fileName : path + "/" + fileName;
                var buckName = isPrivate ? _bucketPrivate : _bucketPublic;

                using (Stream stream = file.OpenReadStream())
                {
                    
                    client.PutObject(buckName, objName , stream);
                }
                return GetFileUrl(objName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFileUrl(string fileName,int expiredMins = 60, bool isPrivate = false)
        {
            if(isPrivate)
            {
                DateTime expired = DateTime.Now.AddHours(expiredMins * 60);

                var client = new OssClient(_endpoint, _accessKeyId, _accessKeySecret);
                var buckName = isPrivate ? _bucketPrivate : _bucketPublic;

                //  var req = new GeneratePresignedUriRequest(buckName, fileName, SignHttpMethod.Get);
                Uri uri = client.GeneratePresignedUri(buckName, fileName);

                return uri.ToString();
            }
            else
            {
                return "http://" + _publicHost + fileName;
            }
          

        }
    }
}
