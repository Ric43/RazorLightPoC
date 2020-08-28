using Microsoft.Extensions.Localization;

namespace za.co.chowned.TemplateResources.Models
{
    public class ViewModel
    {
        public IStringLocalizer Localizer { get; }

        public ViewModel(IStringLocalizer localizer)
        {
            Localizer = localizer;
        }

        public string Name { get; set; }
    }
}
