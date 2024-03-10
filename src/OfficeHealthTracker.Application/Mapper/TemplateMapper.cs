using System.Collections.Generic;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Mapper
{
    public static class TemplateMapper
    {
        public static TemplateViewModel ToViewModel(Template model)
        {
            var viewModel = new TemplateViewModel
            {
                TemplateId = model.TemplateId,
                TemplateName = model.TemplateName,

                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate
            };
            return viewModel;
        }

        public static Template ToDbModel(TemplateViewModel viewModel)
        {
            var model = new Template
            {
                TemplateId = viewModel.TemplateId,
                TemplateName = viewModel.TemplateName,
                CreatedDate = viewModel.CreatedDate,
                UpdatedDate = viewModel.UpdatedDate
            };
            return model;
        }

        public static TemplateListViewModel ToViewModelList(IEnumerable<Template> models)
        {
            var viewModels = new TemplateListViewModel
            {
                Template = new List<TemplateViewModel>()
            };

            foreach (var model in models)
            {
                var viewModel = ToViewModel(model);
                viewModels.Template.Add(viewModel);
            }

            return viewModels;
        }
    }

}
