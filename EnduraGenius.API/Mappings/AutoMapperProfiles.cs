using AutoMapper;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;

namespace EnduraGenius.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Workout,CreateWorkoutRequestDTO>()
                .ForMember(x => x.MainMuscleName,opt => opt.MapFrom(x => x.MainMuscle.Name))
                .ForMember(x => x.SecondaryMuscleName,opt => opt.MapFrom(x => x.SecondaryMuscle.Name))
                .ReverseMap();
            CreateMap<Workout, GetWorkoutDto>()
                .ForMember(x => x.MainMuscle, opt => opt.MapFrom(x => x.MainMuscle.Name))
                .ForMember(x => x.SecondaryMuscle, opt => opt.MapFrom(x => x.SecondaryMuscle.Name))
                .ReverseMap();
            CreateMap<Muscle, CreateMuscleDTO>().ReverseMap();
            CreateMap<Plan, PlanResponseDTO>()
                .ForPath(x => x.PlanCreatedBy, opt => opt.MapFrom(x => x.planCreator.UserName))
                .ReverseMap();
            CreateMap<CreatePlanRequestDTO, Plan>().ReverseMap();
            CreateMap<PlanWorkout, PlanWorkoutsResponseDTO > ()
                .ForMember(x => x.PlanWorkoutID, opt => opt.MapFrom(x => x.Id))
                .ForPath(x => x.Link, opt => opt.MapFrom(x => x.Workout.Link))
                .ForPath(x => x.Description, opt => opt.MapFrom(x => x.Workout.Description))
                .ForPath(x => x.Name, opt => opt.MapFrom(x => x.Workout.Name))
                .ForPath(x => x.WorkoutID, opt => opt.MapFrom(x => x.Workout.Id))
                .ForPath(x => x.MainMuscle, opt => opt.MapFrom(x => x.Workout.MainMuscle.Name))
                .ForPath(x => x.SecondaryMuscle, opt => opt.MapFrom(x => x.Workout.SecondaryMuscle.Name))
                .ReverseMap();
            CreateMap<UserWorkoutResponseDTO, UserWorkout>()
                .ForPath(x => x.Workout.Name, opt => opt.MapFrom(x => x.Name))
                .ForPath(x => x.Workout.Description, opt => opt.MapFrom(x => x.Descreption))
                .ReverseMap();
        }
    }
}
