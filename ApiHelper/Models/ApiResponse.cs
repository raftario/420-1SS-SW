using System;
using System.Collections.Generic;
using System.Text;

namespace ApiHelper.Models
{
    public class ApiResponse<T>
    {
        public T Message { get; set; }
        public string Status { get; set; }
    }
}
