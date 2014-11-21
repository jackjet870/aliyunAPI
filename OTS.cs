using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleApplication1
{
    class OTS
    {
        private static void OTStest(string strResult, string rrrr)
        {

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://apptest.cn-hangzhou.ots.aliyuncs.com/ListTable?"
                + "TableGroupName=DemoTableGroup"
                + "&PartitionKeyType=STRING"
                + "&APIVersion=1"
                + "&Date=Tue%2C%2027%20Dec%202011%2013%3A36%3A01%20GMT"
                + "&OTSAccessKeyId=c8zwVmx3VB0jpo3l"
                + "&SignatureMethod=HmacSHA1"
                + "&SignatureVersion=1"
                + "&Signature=XXXXXXXXXXX");
            myHttpWebRequest.ContentType = "text/html";
            myHttpWebRequest.Method = "GET";
            //myHttpWebRequest.Referer = "";
            //myHttpWebRequest.Headers.Add("cookie:" + cookieHeader);

            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();

            Stream responseStream;
            StreamReader reader;
            responseStream = response.GetResponseStream();
            reader = new System.IO.StreamReader(responseStream, Encoding.UTF8);
            string srcString = reader.ReadToEnd();
        }
    }
}
