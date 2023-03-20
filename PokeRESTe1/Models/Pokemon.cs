namespace PokemonAPI.Models
{
    public class Pokemon
    {
        public int Id { get; set; } // ikke null
        public string? Name { get; set; } // ikke null, minimum længde 2
        public int Level { get; set; } // 1-99
        public int PokeDex { get; set; } // positivt > 0

        public void ValidateName()
        {
            if (Name == null) throw new ArgumentNullException("name is not allowed to be null");
            if (Name.Length < 2) throw new ArgumentException("Name must be at least 2 characters long");
        }

        public void ValidateLevel()
        {
            if (Level < 1 || Level > 99) throw new ArgumentOutOfRangeException("Level must be between 1 and 99");
        }

        public void ValidatePokeDex()
        {
            if (PokeDex < 0) throw new ArgumentOutOfRangeException("PokeDex must be a positive number");
        }

        public void Validate()
        {
            ValidateName();
            ValidateLevel();
            ValidatePokeDex();
        }
    }
}
