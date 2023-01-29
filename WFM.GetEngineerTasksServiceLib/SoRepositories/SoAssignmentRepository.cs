using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WFM.GetEngineerTasksServiceLib.SsRepositories;
using WFM.GetEngineerTasksServiceLib.Mapper;

namespace WFM.GetEngineerTasksServiceLib.SoRepositories
{
    public class SoAssignmentRepository : ISoAssignmentRepository
    {
        readonly ISsAssignmentRepository _ssAssignmentRepository;
        readonly ISoAssignmentMapperFactory _soAssignmentMapperFactory;

        private IMapper _mapper;

        public SoAssignmentRepository(
            ISsAssignmentRepository ssAssignmentRepository,
            ISoAssignmentMapperFactory soAssignmentMapperFactory)
        {
            _ssAssignmentRepository = ssAssignmentRepository ?? throw new ArgumentNullException();
            _soAssignmentMapperFactory = soAssignmentMapperFactory ?? throw new ArgumentNullException();

            _mapper = _soAssignmentMapperFactory.CreateSoAssignmentMapper();
        }

        public SoServiceReference.Assignment[] GetSoAssignments(int engKey, DateTime dateFrom, DateTime dateTo)
        {
            var ssAssignments = _ssAssignmentRepository.GetSsAssignments(engKey, dateFrom, dateTo);

            var soAssignments = ssAssignments
                .Select(a => _mapper.Map<SoServiceReference.Assignment>(a))
                .ToArray();

            return soAssignments;
        }
    }
}