using TechBuddy.Middlewares.ExceptionHandling;
using TechBuddy.Middlewares.ExceptionHandling.Infrastructure;

namespace YoloSozluk.Api.WebApi.Extensions
{
    public class CustomResponseModelCreator : IResponseModelCreator
    {
        public object CreateModel(ModelCreatorContext model)
        {
            return new
            {
                ExMes = model.ErrorMessage,
                DetailedExMes = model.Exception.ToString()
            };
        }
    }
}
