using Mapster;
using Microsoft.Extensions.DependencyInjection;
using ReferigenatorSvc.dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.Models
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<ItemHistoryEntity,ItemViewModel>
       .NewConfig()
       .Map(dest => dest.UpdateItemQuantity, src => src.UsedQuantity);

           
        }
    }
}
