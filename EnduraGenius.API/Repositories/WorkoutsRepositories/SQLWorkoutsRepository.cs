﻿using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using Microsoft.EntityFrameworkCore;

//bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZGVsQGV4YW1wbGUuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIyOWIwOTc1Yy1iMzJmLTQ4NDItOTg4YS1lMDM4ZjA0NzBmZGUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNzM2NDk3NjE5LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDYzLyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwNjMvIn0.s4IXDuvP_6tRlNVz7_cm6BZURqcfJK0nDo0lLZ5iOnE
namespace EnduraGenius.API.Repositories
{
    public class SQLWorkoutsRepository : IWorkoutsRepository
    {
        private readonly EnduraGeniusDBContext _dbcontext;
        public SQLWorkoutsRepository(EnduraGeniusDBContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        public async Task<Workout?> CreateWorkout(Workout workout, Muscle MainMuscle , Muscle SecondaryMuscle, string UserID)
        {
            workout.MainMuscle = MainMuscle;
            workout.SecondaryMuscle = SecondaryMuscle;
            workout.WorkoutCreatedBy = UserID;
            workout.CreatedAt = DateTime.Now;
            workout.UpdatedAt = DateTime.Now;
            workout.IsCertified = true;

            await _dbcontext.Workouts.AddAsync(workout);
            await _dbcontext.SaveChangesAsync();
            return workout;
        }

        public async Task<bool> DeleteWorkout(Guid id)
        {
            var workout = await _dbcontext.Workouts.FindAsync(id);
            if (workout == null)
            {
                return false;
            }
            var workoutPlans = await _dbcontext.PlanWorkouts.Where(x => x.WorkoutId == id).ToListAsync();
            foreach (var planWorkout in workoutPlans)
            {
                _dbcontext.PlanWorkouts.Remove(planWorkout);
                _dbcontext.SaveChanges();
            }
            var userWorkouts = await _dbcontext.UserWorkouts.Where(x => x.WorkoutId == id).ToListAsync();
            foreach (var userWorkout in userWorkouts)
            {
                _dbcontext.UserWorkouts.Remove(userWorkout);
                _dbcontext.SaveChanges();
            }
            _dbcontext.Workouts.Remove(workout);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Workout?> GetWorkoutById(Guid id)
        {
            return await _dbcontext.Workouts.Include(x => x.MainMuscle).Include(x => x.SecondaryMuscle).Where(x => x.Id ==id).FirstOrDefaultAsync();
        }

        public async Task<List<Workout>> GetWorkouts(string? filterOn, string? filterQuery, int pageNumber, int pageSize ,bool IsCertified)
        {
            var workouts = _dbcontext.Workouts
                .Include(x => x.MainMuscle)
                .Include(x => x.SecondaryMuscle)
                .Include(x => x.WorkoutCreator)
                .Where(x => x.IsCertified == IsCertified)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                filterOn = filterOn.ToLower();
                switch (filterOn)
                {
                    case "name":
                        workouts = workouts.Where(x => x.Name.Contains(filterQuery));
                        break;
                    case "mainmuscle":
                        workouts = workouts.Where(x => x.MainMuscle.Name.Contains(filterQuery));
                        break;
                    case "secondarymuscle":
                        workouts = workouts.Where(x => x.SecondaryMuscle.Name.Contains(filterQuery));
                        break;
                    case "description":
                        workouts = workouts.Where(x => x.Description.Contains(filterQuery));
                        break;
                    case "workoutcreatedby":
                        workouts = workouts.Where(x => x.WorkoutCreatedBy.Contains(filterQuery));
                        break;
                    default:
                        return new List<Workout>();
                }
            }

            var skip = (pageNumber - 1) * pageSize;
            workouts = workouts.Skip(skip).Take(pageSize);

            return await workouts.ToListAsync();
        }

        public async Task<bool> UpdateWorkout(Workout workout , GetWorkoutDto updateWorkoutDto)
        {
            workout.Name = updateWorkoutDto.Name ?? workout.Name;
            workout.Link = updateWorkoutDto.Link ?? workout.Link;
            workout.Description = updateWorkoutDto.Description ?? workout.Description;
            if (updateWorkoutDto.MainMuscle != null)
            {
                var NewMainMuscle = await _dbcontext.Muscles.FirstOrDefaultAsync(x => x.Name == updateWorkoutDto.MainMuscle);
                if (NewMainMuscle != null)
                {
                    workout.MainMuscle = NewMainMuscle;
                }
                else
                {
                    return false;
                }
            }
            if (updateWorkoutDto.SecondaryMuscle != null)
            {
                var NewSecondaryMuscle = await _dbcontext.Muscles.FirstOrDefaultAsync(x => x.Name == updateWorkoutDto.SecondaryMuscle);
                if (NewSecondaryMuscle != null)
                {
                    workout.SecondaryMuscle = NewSecondaryMuscle;
                }
                else
                {
                    return false;
                }
            }
            _dbcontext.Workouts.Update(workout);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
        public async Task<Workout?> ChangeCertificationStatus(Guid id)
        {
            var workout = await _dbcontext.Workouts.FindAsync(id);
            if (workout == null)
            {
                return null;
            }
            workout.IsCertified = !workout.IsCertified;
            _dbcontext.Workouts.Update(workout);
            await _dbcontext.SaveChangesAsync();
            return workout;
        }
    }
}
