using WebApi.Enums;

namespace WebApi.Entities
{
    public class Race
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PetType PetType { get; set; }
    }
}