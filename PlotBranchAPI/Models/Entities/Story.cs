namespace PlotBranchAPI.Models.Entities
{
    public class Story
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
