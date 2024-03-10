using System.Collections.Generic;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Mapper
{
    public static class TemplateFieldMapper
    {
        public static TemplateFieldViewModel ToViewModel(TemplateField model)
        {
            var viewModel = new TemplateFieldViewModel
            {
                TemplateId = model.TemplateId,
                TemplateFieldId = model.TemplateFieldId,
                FieldTypeId = model.FieldTypeId,
                FieldOptions = model.FieldOptions,
                Sorting = model.Sorting,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate
            };
            return viewModel;
        }

        public static TemplateField ToDbModel(TemplateFieldViewModel viewModel)
        {
            var model = new TemplateField
            {
                TemplateId = viewModel.TemplateId,
                FieldTypeId = viewModel.FieldTypeId,
                FieldOptions = viewModel.FieldOptions,
                Sorting = viewModel.Sorting,
                CreatedDate = viewModel.CreatedDate,
                UpdatedDate = viewModel.UpdatedDate
            };
            return model;
        }

        public static TemplateFieldListViewModel ToViewModelList(IEnumerable<TemplateField> models)
        {
            var viewModels = new TemplateFieldListViewModel
            {
                TemplateField = new List<TemplateFieldViewModel>()
            };

            foreach (var model in models)
            {
                var viewModel = ToViewModel(model);
                viewModels.TemplateField.Add(viewModel);
            }

            return viewModels;
        }
    }

}
