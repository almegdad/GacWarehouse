using System;
using System.Collections.Generic;
using System.Text;

namespace GacWarehouse.Core.Models
{
    public class ProfileResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
