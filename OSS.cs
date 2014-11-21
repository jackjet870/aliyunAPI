using Aliyun.OpenServices.OpenStorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    class OSS
    {
        static OssClient ossClient;
        static String bucketName = "alicsu";

        public static void Upload()
        {
            string accessid = "c8zwVmx3VB0jpo3l";          // AccessID
            string accesskey = "BO1W6EryszsDZ2Rh8rk8PQfbt6Zxmu";     // AccessKey
            ossClient = new Aliyun.OpenServices.OpenStorageService.OssClient(accessid, accesskey); //当然这里可以封装下
            ObjectMetadata meta = new ObjectMetadata();
            meta.ContentType = "image/jpeg";
            string key = "pic/" + "aaa.jpg";
            Stream stream = new MemoryStream(File.ReadAllBytes("aaa.jpg"));
            PutObjectResult result = ossClient.PutObject(bucketName, key, stream, meta);//上传图片
            AccessControlList accs = ossClient.GetBucketAcl(bucketName);
            string imgurl = string.Empty;
            if (!accs.Grants.Any())//判断是否有读取权限
            {
                imgurl = ossClient.GeneratePresignedUri(bucketName, key, DateTime.Now.AddMinutes(5)).AbsoluteUri; //生成一个签名的Uri 有效期5分钟
            }
            else
            {

                imgurl = string.Format("http://{0}.oss.aliyuncs.com/{1}", bucketName, key);
            } 
        }
    }
}
