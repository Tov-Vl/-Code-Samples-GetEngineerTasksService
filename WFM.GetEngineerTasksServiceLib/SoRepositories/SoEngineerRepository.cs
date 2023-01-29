using System;
using System.Collections.Generic;
using System.Text;

using WFM.So.Repositories;

using SoServiceReference;

namespace WFM.GetEngineerTasksServiceLib.SoRepositories
{
    public class SoEngineerRepository : ISoEngineerRepository
    {
        readonly IRepository<Engineer> _soEngineerRepository;

        public SoEngineerRepository(IRepository<Engineer> soEngineerRepository)
        {
            _soEngineerRepository = soEngineerRepository ?? throw new ArgumentNullException();
        }

        public Engineer GetEngineer(string engLogin)
        {
            var res = _soEngineerRepository.GetItem(new { ID = engLogin }, i => i);

            return res;
        }
    }
}
