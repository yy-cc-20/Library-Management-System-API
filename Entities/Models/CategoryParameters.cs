using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CategoryParameters : QueryStringParameters
    {
        public CategoryParameters() 
        {
            OrderBy = "Name";
        }

        public string? Name { get; set; }
    }
}
