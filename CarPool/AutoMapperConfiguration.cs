using CarPool.DataAccess.Entities;
using CarPool.Models;

namespace CarPool
{
    public static class AutoMapperConfiguration
    {
        public static void Init()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TravelPlanCreateModel, TravelPlan>();
                cfg.CreateMap<TravelPlanEmployeeCreateModel, TravelPlanEmployee>();
                cfg.CreateMap<TravelPlanUpdateModel, TravelPlan>();
            });
        }
    }
}
