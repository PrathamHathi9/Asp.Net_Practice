using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace dotNet.Models
{
    public class InformationBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var houseNumber = bindingContext.ValueProvider.GetValue("houseNumber").FirstValue;
            var near = bindingContext.ValueProvider.GetValue("near").FirstValue;
            var area = bindingContext.ValueProvider.GetValue("area").FirstValue;

            var information = new Information
            {
                Address = $"{houseNumber},{near},{area}"
            };

            bindingContext.Result = ModelBindingResult.Success(information);
            return Task.CompletedTask;
        }
    }
}
