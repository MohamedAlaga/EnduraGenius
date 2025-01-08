namespace EnduraGenius.API.Models.Domain
{
    public class Plan
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descreption { get; set; }
        public bool IsPublic { get; set; } = false;
        public string PlanCreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // navigation properties
        public User planCreator { get; set; }
    }
}
