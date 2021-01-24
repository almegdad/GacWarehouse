using GacWarehouse.TaskScheduler.Models;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GacWarehouse.TaskScheduler.Helpers
{
    public class OrderJob : IJob
    {
        OrderApi _orderApi;
        public OrderJob()
        {
            _orderApi = new OrderApi();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Order Job Started!");            

            try
            {
                //file name pattern : [Status]_[Timestamp]_[Guid] , ex: New_99970660880861363_5a7001f6-273c-4598-9d73-efd3611f02d6.xml
                var folderPath = Path.GetFullPath(@$"../../Data/Orders/{DateTime.Now.ToString("ddMMyyyy")}");
                foreach (string filePath in Directory.EnumerateFiles(folderPath, "*.xml"))
                {
                    var fileName = Path.GetFileName(filePath);

                    if (fileName.StartsWith("New"))
                    {
                        var xml = File.ReadAllText(filePath);
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xml);

                        string json = JsonConvert.SerializeXmlNode(doc.DocumentElement);
                        var orderRequest = JsonConvert.DeserializeObject<OrderRequestDto>(json).Root;

                        var url = "Order/CreateNewOrder";
                        var orderResponse = await _orderApi.PostAPI<OrderResponse, OrderRequest>(url, orderRequest);
                        if (orderResponse.Success)
                        {
                            //rename the file to mark as processed
                            var newFileName = fileName.Replace("New", "Processed");
                            var newFileNamePath = @$"{Path.GetDirectoryName(filePath)}\{newFileName}";
                            File.Move(filePath, newFileNamePath);

                            await Console.Out.WriteLineAsync($"Order with Id: {orderResponse.Data.OrderId} is processed successfully.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Order Job Exception: {e?.Message}");
            }

            await Console.Out.WriteLineAsync("Order Job Finished!");
        }
    }
}
