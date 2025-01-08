using Microsoft.AspNetCore.Identity;

namespace EnduraGenius.API.Models.Domain
{
    public class User : IdentityUser
    {
        public int Points { get; set; } = 0;
        public float WeightInKg { get; set; }
        public int TallInCm { get; set; }
        public int Age { get; set; }
        public bool IsMale { get; set; }

    }
}
