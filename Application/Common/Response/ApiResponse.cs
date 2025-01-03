using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Response
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool Result { get; set; }
        public string? Message { get; set; }
        public string? Url { get; set; }
    }
}
