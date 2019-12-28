using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geomethod.Data
{
    public abstract class Binder
    {
        public abstract void UseTableSchema(string name);
        public abstract void Bind(ref int val);
        public abstract void Bind(ref float val);
        public abstract void Bind(ref double val);
    }
}
