using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aliyun.Api;
using Aliyun.Api.ECS.ECS20140526.Request;
using Aliyun.Api.ECS.ECS20140526.Response;
using System.Net;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {

        private static string serverUrl = "http://ecs.aliyuncs.com/";
        private static string accessKeyId = "c8zwVmx3VB0jpo3l";
        private static string accessKeySecret = "BO1W6EryszsDZ2Rh8rk8PQfbt6Zxmu";

        private static IAliyunClient client = new DefaultAliyunClient(serverUrl, accessKeyId, accessKeySecret);

        static void Main(string[] args)
        {
            //CreateInstance();
            //DescribeInstanceAttribute("i-23ywunfke");
            //StopInstance("i-23ywunfke");
            //StartInstance("i-23ywunfke");


            //TestClass obj = new TestClass() { A = "aa", B = 23.2, C = DateTime.Now };
            //MemCached.getInstance().Store(Enyim.Caching.Memcached.StoreMode.Set, "dasdasdasd", obj);
            //TestClass value = MemCached.getInstance().Get("dasdasdasd") as TestClass;
            //Console.Write(value.A + "  " + value.B + "  " + value.C);

            //OSS
            OSS.Upload();

            //RDS API
            //DescribeDBInstances();
            //DescribeDatabases();
            Console.ReadKey();
        }

        /// <summary>
        /// 查看数据库实例列表
        /// </summary>
        private static void DescribeDBInstances()
        {
            String resp = new RdsRequest("beRXiSgcylsRxrka", "GScTeWEj0mvm3HeTStVEhMCviyqw3l").Execute("DescribeDBInstances",
                                                  new Dictionary<String, String>()
                                                  {
                                                      {"DBInstanceId","rdsr3ayjqem7vae"}
                                                      //{ "RegionId", "cn-hangzhou" },
                                                      //{ "Engine", "MySQL" },
                                                      //{ "DBInstanceNetType", "0" }
                                                  });

            Console.WriteLine(resp);
        }

        /// <summary>
        /// 查看数据库列表
        /// </summary>
        private static void DescribeDatabases()
        {
            String resp = new RdsRequest("beRXiSgcylsRxrka", "GScTeWEj0mvm3HeTStVEhMCviyqw3l").Execute("DescribeDatabases",
                                                  new Dictionary<String, String>()
                                                  {
                                                      {"DBInstanceId","rdsr3ayjqem7vae"}
                                                      //{ "DBName", "newgrand" },
                                                      //{ "DBStatus", "Running" }
                                                  });

            Console.WriteLine(resp);
        }


        /// <summary>
        /// 创建实例
        /// </summary>
        public static void CreateInstance()
        {
            CreateInstanceRequest createInstanceRequest = new CreateInstanceRequest();
            createInstanceRequest.RegionId = "cn-hangzhou";
            createInstanceRequest.ImageId = "_32_23c472_20120822172155_aliguest.vhd";
            createInstanceRequest.InstanceType = "ecs.t1.small";
            createInstanceRequest.SecurityGroupId = "sg-c0003e8b9";

            try
            {
                CreateInstanceResponse createInstanceResponse = client.Execute(createInstanceRequest);
                if (string.IsNullOrEmpty(createInstanceResponse.Code))
                {//创建成功
                    String instanceId = createInstanceResponse.InstanceId;//取得实例ID
                }
                else
                {//创建失败
                    String errorCode = createInstanceResponse.Code;//取得错误码
                    String message = createInstanceResponse.Message;//取得错误信息
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception
            }

        }

        public static void DescribeInstanceAttribute(String instanceId)
        {
            DescribeInstanceAttributeRequest describeInstanceAttributeRequest = new DescribeInstanceAttributeRequest();
            describeInstanceAttributeRequest.InstanceId = instanceId;
            try
            {

                DescribeInstanceAttributeResponse describeInstanceAttributeResponse = client.Execute(describeInstanceAttributeRequest);
                if (string.IsNullOrEmpty(describeInstanceAttributeResponse.Code))
                {
                    //查询成功
                }
                else
                { 
                    String errorCode = describeInstanceAttributeResponse.Code;//取得错误码
                    String message = describeInstanceAttributeResponse.Message;//取得错误信息
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception
            }
        }

        /// <summary>
        /// 删除实例
        /// </summary>
        /// <param name="instanceId"></param>
        public static void DeleteInstance(String instanceId)
        {
            DeleteInstanceRequest deleteInstanceRequest = new DeleteInstanceRequest();
            deleteInstanceRequest.InstanceId = instanceId;
            try
            {
                DeleteInstanceResponse deleteInstanceResponse = client.Execute(deleteInstanceRequest);
                if (string.IsNullOrEmpty(deleteInstanceResponse.Code))
                {//删除成功

                }
                else
                {//删除失败
                    String errorCode = deleteInstanceResponse.Code;//取得错误码
                    String message = deleteInstanceResponse.Message;//取得错误信息
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception
            }
        }

        /// <summary>
        /// 停止实例
        /// </summary>
        /// <param name="instanceId"></param>
        public static void StopInstance(string instanceId)
        {
            StopInstanceRequest stopInstanceRequest = new StopInstanceRequest();
            stopInstanceRequest.InstanceId = instanceId;


            StopInstanceResponse stopInstanceResponse = client.Execute(stopInstanceRequest);
            if (string.IsNullOrEmpty(stopInstanceResponse.Code))
            {//查询成功
                //查看实例信息相关代码
                //......
            }
            else
            { 
                String errorCode = stopInstanceResponse.Code;//取得错误码
                String message = stopInstanceResponse.Message;//取得错误信息
            }
        }

        /// <summary>
        /// 启动实例
        /// </summary>
        /// <param name="instanceId"></param>
        public static void StartInstance(string instanceId)
        {
            StartInstanceRequest startInstanceRequest = new StartInstanceRequest();
            startInstanceRequest.InstanceId = instanceId;

            StartInstanceResponse response = client.Execute(startInstanceRequest);
            if (string.IsNullOrEmpty(response.Code))
            {
 
            }
            else
            { 
                String errorCode = response.Code;//取得错误码
                String message = response.Message;//取得错误信息
            }
        }

        public static string IsHoliday(string date)
        {
            string url = @"http://www.easybots.cn/api/holiday.php?d=";
            url = url + date;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Timeout = 2000;
            httpRequest.Method = "GET";
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            StreamReader sr = new StreamReader(httpResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("gb2312"));
            string result = sr.ReadToEnd();
            result = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            int status = (int)httpResponse.StatusCode;
            sr.Close();
            return result;
        }

    }
}
