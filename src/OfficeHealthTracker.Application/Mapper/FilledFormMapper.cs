using System.Collections.Generic;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Mapper
{
    public static class FilledFormMapper
    {
        public static FilledFormViewModel ToViewModel(FilledForm model)
        {
            var viewModel = new FilledFormViewModel
            {
                FilledFormId = model.FilledFormId,
                TemplateId = model.TemplateId,
                FieldValues = model.FieldValues,
                Timestamp = model.Timestamp,
            };
            return viewModel;
        }

        public static FilledForm ToDbModel(FilledFormViewModel viewModel)
        {
            var model = new FilledForm
            {
                FilledFormId = viewModel.FilledFormId,
                TemplateId = viewModel.TemplateId,
                FieldValues = viewModel.FieldValues,
                Timestamp = viewModel.Timestamp,
            };
            return model;
        }

        public static FilledFormListViewModel ToViewModelList(IEnumerable<FilledForm> models)
        {
            var viewModels = new FilledFormListViewModel
            {
                FilledForm = new List<FilledFormViewModel>()
            };

            foreach (var model in models)
            {
                var viewModel = ToViewModel(model);
                viewModels.FilledForm.Add(viewModel);
            }

            return viewModels;
        }
    }

}
