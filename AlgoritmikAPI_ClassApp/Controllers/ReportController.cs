using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/info/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReport _IReport;

        public ReportController(IReport IReport)
        {
            _IReport = IReport;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> Get()
        {
            return await Task.FromResult(_IReport.GetReportDetails());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> Get(int id)
        {
            var report = await Task.FromResult(_IReport.GetReportDetails(id));
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }

        [HttpPost]
        public async Task<ActionResult<Report>> Post(Report report)
        {
            _IReport.AddReport(report);
            return await Task.FromResult(report);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Report>> Put(int id, Report report)
        {
            if (id != report.reportId)
            {
                return BadRequest();
            }
            try
            {
                _IReport.UpdateReport(report);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(report);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Report>> Delete(int id)
        {
            var report = _IReport.DeleteReport(id);
            return await Task.FromResult(report);
        }

        private bool ReportExists(int id)
        {
            return _IReport.CheckReport(id);
        }
    }
}
