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
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        private readonly ILogger<TemplateController> _logger;

        public TemplateController(ITemplateService templateService, ILogger<TemplateController> logger)
        {
            _templateService = templateService;
            _logger = logger;
        }

        [HttpGet, Authorize]
        public ActionResult<TemplateListViewModel> GetAllTemplates()
        {
            var data = new ReturnMessageViewModel<TemplateListViewModel>();
            try
            {
                _logger.LogInformation("Getting all templates.");
                data.Message = "All templates retrieved successfully.";
                var template = _templateService.GetAll();
                data.Data = template;
                if (data.Data.Template.Count == 0) data.Message = "No templates were found.";
                data.Success = true;
                _logger.LogInformation("All templates retrieved successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting all templates: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpGet("{id}"), Authorize]
        public ActionResult<ReturnMessageViewModel<TemplateResponse>> GetTemplateById(string id)
        {
            var data = new ReturnMessageViewModel<TemplateResponse>();
            try
            {
                _logger.LogInformation($"Getting template with ID: {id}.");
                var template = _templateService.GetById(Guid.Parse(id));
                if (template == null)
                {
                    _logger.LogWarning("Template not found.");
                    data.Success = false;
                    data.Message = "Template not found.";
                    return NotFound(data);
                }

                data.Data = template;
                data.Success = true;
                data.Message = "Template found.";
                _logger.LogInformation("Template found.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting template: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpPost("add"), Authorize]
        public ActionResult<ReturnMessageViewModel<TemplateViewModel>> AddTemplate(TemplateRequest template)
        {
            var data = new ReturnMessageViewModel<TemplateViewModel>();
            if (template.Template == null || template.FieldList.TemplateField == null)
            {
                _logger.LogInformation("Adding template.");
                data.Success = false;
                data.Message = "Template data is missing.";
                return BadRequest(data);
            }
            try
            {
                _logger.LogInformation("Adding template.");
                var addedTemplate = _templateService.Add(template.Template, template.FieldList);
                data.Data = addedTemplate;
                data.Success = true;
                data.Message = "Template created successfully.";
                _logger.LogInformation("Template created successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding template: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpPut("update"), Authorize]
        public ActionResult<ReturnMessageViewModel<TemplateViewModel>> UpdateTemplate(TemplateRequest template)
        {
            var data = new ReturnMessageViewModel<TemplateViewModel>();
            if (template.Template == null)
            {
                _logger.LogInformation("Updating template.");
                return BadRequest("Template data is missing.");
            }
            try
            {
                _logger.LogInformation("Updating template.");
                var existingTemplate = _templateService.GetById(template.Template.TemplateId);
                if (existingTemplate == null)
                {
                    _logger.LogWarning("No record found to update.");
                    data.Success = false;
                    data.Message = "No record found to update.";
                    return NotFound(data);
                }

                var updatedTemplate = _templateService.Update(template.Template, template.FieldList);
                data.Data = updatedTemplate;
                data.Success = true;
                data.Message = "Template updated successfully.";
                _logger.LogInformation("Template updated successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating template: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpDelete("delete/{id}"), Authorize]
        public IActionResult DeleteTemplate(string id)
        {
            var data = new ReturnMessageViewModel<TemplateViewModel>();
            try
            {
                _logger.LogInformation($"Deleting template with ID: {id}.");
                var existingTemplate = _templateService.GetById(Guid.Parse(id));
                if (existingTemplate == null)
                {
                    _logger.LogWarning("No record found to delete.");
                    data.Success = false;
                    data.Message = "No record found to delete.";
                    return NotFound(data);
                }

                _templateService.Delete(Guid.Parse(id));
                data.Success = true;
                data.Message = "Template deleted successfully.";
                _logger.LogInformation("Template deleted successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting template: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }
    }
}
