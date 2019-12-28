using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geomethod.Data
{
    public enum ColType{String=DbType.String,Int=DbType.Int32,DateTime=DbType.DateTime}

    interface IBindable
    {
        void Bind(Binder binder);
    }
}
