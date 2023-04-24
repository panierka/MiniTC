using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    internal interface INameConflictResolver
    {
        public string Resolve(FilePath path);
    }
}
