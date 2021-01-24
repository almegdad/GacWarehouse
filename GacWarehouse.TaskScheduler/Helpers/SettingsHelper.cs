using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GacWarehouse.TaskScheduler.Helpers
{
    public class SettingsHelper
    {
        private readonly IConfiguration _configuration;
        public SettingsHelper()
        {
            _configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", true, true)
          .Build();
        }

        public string CronExpression { get { return _configuration["Quartz:CronExpression"]; } }
        public string Username { get { return _configuration["Account:Username"]; } }
        public string Password { get { return _configuration["Account:Password"]; } }
    }
}
