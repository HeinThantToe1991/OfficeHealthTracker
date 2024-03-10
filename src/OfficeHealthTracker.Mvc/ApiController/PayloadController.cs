using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeHealthTracker.Domain;
using OfficeHealthTracker.Interfaces;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Mvc.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayloadController : ControllerBase
    {
        private readonly IPayloadService _service;
        private readonly ILogger<PayloadController> _logger;

        public PayloadController(IPayloadService service, ILogger<PayloadController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("add"), Authorize]
        public ActionResult<PayloadViewModel> AddFieldType(PayloadViewModel viewModel)
        {
            var data = new ReturnMessageViewModel<PayloadViewModel>();
            if (viewModel == null)
            {
                return BadRequest("payload data is missing.");
            }
            try
            {
                _logger.LogInformation("Adding payload.");
                var addedFieldType = _service.Add(viewModel);
                data.Data = addedFieldType;
                data.Success = true;
                data.Message = "payload created successfully.";
                _logger.LogInformation("payload created successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding payload: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

       
    }
}
