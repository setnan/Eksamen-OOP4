namespace Question_2.Models;

public class Position
{
    public required string Title { get; set; }
    public required string Specialization { get; set; }
    public required string Seniority { get; set; }
    public required List<string> Skills { get; set; }
}