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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        [HttpGet, Authorize]
        public ActionResult<AddressListViewModel> GetAllAddresses()
        {
            var data = new ReturnMessageViewModel<AddressListViewModel>();
            try
            {
                _logger.LogInformation("Getting all addresses.");
                var addresses = _addressService.GetAll();
                var addressList = new AddressListViewModel { Address = addresses.Address };
                data.Data = addressList;
                data.Success = true;
                data.Message = "All addresses retrieved successfully.";
                _logger.LogInformation("All addresses retrieved successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting all addresses: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpGet("{id}"), Authorize]
        public IActionResult GetAddressById(string id)
        {
            var data = new ReturnMessageViewModel<AddressViewModel>();
            try
            {
                _logger.LogInformation($"Getting address with ID: {id}.");
                var address = _addressService.GetById(Guid.Parse(id));
                if (address == null)
                {
                    _logger.LogWarning("Address not found.");
                    data.Success = false;
                    data.Message = "Address not found.";
                    return NotFound(data);
                }

                data.Data = address;
                data.Success = true;
                data.Message = "Address found.";
                _logger.LogInformation("Address found.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting address: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpPost("add"), Authorize]
        public ActionResult<AddressViewModel> AddAddress(AddressViewModel address)
        {
            var data = new ReturnMessageViewModel<AddressViewModel>();
            if (address == null)
            {
                _logger.LogInformation("Adding address.");
                return BadRequest("Address data is missing.");
            }

            try
            {
                _logger.LogInformation("Adding address.");
                var addedAddress = _addressService.Add(address);
                data.Data = addedAddress;
                data.Success = true;
                data.Message = "Address added successfully.";
                _logger.LogInformation("Address added successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding address: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpPut("update"), Authorize]
        public ActionResult UpdateAddress(AddressViewModel address)
        {
            var data = new ReturnMessageViewModel<AddressViewModel>();
            if (address == null)
            {
                _logger.LogInformation("Updating address.");
                return BadRequest("Address data is missing.");
            }

            try
            {
                _logger.LogInformation("Updating address.");
                var existingAddress = _addressService.GetById(address.AddressId);
                if (existingAddress == null)
                {
                    _logger.LogWarning("No record found to update.");
                    data.Success = false;
                    data.Message = "No record found to update.";
                    return NotFound(data);
                }

                _addressService.Update(address);
                data.Success = true;
                data.Message = "Address updated successfully.";
                _logger.LogInformation("Address updated successfully.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating address: {ex.Message}");
                data.Success = false;
                data.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, data);
            }
        }

        [HttpDelete("delete/{id}"), Authorize]
        public IActionResult DeleteAddress(string id)
        {
            var data = new ReturnMessageViewModel<AddressViewModel>();
            try
            {
                _logger.LogInformation($"Deleting address with ID: {id}.");
                var existingAddress = _addressService.GetById(Guid.Parse(id));
                if (existingAddress == null)
                {
                    _logger.LogWarning("No record found to delete.");
                    data.Success = false;
                    data.Message = "No record found to delete.";
                    return NotFound(data);
                }

                _addressService.Delete(Guid.Parse(id));
                data.Success = true;
                data.Message = "Address deleted successfully.";
                _logger.LogInformation("Address deleted successfully.");
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
    }
}