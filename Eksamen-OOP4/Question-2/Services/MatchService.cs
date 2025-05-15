using Question_2.Models;

namespace Question_2.Services;

/// <summary>
/// Tjenesteklasse for matching av søkere med stillingsposisjoner.
/// Implementerer logikk for å finne den beste stillingen for hver søker basert på ønskede kriterier.
/// </summary>
public class MatchService
{
    /// <summary>
    /// Matcher søkere med stillingsposisjoner basert på ulike kriterier.
    /// </summary>
    /// <param name="applicants">Liste over søkere som skal matches.</param>
    /// <param name="positions">Liste over tilgjengelige stillingsposisjoner.</param>
    /// <returns>Liste med tupler av søkere og deres matchede stillingsposisjoner.</returns>
    /// <remarks>
    /// Matchingprosessen følger en prioritert rekkefølge:
    /// 1. Filtrerer stillinger med samme tittel (obligatorisk match)
    /// 2. Filtrerer videre på senioritetsnivå
    /// 3. Rangerer gjenværende kandidater basert på skill-overlap og spesialisering
    /// </remarks>
    public List<(Applicant Applicant, Position MatchedPosition)> MatchApplicantsToPositions(List<Applicant> applicants, List<Position> positions)
    {
        List<(Applicant, Position)> matches = new();

        foreach (var applicant in applicants)
        {
            Position desired = applicant.DesireedPosition;

            // Finner kandidater med samme tittel
            List<Position> titleMatches = positions
                .Where(p => p.Title.Equals(desired.Title, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Filtrer på seniority
            List<Position> seniorityMatches = titleMatches
                .Where(p => p.Seniority.Equals(desired.Seniority, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Basert på overlap av ferdigheter og evt. spesialisering
            Position? bestMatch = seniorityMatches
                .OrderByDescending(p => ScoreMatch(p, desired))
                .FirstOrDefault();

            if (bestMatch != null)
            {
                matches.Add((applicant, bestMatch));
            }
        }

        return matches;
    }

    /// <summary>
    /// Beregner en score for hvor godt en stillingsposisjon matcher med en ønsket posisjon.
    /// </summary>
    /// <param name="position">Eksisterende stillingsposisjon som evalueres.</param>
    /// <param name="desired">Ønsket stillingsposisjon fra søkeren.</param>
    /// <returns>Numerisk score som indikerer hvor godt posisjonene matcher.</returns>
    /// <remarks>
    /// Scoringsalgoritmen gir:
    /// - 2 poeng for match på spesialisering
    /// - 1 poeng for hver overlappende ferdighet
    /// </remarks>
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