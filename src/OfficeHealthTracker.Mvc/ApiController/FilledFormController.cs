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
    public class FilledFormController : ControllerBase
    {
        private readonly IFilledFormService _filledFormService;
        private readonly ILogger<FilledFormController> _logger;

        public FilledFormController(IFilledFormService filledFormService, ILogger<FilledFormController> logger)
        {
            _filledFormService = filledFormService;
            _logger = logger;
        }

        [HttpGet, Authorize]
        public ActionResult<FilledFormListViewModel> GetAllFilledForms()
        {
            var data = new ReturnMessageViewModel<FilledFormListViewModel>();
            try
            {
                _logger.LogInformation("Getting all filled forms.");
                var filledForms = _filledFormService.GetAll();
                var filledFormList = new FilledFormListViewModel { FilledForm = filledForms.FilledForm };
                data.Data = filledFormList;
                data.Success = true;
                data.Message = "All filled forms retrieved successfully.";
                _logger.LogInformation("All filled forms retrieved successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting all filled forms: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpGet("{id}"), Authorize]
        public ActionResult<ReturnMessageViewModel<FilledFormViewModel>> GetFilledFormById(string id)
        {
            var data = new ReturnMessageViewModel<FilledFormViewModel>();
            try
            {
                _logger.LogInformation($"Getting filled form with ID: {id}.");
                var filledForm = _filledFormService.GetById(Guid.Parse(id));
                if (filledForm == null)
                {
                    _logger.LogWarning("Filled form not found.");
                    data.Success = false;
                    data.Message = "Filled form not found.";
                    return NotFound(data);
                }

                data.Data = filledForm;
                data.Success = true;
                data.Message = "Filled form found.";
                _logger.LogInformation("Filled form found.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting filled form: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpPost("add"), Authorize]
        public ActionResult<FilledFormViewModel> AddFilledForm(FilledFormViewModel viewModel)
        {
            var data = new ReturnMessageViewModel<FilledFormViewModel>();
            if (viewModel == null)
            {
                _logger.LogInformation("Adding filled form.");
                return BadRequest("Filled form data is missing.");
            }
            try
            {
                _logger.LogInformation("Adding filled form.");
                var addedFilledForm = _filledFormService.Add(viewModel);
                data.Data = addedFilledForm;
                data.Success = true;
                data.Message = "Filled form created successfully.";
                _logger.LogInformation("Filled form created successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding filled form: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpPut("update"), Authorize]
        public ActionResult<FilledFormViewModel> UpdateFilledForm(FilledFormViewModel viewModel)
        {
            var data = new ReturnMessageViewModel<FilledFormViewModel>();
            if (viewModel == null)
            {
                _logger.LogInformation("Updating filled form.");
                return BadRequest("Filled form data is missing.");
            }
            try
            {
                _logger.LogInformation("Updating filled form.");
                var existingFilledForm = _filledFormService.GetById(viewModel.FilledFormId);
                if (existingFilledForm == null)
                {
                    _logger.LogWarning("No record found to update.");
                    data.Success = false;
                    data.Message = "No record found to update.";
                    return NotFound(data);
                }

                var updatedFilledForm = _filledFormService.Update(viewModel);
                data.Data = updatedFilledForm;
                data.Success = true;
                data.Message = "Filled form updated successfully.";
                _logger.LogInformation("Filled form updated successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating filled form: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpDelete("delete/{id}"), Authorize]
        public IActionResult DeleteFilledForm(string id)
        {
            var data = new ReturnMessageViewModel<FilledFormViewModel>();
            try
            {
                _logger.LogInformation($"Deleting filled form with ID: {id}.");
                var existingFilledForm = _filledFormService.GetById(Guid.Parse(id));
                if (existingFilledForm == null)
                {
                    _logger.LogWarning("No record found to delete.");
                    data.Success = false;
                    data.Message = "No record found to delete.";
                    return NotFound(data);
                }

                _filledFormService.Delete(Guid.Parse(id));
                data.Success = true;
                data.Message = "Filled form deleted successfully.";
                _logger.LogInformation("Filled form deleted successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting filled form: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }
    }
}
