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
                            //rename the file
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
