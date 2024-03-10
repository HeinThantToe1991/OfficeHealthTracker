using System.Collections.Generic;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Mapper
{
    public static class AddressMapper
    {
        public static AddressViewModel ToViewModel(Address model)
        {
            var viewModel = new AddressViewModel
            {
                AddressId = model.AddressId,
                Building = model.Building,
                Level = model.Level,
                Room = model.Room,
                CarPark = model.CarPark,
                Lobby = model.Lobby,
                Pantry = model.Pantry,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate
            };
            return viewModel;
        }

        public static Address ToDbModel(AddressViewModel viewModel)
        {
            var model = new Address
            {
                AddressId = viewModel.AddressId,
                Building = viewModel.Building,
                Level = viewModel.Level,
                Room = viewModel.Room,
                CarPark = viewModel.CarPark,
                Lobby = viewModel.Lobby,
                Pantry = viewModel.Pantry,
                CreatedDate = viewModel.CreatedDate,
                UpdatedDate = viewModel.UpdatedDate
            };
            return model;
        }

        public static AddressListViewModel ToViewModelList(IEnumerable<Address> models)
        {
            var viewModels = new AddressListViewModel
            {
                Address = new List<AddressViewModel>()
            };

            foreach (var model in models)
            {
                var viewModel = ToViewModel(model);
                viewModels.Address.Add(viewModel);
            }

            return viewModels;
        }
    }

}
