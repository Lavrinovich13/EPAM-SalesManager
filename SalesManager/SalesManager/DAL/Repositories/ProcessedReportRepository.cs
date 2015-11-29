using DAL.AbstractRepository;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProcessedReportRepository : DataRepository<ProcessedReport, EntityModels.ProcessedReport>
    {
        protected override EntityModels.ProcessedReport ObjectToEntity(ProcessedReport item)
        {
            return new EntityModels.ProcessedReport()
            {
                Id = item.Id,
                FileName = item.FileName
            };
        }

        protected override ProcessedReport EntityToObject(EntityModels.ProcessedReport item)
        {
            return new ProcessedReport()
            {
                Id = item.Id,
                FileName = item.FileName
            };
        }
    }
}
