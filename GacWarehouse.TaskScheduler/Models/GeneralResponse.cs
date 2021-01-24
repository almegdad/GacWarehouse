﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GacWarehouse.TaskScheduler.Models
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
