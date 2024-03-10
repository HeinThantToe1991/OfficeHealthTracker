using System.Collections.Generic;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Mapper
{
    public static class FieldTypeMapper
    {
        public static FieldTypeViewModel ToViewModel(FieldType model)
        {
            var viewModel = new FieldTypeViewModel
            {
                FieldTypeId = model.FieldTypeId,
                TypeName = model.TypeName,
                // Map other properties as needed
            };
            return viewModel;
        }

        public static FieldType ToDbModel(FieldTypeViewModel viewModel)
        {
            var model = new FieldType
            {
                FieldTypeId = viewModel.FieldTypeId,
                TypeName = viewModel.TypeName,
                // Map other properties as needed
            };
            return model;
        }

        public static FieldTypeListViewModel ToViewModelList(IEnumerable<FieldType> models)
        {
            var viewModels = new FieldTypeListViewModel
            {
                FieldType = new List<FieldTypeViewModel>()
            };

            foreach (var model in models)
            {
                var viewModel = ToViewModel(model);
                viewModels.FieldType.Add(viewModel);
            }

            return viewModels;
        }
    }

}
