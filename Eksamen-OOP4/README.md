# OOP4 Eksamen - Søker/Stilling Matchmaking

Dette prosjektet består av to oppgaver for OOP4-eksamen. Her følger en beskrivelse av løsningene.

## Question 2 - API Interaksjon og Søker/Stilling Matching

Dette er en konsollapplikasjon som henter data fra et eksternt API, deseraliserer det til objektorienterte klasser, og foreslår matching mellom jobbsøkere og ledige stillinger basert på ulike kriterier.

### Teknikker og ferdigheter brukt

I Question 2 demonstrerer jeg flere sentrale konsepter fra objektorientert programmering. Løsningen bruker asynkrone API-kall med HttpClient for å hente data, samt System.Text.Json for deserialisering fra JSON til egendefinerte modellklasser. Matchalgoritmene benytter LINQ for effektiv filtrering og sortering etter flere kriterier (tittel, så seniority, deretter spesialisering og ferdigheter). For å opprette en brukervennlig interaksjonsmulighet anvendes konsoll-UI med tastaturnavigering, samt fargeformatering for å tydeliggjøre resultater. Løsningen er basert på separasjon av ansvar gjennom deling i Services, Models og UI-komponenter.

## Question 1 - ASCII og Luhn Algoritme

En elegant konsollapplikasjon som konverterer en tekststreng (f.eks. et navn) til ASCII-verdier og deretter beregner en sjekksiffer basert på Luhn-algoritmen. Programmet viser konverteringen i en oversiktlig, fargerik tabell og gir brukeren umiddelbar visuell tilbakemelding på hvert steg i prosessen.

### Teknikker og ferdigheter brukt

I Question 1 demonstrerer jeg både grunnleggende C#-programmering og mer avanserte konsepter innen applikasjonsarkitektur og brukeropplevelse. Koden er strukturert i en modulbasert arkitektur med tydelig separasjon av ansvar:

#### Arkitektur og Moduler
* **Core Logic** - Separate klasser for applikasjonens kjernefunksjonalitet
  * `AsciiConverter`: Håndterer konvertering fra tekst til ASCII-verdier
  * `LuhnCalculator`: Implementerer Luhn-algoritmen for å beregne sjekksiffer

* **Konfigurasjonssystem** - Ekstern konfigurasjon for innstillinger
  * JSON-basert konfigurasjon via `appsettings.json`
  * Sterkt typede konfigurasjonsklasser i `Configuration`-mappen
  * Automatisk lasting og feilhåndtering av konfigurasjon

* **UI Abstraksjon** - Modulbasert UI-håndtering
  * `UIHelper`-klasse som kapsler inn alle UI-operasjoner
  * Konfigurerbare farger, tekster og meldinger via konfigurasjonsfilen
  * Gjenbrukbare UI-komponenter for hovedfunksjonaliteten

Løsningen benytter Spectre.Console-biblioteket for å skape et moderne og brukervennlig kommandolinjegrensesnitt med:
* Formatert tekst med konfigurerbare farger og stilarter
* Interaktive tabeller for datavisualisering
* Validerte brukerinput med håndtering av feilsituasjoner
* Konsistent visuell rytme og struktur

Denne løsningen viser ikke bare en implementasjon av Luhn-algoritmen, men også moderne applikasjonsarkitektur med konfigurerbarhet, UI-abstraksjon og modulbasert design.

## Oppsett og bruk

1. Åpne løsningen i Visual Studio eller annet .NET-kompatibelt utviklingsmiljø
2. Bygg løsningen med .NET 6.0 eller nyere
3. For å kjøre Question-1:
   - Naviger til Question-1 mappen
   - Kjør `dotnet run` eller start direkte fra utviklingsmiljøet
   - Skriv inn et ord/navn og se den visuelle konverteringen til ASCII med sjekksiffer
   - Skriv 'Q' for å avslutte programmet
4. For å kjøre Question-2:
   - Naviger til Question-2 mappen
   - Kjør `dotnet run` eller start direkte fra utviklingsmiljøet
   - Bruk piltastene for å navigere i menyen og Enter for å velge

### Forutsetninger
- .NET 6.0 SDK eller nyere
- NuGet-pakker:
  - Spectre.Console (for Question-1)
- Internettilkobling (for Question-2 API-kommunikasjon)

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
