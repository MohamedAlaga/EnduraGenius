namespace EnduraGenius.API.Models.DTO
{
    public class UpdatePlanRequestDTO
    {
        public string? PlanName { get; set; }
        public string? PlanDescription { get; set; }
        public bool? IsPublic { get; set; }
    }
}
