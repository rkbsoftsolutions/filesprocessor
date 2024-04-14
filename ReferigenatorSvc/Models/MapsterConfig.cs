using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ReferigenatorSvc.Models
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
       //     TypeAdapterConfig<ItemHistoryEntity,ItemViewModel>
       //.NewConfig()
       //.Map(dest => dest.UpdateItemQuantity, src => src.UsedQuantity);

           
        }
    }
}
