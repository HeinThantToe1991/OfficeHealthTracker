using Microsoft.Extensions.Logging;
using System;
using Microsoft.VisualBasic;
using OfficeHealthTracker.Application.Mapper;
using OfficeHealthTracker.Interfaces;
using OfficeHealthTracker.Interfaces.Repository;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IAddressRepository addressRepository, ILogger<AddressService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public AddressListViewModel GetAll()
        {
            _logger.LogInformation("Getting all addresses.");
            var data = _addressRepository.GetAll();
            return AddressMapper.ToViewModelList(data);
        }

        public AddressViewModel GetById(Guid id)
        {
            _logger.LogInformation($"Getting address with ID {id}.");
            var address = _addressRepository.GetAddressById(id);
            return AddressMapper.ToViewModel(address);
        }

        public AddressViewModel Add(AddressViewModel address)
        {
            try
            {
                address.AddressId = Guid.NewGuid();
                address.CreatedDate = DateTime.Now;
                _addressRepository.Add(AddressMapper.ToDbModel(address));
                return address;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding address: {ex.Message}");
                return new AddressViewModel();
            }
        }

        public AddressViewModel Update(AddressViewModel addressViewModel)
        {
            try
            {
                addressViewModel.UpdatedDate = DateTime.Now;
                var address = AddressMapper.ToDbModel(addressViewModel);
                _addressRepository.Update(address);
                _logger.LogInformation($"Address with ID {addressViewModel.AddressId} updated successfully.");
                return addressViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating address: {ex.Message}");
                return new AddressViewModel();
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                _addressRepository.Delete(id);
                _logger.LogInformation($"Address with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting address: {ex.Message}");
                throw; // Rethrow the exception for handling in the upper layer
            }
        }
    }
}
