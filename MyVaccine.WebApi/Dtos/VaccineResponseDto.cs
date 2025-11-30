namespace MyVaccine.WebApi.Dtos
{
    public class VaccineResponseDto
    {
        public int VaccineId { get; set; }
        public string Name { get; set; }
        public bool RequiresBooster { get; set; }
        public List<string> CategoryNames { get; set; }
    }
}
