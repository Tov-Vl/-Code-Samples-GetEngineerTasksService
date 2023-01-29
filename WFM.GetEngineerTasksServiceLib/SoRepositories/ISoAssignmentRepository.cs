using System;
using System.Collections.Generic;
using System.Text;

using SoServiceReference;

namespace WFM.GetEngineerTasksServiceLib.SoRepositories
{
    public interface ISoAssignmentRepository 
    {
        public Assignment[] GetSoAssignments(int engKey, DateTime dateFrom, DateTime dateTo);
    }
}
