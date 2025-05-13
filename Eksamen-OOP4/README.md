# OOP4 Eksamen - Søker/Stilling Matchmaking

Dette prosjektet består av to oppgaver for OOP4-eksamen. Her følger en beskrivelse av løsningene.

## Question 2 - API Interaksjon og Søker/Stilling Matching

Dette er en konsollapplikasjon som henter data fra et eksternt API, deseraliserer det til objektorienterte klasser, og foreslår matching mellom jobbsøkere og ledige stillinger basert på ulike kriterier.

### Teknikker og ferdigheter brukt

I Question 2 demonstrerer jeg flere sentrale konsepter fra objektorientert programmering. Løsningen bruker asynkrone API-kall med HttpClient for å hente data, samt System.Text.Json for deserialisering fra JSON til egendefinerte modellklasser. Matchalgoritmene benytter LINQ for effektiv filtrering og sortering etter flere kriterier (tittel, så seniority, deretter spesialisering og ferdigheter). For å opprette en brukervennlig interaksjonsmulighet anvendes konsoll-UI med tastaturnavigering, samt fargeformatering for å tydeliggjøre resultater. Løsningen er basert på separasjon av ansvar gjennom deling i Services, Models og UI-komponenter.

## Question 1 - ASCII og Luhn Algoritme

En konsollapplikasjon som konverterer en tekststreng (f.eks. et navn) til ASCII-verdier og deretter beregner en sjekksiffer basert på Luhn-algoritmen. Programmet viser konverteringen steg-for-steg og legger til slutt til sjekksifferet for å lage et endelig nummer.

### Teknikker og ferdigheter brukt

I Question 1 demonstrerer jeg grunnleggende C#-programmering med fokus på konvertering mellom datatyper og algoritmisk tenkning. Løsningen bruker eksplisitt typede variabler og viser bruk av ulike kontrollstrukturer som if-setninger og for-løkker. Jeg bruker objektorientert programmering med separate klasser for å skille ansvarsområder (en for ASCII-konvertering og en for Luhn-algoritmen). Programmet inkluderer god feilhåndtering for ugyldig input og viser informativ output til brukeren med detaljert beskrivelse av hvert steg i prosessen.

## Oppsett og bruk

1. Åpne løsningen i Visual Studio eller annet .NET-kompatibelt utviklingsmiljø
2. Bygg løsningen med .NET 6.0 eller nyere
3. Kjør Question-2 prosjektet med `dotnet run` fra Question-2 mappen eller direkte fra utviklingsmiljøet
4. Bruk piltastene for å navigere i menyen og Enter for å velge

### Forutsetninger
- .NET 6.0 SDK eller nyere
- Internettilkobling for API-kommunikasjon

## Prosjektstruktur

Prosjektet er organisert i følgende moduler:

- **Models/**: Inneholder klassene som representerer datamodellene (Applicant, Position, ExamData)
- **Services/**: Inneholder tjenester for API-kommunikasjon og matching-algoritmer
- **UI/**: Inneholder klasser for presentasjon og brukerinteraksjon

## API-informasjon

API-endepunktet som brukes er:
```
exam.05093218.nip.io/api/ExamTask
```

API-nøkkelen som kreves er inkludert i koden.

## AI-bruk

[Her kan du legge til informasjon om eventuell bruk av AI-assistenter i prosjektet, hvis aktuelt]
