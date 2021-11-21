using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoginExampleServer.Data;
using LoginExampleServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginExampleServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private IJobsService _jobsService;

        public JobsController(IJobsService jobsService)
        {
            this._jobsService = jobsService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> GetJobs([FromQuery] string? jobTitle, [FromQuery] int? jobId)
        {
            try
            {
                IList<Job> jobs = await _jobsService.GetJobsAsync();
                IList<Job> filteredJobs = new List<Job>();
                if (jobTitle == null && jobId == null)
                {
                    filteredJobs = jobs;
                }
                else if (jobId != null)
                {
                    foreach (Job job in jobs)
                    {
                        if (job.Id == jobId) filteredJobs.Add(job);
                    }
                }
                else
                {
                    foreach (Job job in jobs)
                    {
                        if (job.JobTitle.Equals(jobTitle)) filteredJobs.Add(job);
                    }
                }

                return Ok(filteredJobs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Adult>> AddJob([FromBody] Job job)
        {
            try
            {
                await _jobsService.AddJobAsync(job);
                return Created($"/{job.Id}", job);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteJob([FromRoute] int id)
        {
            try
            {
                await _jobsService.RemoveJobAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}