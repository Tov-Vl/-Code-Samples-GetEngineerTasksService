using System;
using System.Collections.Generic;
using System.Text;

using WFM.GetEngineerTasksServiceLib.Helpers;

using SoServiceReference;

namespace WFM.GetEngineerTasksServiceLib.SoRepositories
{
    public class ValidatingSoEngineerRepository : ISoEngineerRepository
    {
        public ISoEngineerRepository Target { get; set; }

        public Engineer GetEngineer(string engLogin)
        {
            if (Target == null)
                throw new ArgumentNullException();

            var res = Target.GetEngineer(engLogin);

            if (res == null)
                throw new EngineerNotFoundException($"Агент {engLogin} не найден");

            return res;
        }
    }
}
