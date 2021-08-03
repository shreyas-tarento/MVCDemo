using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models
{
    public class CommonResult
    {
        public Object Success { get; set; }
        public List<String> Error { get; set; }
    }
}
