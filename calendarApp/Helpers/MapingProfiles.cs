namespace CalendarApp.Helpers
{
    using AutoMapper;
    using CalendarApp.Models.InputModels;
    using CalendarApp.Models.DbModels;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<LoginInputModel, ApplicationUser>().ReverseMap();
            CreateMap<RegisterInputModel, ApplicationUser>().ReverseMap();
            //CreateMap<EventInputModel, Event>().ReverseMap();
            //CreateMap<UserEventInputModel, UserEvent>().ReverseMap();
        }
    }
}