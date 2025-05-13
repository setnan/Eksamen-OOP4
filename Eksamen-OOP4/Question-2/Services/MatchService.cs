using Question_2.Models;

namespace Question_2.Services;

public class MatchService
{
    public List<(Applicant Applicant, Position MatchedPosition)> MatchApplicantsToPositions(List<Applicant> applicants, List<Position> positions)
    {
        List<(Applicant, Position)> matches = new();

        foreach (var applicant in applicants)
        {
            var desired = applicant.DesireedPosition;

            // Finn kandidater med samme tittel (obligatorisk)
            var titleMatches = positions
                .Where(p => p.Title.Equals(desired.Title, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Deretter filtrer på seniority
            var seniorityMatches = titleMatches
                .Where(p => p.Seniority.Equals(desired.Seniority, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Nå rangerer vi basert på skill overlap og evt. specialization
            var bestMatch = seniorityMatches
                .OrderByDescending(p => ScoreMatch(p, desired))
                .FirstOrDefault();

            if (bestMatch != null)
            {
                matches.Add((applicant, bestMatch));
            }
        }

        return matches;
    }

    private int ScoreMatch(Position position, Position desired)
    {
        int score = 0;

        // Match på specialization
        if (position.Specialization.Equals(desired.Specialization, StringComparison.OrdinalIgnoreCase))
            score += 2;

        // Score for overlappende skills
        score += position.Skills
            .Count(skill => desired.Skills
                .Contains(skill, StringComparer.OrdinalIgnoreCase));

        return score;
    }
}