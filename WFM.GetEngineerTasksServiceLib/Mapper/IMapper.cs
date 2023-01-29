using System;
using System.Collections.Generic;
using System.Text;

namespace WFM.GetEngineerTasksServiceLib.Mapper
{
    public interface IMapper<TSource, TTarget>
    {
        TTarget Map(TSource source);
    }
}
