namespace ArbedeAPI.DTOs
{
    public class RaceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime SelectedAt { get; set; }
    }

    public class RaceCheckResponseDto
    {
        public bool HasRace { get; set; }
        public bool ShouldRedirectToRaceSelection { get; set; }
        public List<RaceDto> Races { get; set; }
    }
}
