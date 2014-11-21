using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleApplication1
{
    /// <summary>
    /// MemCached客户端
    /// </summary>
    public sealed class MemCached
    {
        private static MemcachedClient MemClient;
        static readonly object padlock = new object();
        //线程安全的单例模式
        public static MemcachedClient getInstance()
        {
            if (MemClient == null)
            {
                lock (padlock)
                {
                    if (MemClient == null)
                    {
                        MemClientInit();
                    }
                }
            }
            return MemClient;
        }

        static void MemClientInit()
        {
            //初始化缓存
            MemcachedClientConfiguration memConfig = new MemcachedClientConfiguration();
            IPAddress newaddress = Dns.GetHostEntry("01efa5615db111e4.m.cnhzaliqshpub001.ocs.aliyuncs.com").AddressList[0];//your_instanceid替换为你的OCS实例的ID
            IPEndPoint ipEndPoint = new IPEndPoint(newaddress, 11211);

            // 配置文件 - ip
            memConfig.Servers.Add(ipEndPoint);
            // 配置文件 - 协议
            memConfig.Protocol = MemcachedProtocol.Binary;
            // 配置文件-权限
            memConfig.Authentication.Type = typeof(PlainTextAuthenticator);
            memConfig.Authentication.Parameters["zone"] = "";
            memConfig.Authentication.Parameters["userName"] = "01efa5615db111e4";
            memConfig.Authentication.Parameters["password"] = "ba07_f7ff";
            //下面请根据实例的最大连接数进行设置
            memConfig.SocketPool.MinPoolSize = 5;
            memConfig.SocketPool.MaxPoolSize = 200;
            MemClient = new MemcachedClient(memConfig);
        }

    }
}
