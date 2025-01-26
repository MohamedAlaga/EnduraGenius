using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.PlansUsersRepositories;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Repositories.PlanRepositories
{
    public class SQLPLansRepository : IPlanRepository
    {
        private readonly EnduraGeniusDBContext _context;
        public SQLPLansRepository(EnduraGeniusDBContext dBContext)
        {
            _context = dBContext;
        }

        public async Task<Plan?> CreatePlan(string name, string description, string userId, bool isPublic)
        {
            try
            {
                var plan = new Plan
                {
                    Name = name,
                    Descreption = description,
                    PlanCreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsPublic = isPublic
                };
                await _context.Plans.AddAsync(plan);
                await _context.SaveChangesAsync();
                var newPlan = await _context.Plans.Include(x => x.planCreator).FirstOrDefaultAsync(x => x.Id == plan.Id);
                return newPlan;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> DeletePlan(Guid id, string UserId)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                return false;
            }
            var userPlans = await _context.PlansUsers.Where(x => x.PlanId == id).ToListAsync();
            foreach (var userPlan in userPlans)
            {
                _context.PlansUsers.Remove(userPlan);
                await _context.SaveChangesAsync();
            }
            var planWorkouts = await _context.PlanWorkouts.Where(x => x.PlanId == id).ToListAsync();
            foreach (var planWorkout in planWorkouts)
            {
                _context.PlanWorkouts.Remove(planWorkout);
                await _context.SaveChangesAsync();
            }
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Plan?> GetPlanById(Guid id, string userId)
        {
            return await _context.Plans.Include(x => x.planCreator).Where(x=> x.Id == id).Where(x=> x.IsPublic || x.PlanCreatedBy ==userId).FirstOrDefaultAsync();
        }

        public async Task<List<Plan>> GetPlansByCreatorId(string userId)
        {
            return await _context.Plans.Include(x => x.planCreator).Where(x => x.PlanCreatedBy == userId).ToListAsync();
        }

        public async Task<List<Plan>> GetPublicPlans(string userId)
        {
            return await _context.Plans.Include(x => x.planCreator).Where(x => x.IsPublic == true || x.PlanCreatedBy == userId).ToListAsync();
        }

        public async Task<Plan?> UpdatePlan(Guid id,string userId, string? name, string? description, bool? isPublic,List<CreatePlanWorkoutsDto>? workoutsDtos)
        {
            var plan = _context.Plans.Find(id);
            if (plan == null || plan.PlanCreatedBy != userId)
            {
                return await Task.FromResult<Plan?>(null);
            }
            plan.Name = name ?? plan.Name;
            plan.Descreption = description ?? plan.Descreption;
            plan.IsPublic = isPublic ?? plan.IsPublic;
            plan.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            if (workoutsDtos != null && workoutsDtos.Count > 0)
            {
                var planWorkouts = await _context.PlanWorkouts.Where(x => x.PlanId == id).ToListAsync();
                _context.PlanWorkouts.RemoveRange(planWorkouts);
                await _context.SaveChangesAsync();
                foreach (var workoutDto in workoutsDtos)
                {
                    var workout = await _context.Workouts.FindAsync(workoutDto.WorkoutId);
                    if (workout == null)
                    {
                        continue;
                    }
                    var planWorkout = new PlanWorkout
                    {
                        PlanId = id,
                        WorkoutId = workoutDto.WorkoutId,
                        Order = workoutDto.Order,
                        Reps = workoutDto.Reps
                    };
                    await _context.PlanWorkouts.AddAsync(planWorkout);
                }
                await _context.SaveChangesAsync();
            }

            return await Task.FromResult<Plan?>(plan);
        }


        public async Task<List<CustomWorkoutDTO>> CreateProSplitWokoutPlan(string userId)
        {
            var shoulder_Top = await GetLeastPlayed(userId, Guid.Parse("df475ba8-6be7-42b6-871f-6a29cd4c91d8"), 2);
            var shoulder_front = await GetLeastPlayed(userId, Guid.Parse("d729e9f8-3384-4873-83bc-74acdccacabe"), 2);
            var shoulder_Back = await GetLeastPlayed(userId, Guid.Parse("c4ac8999-40a4-40c8-9518-42be7e2d0744"), 3);
            var upper_chest = await GetLeastPlayed(userId, Guid.Parse("8909c9d4-2f45-4a2f-90c8-d6f475820a2f"), 3);
            var lower_chest = await GetLeastPlayed(userId, Guid.Parse("d8738439-5832-4764-8290-20ebe48d50dc"), 3);
            var lats = await GetLeastPlayed(userId, Guid.Parse("a2528d27-0dfe-48c8-9a18-7887dd9743ef"), 3);
            var middle_back = await GetLeastPlayed(userId, Guid.Parse("405069d0-6e66-4d50-883d-e5b29a403a84"), 3);
            var lower_back = await GetLeastPlayed(userId, Guid.Parse("b61c068e-8e8b-4756-a05c-482d0ec70f9e"), 2);
            var Quadriceps = await GetLeastPlayed(userId, Guid.Parse("b391d0e8-442e-414a-85cc-9445b3bd11be"), 2);
            var Hamstrings = await GetLeastPlayed(userId, Guid.Parse("2c6891f3-98d9-4bfe-bf90-a7e5b6b1caf8"), 2);
            var Gluteus = await GetLeastPlayed(userId, Guid.Parse("619fe532-8226-4d84-9a61-b64bcdef8084"), 2);
            var calves1 = await GetLeastPlayed(userId, Guid.Parse("e56d075d-e1db-49d0-be1c-a925b80e87a6"), 1);
            var calves2 = await GetLeastPlayed(userId, Guid.Parse("cb5afad8-c1ae-44e5-ac7b-aa992f5c80a7"), 1);
            var forearms = await GetLeastPlayed(userId, Guid.Parse("7be040bd-2759-4b13-a049-8cde648dfd75"), 2);
            var biceps = await GetLeastPlayed(userId, Guid.Parse("6ed76f26-52f2-4350-b0b3-103e370f0835"), 3);
            var triceps = await GetLeastPlayed(userId, Guid.Parse("f19a5492-81ea-4ebc-8b03-1198e8440a58"), 3);
            string reps = "12  * 12 * 10";
            List<Workout> chest = new List<Workout>();
            chest = chest.Concat(upper_chest).ToList();
            chest = chest.Concat(lower_chest).ToList();
            List<Workout> back = new List<Workout>();
            back = lats.Concat(middle_back).ToList();
            back = back.Concat(lower_back).ToList();
            List<Workout> shoulder = new List<Workout>();
            shoulder = shoulder.Concat(shoulder_Top).ToList();
            shoulder = shoulder.Concat(shoulder_front).ToList();
            shoulder = shoulder.Concat(shoulder_Back).ToList();
            List<Workout> arms = new List<Workout>();
            arms = arms.Concat(biceps).ToList();
            arms = arms.Concat(triceps).ToList();
            arms = arms.Concat(forearms).ToList();
            List<Workout> legs = new List<Workout>();
            legs = legs.Concat(Quadriceps).ToList();
            legs = legs.Concat(Hamstrings).ToList();
            legs = legs.Concat(Gluteus).ToList();
            legs = legs.Concat(calves1).ToList();
            legs = legs.Concat(calves2).ToList();
            List<CustomWorkoutDTO> customWorkout = new List<CustomWorkoutDTO>();

            int order = 1;
            foreach (var item in chest)
            {
                customWorkout.Add(new CustomWorkoutDTO { WorkoutId = item.Id, WorkoutName = item.Name, DayNumber = 1, Order = order, Reps = reps });
                order++;
            }
            order = 1;
            foreach (var item in back)
            {
                customWorkout.Add(new CustomWorkoutDTO { WorkoutId = item.Id, WorkoutName = item.Name, DayNumber = 2, Order = order, Reps = reps });
                order++;

            }
            order = 1;
            foreach (var item in shoulder)
            {
                customWorkout.Add(new CustomWorkoutDTO { WorkoutId = item.Id, WorkoutName = item.Name, DayNumber = 3, Order = order, Reps = reps });
                order++;
            }
            order = 1;
            foreach (var item in arms)
            {
                customWorkout.Add(new CustomWorkoutDTO { WorkoutId = item.Id, WorkoutName = item.Name, DayNumber = 4, Order = order, Reps = reps });
                order++;

            }
            order = 1;
            foreach (var item in legs)
            {
                customWorkout.Add(new CustomWorkoutDTO { WorkoutId = item.Id, WorkoutName = item.Name, DayNumber = 5, Order = order, Reps = reps });
                order++;
            }
            return customWorkout;
        }

        public async Task<List<CustomWorkoutDTO>> CreateUpperLowerPlan(string userId)
        {
            string reps = "12  * 12 * 10";
            var shoulder_Top = await GetLeastPlayed(userId, Guid.Parse("df475ba8-6be7-42b6-871f-6a29cd4c91d8"), 2);
            var shoulder_front = await GetLeastPlayed(userId, Guid.Parse("d729e9f8-3384-4873-83bc-74acdccacabe"), 1);
            var shoulder_Back = await GetLeastPlayed(userId, Guid.Parse("c4ac8999-40a4-40c8-9518-42be7e2d0744"), 3);
            var upper_chest = await GetLeastPlayed(userId, Guid.Parse("8909c9d4-2f45-4a2f-90c8-d6f475820a2f"), 2);
            var lower_chest = await GetLeastPlayed(userId, Guid.Parse("d8738439-5832-4764-8290-20ebe48d50dc"), 2);
            var lats = await GetLeastPlayed(userId, Guid.Parse("a2528d27-0dfe-48c8-9a18-7887dd9743ef"), 2);
            var middle_back = await GetLeastPlayed(userId, Guid.Parse("405069d0-6e66-4d50-883d-e5b29a403a84"), 2);
            var lower_back = await GetLeastPlayed(userId, Guid.Parse("b61c068e-8e8b-4756-a05c-482d0ec70f9e"), 2);
            var Quadriceps = await GetLeastPlayed(userId, Guid.Parse("b391d0e8-442e-414a-85cc-9445b3bd11be"), 2);
            var Hamstrings = await GetLeastPlayed(userId, Guid.Parse("2c6891f3-98d9-4bfe-bf90-a7e5b6b1caf8"), 2);
            var Gluteus = await GetLeastPlayed(userId, Guid.Parse("619fe532-8226-4d84-9a61-b64bcdef8084"), 2);
            var calves1 = await GetLeastPlayed(userId, Guid.Parse("e56d075d-e1db-49d0-be1c-a925b80e87a6"), 1);
            var calves2 = await GetLeastPlayed(userId, Guid.Parse("cb5afad8-c1ae-44e5-ac7b-aa992f5c80a7"), 1);
            var biceps = await GetLeastPlayed(userId, Guid.Parse("6ed76f26-52f2-4350-b0b3-103e370f0835"), 2);
            var triceps = await GetLeastPlayed(userId, Guid.Parse("f19a5492-81ea-4ebc-8b03-1198e8440a58"), 2);

            List<CustomWorkoutDTO> customWorkout = new List<CustomWorkoutDTO>();
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = upper_chest[0].Id, WorkoutName = upper_chest[0].Name, DayNumber = 1, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = lower_chest[0].Id, WorkoutName = lower_chest[0].Name, DayNumber = 1, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = lats[0].Id, WorkoutName = lats[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = middle_back[0].Id, WorkoutName = middle_back[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = shoulder_Top[0].Id, WorkoutName = shoulder_Top[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = shoulder_front[0].Id, WorkoutName = shoulder_front[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = biceps[0].Id, WorkoutName = biceps[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = triceps[0].Id, WorkoutName = triceps[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Quadriceps[0].Id, WorkoutName = Quadriceps[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Quadriceps[1].Id, WorkoutName = Quadriceps[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Hamstrings[0].Id, WorkoutName = Hamstrings[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Hamstrings[1].Id, WorkoutName = Hamstrings[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Gluteus[0].Id, WorkoutName = Gluteus[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Gluteus[1].Id, WorkoutName = Gluteus[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = lower_back[0].Id, WorkoutName = lower_back[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = calves1[0].Id, WorkoutName = calves1[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = calves2[0].Id, WorkoutName = calves2[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = upper_chest[1].Id, WorkoutName = upper_chest[1].Name, DayNumber = 1, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = lower_chest[1].Id, WorkoutName = lower_chest[1].Name, DayNumber = 1, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = lats[1].Id, WorkoutName = lats[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = middle_back[1].Id, WorkoutName = middle_back[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = shoulder_Top[1].Id, WorkoutName = shoulder_Top[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = shoulder_Back[0].Id, WorkoutName = shoulder_Back[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = biceps[1].Id, WorkoutName = biceps[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = triceps[1].Id, WorkoutName = triceps[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Quadriceps[0].Id, WorkoutName = Quadriceps[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Quadriceps[1].Id, WorkoutName = Quadriceps[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Hamstrings[0].Id, WorkoutName = Hamstrings[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Hamstrings[1].Id, WorkoutName = Hamstrings[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Gluteus[0].Id, WorkoutName = Gluteus[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = Gluteus[1].Id, WorkoutName = Gluteus[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = lower_back[1].Id, WorkoutName = lower_back[1].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = calves1[0].Id, WorkoutName = calves1[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            customWorkout.Add(new CustomWorkoutDTO { WorkoutId = calves2[0].Id, WorkoutName = calves2[0].Name, DayNumber = 4, Order = 1, Reps = reps });
            return customWorkout;
        }

        private async Task<List<Workout>> GetLeastPlayed(string userId, Guid muscleId, int itemsNumber)
        {
            var leastPlayedWorkouts = await _context.Workouts
                .Where(w => w.MainMuscleId == muscleId)
                .GroupJoin(
                    _context.UserWorkouts.Where(uw => uw.UserId == userId),
                    workout => workout.Id,
                    userWorkout => userWorkout.WorkoutId,
                    (workout, userWorkouts) => new
                    {
                        Workout = workout,
                        TimesPerformed = userWorkouts.Any() ? userWorkouts.Sum(uw => uw.TimesPerformed) : 0
                    }
                )
                .OrderBy(w => w.TimesPerformed)
                .Take(itemsNumber)
                .Select(w => w.Workout)
                .ToListAsync();
            return leastPlayedWorkouts;
        }
    }
}