namespace EnduraGenius.API.Models.Domain
{
    public class PlansUsers
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid PlanId { get; set; }
        public int PlanOrder { get; set; }

        // navigation properties
        public User User { get; set; }
        public Plan Plan { get; set; }
    }
}
