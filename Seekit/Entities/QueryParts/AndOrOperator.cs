using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seekit.Entities
{
    public class AndOrOperator: IQueryPart
    {
        public string Operator { get; set; }
    }
}
