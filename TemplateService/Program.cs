using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RazorLight;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using za.co.chowned.TemplateResources.Models;
using za.co.chowned.TemplateService.Resources;

namespace za.co.chowned.TemplateService
{
    class Program
    {
        private static LoggerFactory _loggerFactory;
        private static ResourceManagerStringLocalizerFactory _resourceFactory;

        static async Task<int> Main(string[] args)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-ZA");
            //var culture = CultureInfo.CreateSpecificCulture("en-GB");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            _loggerFactory = new LoggerFactory();

            var localizationOptions = new LocalizationOptions { ResourcesPath = "" };
            var options = Options.Create(localizationOptions);

            _resourceFactory = new ResourceManagerStringLocalizerFactory(options, _loggerFactory);
            var localizer = _resourceFactory.Create(typeof(Shared));

            Assembly resourceAssembly = Assembly.GetAssembly(typeof(ViewModel));

            var engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(resourceAssembly)
                .UseMemoryCachingProvider()
                .Build();

            var model = new ViewModel(localizer) { Name = "John Doe" };
            string result = await engine.CompileRenderAsync("za.co.chowned.TemplateResources.EmailTemplates.EmailBodyTemplate", model);

            Console.WriteLine(result);

            return 0;
        }
    }
}
