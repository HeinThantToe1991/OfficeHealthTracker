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
    public class FieldTypeController : ControllerBase
    {
        private readonly IFieldTypeService _fieldTypeService;
        private readonly ILogger<FieldTypeController> _logger;

        public FieldTypeController(IFieldTypeService fieldTypeService, ILogger<FieldTypeController> logger)
        {
            _fieldTypeService = fieldTypeService;
            _logger = logger;
        }

        [HttpGet, Authorize]
        public ActionResult<FieldTypeListViewModel> GetAllFieldTypes()
        {
            var data = new ReturnMessageViewModel<FieldTypeListViewModel>();
            try
            {
                _logger.LogInformation("Getting all field types.");
                var fieldTypes = _fieldTypeService.GetAll();
                var fieldTypeList = new FieldTypeListViewModel { FieldType = fieldTypes.FieldType };
                data.Data = fieldTypeList;
                data.Success = true;
                data.Message = "All field types retrieved successfully.";
                _logger.LogInformation("All field types retrieved successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting all field types: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpPost("add"), Authorize]
        public ActionResult<FieldTypeViewModel> AddFieldType(FieldTypeViewModel viewModel)
        {
            var data = new ReturnMessageViewModel<FieldTypeViewModel>();
            if (viewModel == null)
            {
                return BadRequest("Field type data is missing.");
            }
            try
            {
                _logger.LogInformation("Adding field type.");
                var addedFieldType = _fieldTypeService.Add(viewModel);
                data.Data = addedFieldType;
                data.Success = true;
                data.Message = "Field type created successfully.";
                _logger.LogInformation("Field type created successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding field type: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpPut("update"), Authorize]
        public ActionResult<FieldTypeViewModel> UpdateFieldType(FieldTypeViewModel viewModel)
        {
            var data = new ReturnMessageViewModel<FieldTypeViewModel>();
            if (viewModel == null)
            {
                _logger.LogInformation("Updating field type.");
                return BadRequest("Field type data is missing.");
            }
            try
            {
                _logger.LogInformation("Updating field type.");
                var existingFieldType = _fieldTypeService.GetById(viewModel.FieldTypeId);
                if (existingFieldType == null)
                {
                    _logger.LogWarning("No record found to update.");
                    data.Success = false;
                    data.Message = "No record found to update.";
                    return NotFound(data);
                }

                var updatedFieldType = _fieldTypeService.Update(viewModel);
                data.Data = updatedFieldType;
                data.Success = true;
                data.Message = "Field type updated successfully.";
                _logger.LogInformation("Field type updated successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating field type: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpDelete("delete/{id}"), Authorize]
        public IActionResult DeleteFieldType(string id)
        {
            var data = new ReturnMessageViewModel<FieldTypeViewModel>();
            try
            {
                _logger.LogInformation($"Deleting field type with ID: {id}.");
                var existingFieldType = _fieldTypeService.GetById(Guid.Parse(id));
                if (existingFieldType == null)
                {
                    _logger.LogWarning("No record found to delete.");
                    data.Success = false;
                    data.Message = "No record found to delete.";
                    return NotFound(data);
                }

                _fieldTypeService.Delete(Guid.Parse(id));
                data.Success = true;
                data.Message = "Field type deleted successfully.";
                _logger.LogInformation("Field type deleted successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting field type: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpGet("id/{id}"), Authorize]
        public IActionResult GetFieldTypeById(string id)
        {
            var data = new ReturnMessageViewModel<FieldTypeViewModel>();
            try
            {
                _logger.LogInformation($"Getting field type with ID: {id}.");
                var fieldType = _fieldTypeService.GetById(Guid.Parse(id));
                if (fieldType == null)
                {
                    _logger.LogWarning("Field type not found.");
                    data.Success = false;
                    data.Message = "Field type not found.";
                    return NotFound(data);
                }

                data.Data = fieldType;
                data.Success = true;
                data.Message = "Field type found.";
                _logger.LogInformation("Field type found.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting field type: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }
    }
}
