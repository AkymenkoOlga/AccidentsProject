using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentsProject.Controllers.Dtos;
using AccidentsProject.Services;
using AccidentsProject.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccidentsProject.Controllers
{
    [Route("api/accidents")]
    public class AccidentsController : Controller
    {
        private readonly IAccidentService accidentService;

        public AccidentsController(IAccidentService accidentService)
        {
            this.accidentService = accidentService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetAccidentsAsync([FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            IEnumerable<Accident> accidents = 
                await this.accidentService.GetAccidentsAsync()
                    .ConfigureAwait(false);

            if (startDate != null && endDate != null)
            {
                accidents = accidents.Where(a => a.Date >= startDate.Date && 
                    a.Date <= endDate.AddDays(1).AddTicks(-1));
            }

            return Ok(accidents.Select(AccidentDto.From).ToArray());
        }

        [HttpGet, Route("pages")]
        public async Task<IActionResult> GetPaginationAccidentsAsync([FromRoute]int skip, [FromQuery]int take)
        {
            IEnumerable<Accident> accidents =
                await this.accidentService.GetAccidentsAsync(skip, take)
                    .ConfigureAwait(false);

            return Ok(accidents.Select(AccidentDto.From).ToArray());
        }

        [HttpGet, Route("{accidentId}")]
        public async Task<IActionResult> GetAccidentAsync([FromRoute]string accidentId)
        {
            Accident accident =
                await this.accidentService.GetAccidentAsync(accidentId)
                    .ConfigureAwait(false);

            return Ok(AccidentDto.From(accident));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAccidentAsync([FromBody]AccidentDto accidentDto)
        {
            if (accidentDto == null)
                return BadRequest();

            Accident accident = await this.accidentService
                .CreateAccidentAsync(accidentDto.ToModel())
                .ConfigureAwait(false);

            if (accident == null)
                return BadRequest();

            return Ok(AccidentDto.From(accident));
        }

        [HttpPut, Route("{accidentId}")]
        public async Task<IActionResult> UpdateAccidentAsync([FromRoute]string accidentId, [FromBody]AccidentDto accidentDto)
        {
            if (accidentDto == null)
                return BadRequest();

            await this.accidentService
                .UpdateAccidentAsync(accidentDto.ToModel())
                .ConfigureAwait(false);

            return Ok();
        }

        [HttpDelete, Route("{accidentId}")]
        public async Task<IActionResult> DeleteAccidentAsync([FromRoute]string accidentId)
        {
            await this.accidentService
                .DeleteAccidentAsync(accidentId)
                .ConfigureAwait(false);

            return Ok();
        }

        [HttpGet, Route("tags")]
        public async Task<IActionResult> GetAccidentsTagsAsync()
        {
            IEnumerable<string> tags = await this.accidentService
                .GetTagsAsync()
                .ConfigureAwait(false);

            return Ok(tags.ToArray());
        }
    }
}
