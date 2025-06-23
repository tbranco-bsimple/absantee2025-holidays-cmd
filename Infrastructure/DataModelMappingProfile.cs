using AutoMapper;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;

namespace Infrastructure;

public class DataModelMappingProfile : Profile
{
    public DataModelMappingProfile()
    {

        CreateMap<Collaborator, CollaboratorDataModel>();
        CreateMap<CollaboratorDataModel, Collaborator>()
            .ConvertUsing<CollaboratorDataModelConverter>();

        CreateMap<HolidayPlan, HolidayPlanDataModel>();
        CreateMap<HolidayPlanDataModel, HolidayPlan>()
            .ConvertUsing<HolidayPlanDataModelConverter>();

        CreateMap<HolidayPeriod, HolidayPeriodDataModel>();
        CreateMap<HolidayPeriodDataModel, HolidayPeriod>()
            .ConvertUsing<HolidayPeriodDataModelConverter>();
    }

}