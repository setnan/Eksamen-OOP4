# OOP4 Eksamen - Søker/Stilling Matchmaking

Dette prosjektet består av to oppgaver for OOP4-eksamen. Her følger en beskrivelse av løsningene.

## Question 2 - API Interaksjon og Søker/Stilling Matching

Dette er en konsollapplikasjon som henter data fra et eksternt API, deseraliserer det til objektorienterte klasser, og foreslår matching mellom jobbsøkere og ledige stillinger basert på ulike kriterier.

### Teknikker og ferdigheter brukt

I Question 2 demonstrerer jeg flere sentrale konsepter fra objektorientert programmering. Løsningen bruker asynkrone API-kall med HttpClient for å hente data, samt System.Text.Json for deserialisering fra JSON til egendefinerte modellklasser. Matchalgoritmene benytter LINQ for effektiv filtrering og sortering etter flere kriterier (tittel, så seniority, deretter spesialisering og ferdigheter). For å opprette en brukervennlig interaksjonsmulighet anvendes konsoll-UI med tastaturnavigering, samt fargeformatering for å tydeliggjøre resultater. Løsningen er basert på separasjon av ansvar gjennom deling i Services, Models og UI-komponenter.

## Question 1 - ASCII-konvertering og Luhn-algoritmen

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

### Forutsetninger

- .NET 6.0 SDK eller nyere
  - Last ned fra: https://dotnet.microsoft.com/download
  - Verifiser installasjonen ved å kjøre `dotnet --version` i kommandolinjen
- Utviklingsmiljø (et av følgende):
  - Visual Studio 2022 eller nyere
  - Visual Studio Code med C# Extension
  - JetBrains Rider
- Internettilkobling for Question-2 (API-kommunikasjon)

### Installasjon

#### Klone eller last ned prosjektet
```
git clone <repository-url>
# Eller last ned og pakk ut ZIP-filen fra lö
cd Eksamen-OOP4
```

#### Installere avhengigheter

```bash
# For Question-1 (installerer Spectre.Console)
cd Question-1
dotnet restore

# For Question-2
cd ../Question-2
dotnet restore
```

### Bygge prosjektene

```bash
# Bygg Question-1
cd Question-1
dotnet build

# Bygg Question-2
cd ../Question-2
dotnet build
```

### Kjøre applikasjonene

#### Question-1 (ASCII og Luhn-algoritmen)

```bash
cd Question-1
dotnet run
```

Etter oppstart:
1. Skriv inn et ord eller navn når du blir bedt om det
2. Se den visuelle fremstillingen av ASCII-konverteringen

#### Question-2 (API Interaksjon og Matching)

```bash
cd Question-2
dotnet run
```

##### Bruk av miljøvariabler for API-konfigurasjon

Løsningen støtter bruk av miljøvariabler for API-konfigurasjon, som er foretrukket fremfor hardkodede verdier. For å sette miljøvariabler i PowerShell:

```powershell
# Setter miljøvariabler for API-konfigurasjon
$env:EXAM_API_KEY="b569e4f6-77c2-475a-bf77-d15e81dd4dbd"
$env:EXAM_API_URL="exam.05093218.nip.io/api/ExamTask"

# Kjør applikasjonen etter å ha satt miljøvariabler
dotnet run
```

For Command Prompt:
```cmd
SET EXAM_API_KEY=b569e4f6-77c2-475a-bf77-d15e81dd4dbd
SET EXAM_API_URL=exam.05093218.nip.io/api/ExamTask
dotnet run
```

Hvis miljøvariabler ikke er satt, vil applikasjonen bruke standardverdiene fra appsettings.json.

## Preferert operativsystem
Denne løsningen er utviklet og testet på Windows. Jeg foretrekker at den vurderes på Windows-plattformen.

## Bruk av AI
I utviklingen av denne oppgaven har jeg benyttet følgende AI-verktøy:

- **OpenAI's ChatGPT**: Brukt for å forbedre UI med Spectre.Console, refaktorere metodenavn fra norsk til engelsk, og generelle forbedringer i brukergrensesnittet.

[Her kan du legge til mer spesifikk informasjon om hvilke deler av koden som ble påvirket av AI-assistanse, eller angi at "Jeg har ikke brukt AI for denne oppgaven" hvis det er tilfelle]
3. Se resultatet av Luhn-algoritmen med sjekksifferet
4. Skriv 'Q' for å avslutte programmet

#### Question-2 (API og matching-system)

```bash
cd Question-2
dotnet run
```

Etter oppstart:
1. Applikasjonen vil automatisk hente data fra API-et
2. Bruk piltastene (↑↓) for å navigere i menyen
3. Trykk Enter for å velge et menyvalg
4. Bruk menyalternativene for å utforske matchinger mellom søkere og stillinger

### Problemer med API-tilkobling?

Hvis du opplever problemer med API-tilkoblingen i Question-2:
1. Sjekk at internettforbindelsen din er aktiv
2. Verifiser at API-nøkkelen i `appsettings.json` er gyldig
3. Kontroller at API-endepunktet er tilgjengelig

## Prosjektstruktur

Prosjektet er organisert i følgende moduler:

- **Models/**: Inneholder klassene som representerer datamodellene (Applicant, Position, ExamData)
- **Services/**: Inneholder tjenester for API-kommunikasjon og matching-algoritmer
- **UI/**: Inneholder klasser for presentasjon og brukerinteraksjon
- **Configuration/**: Inneholder konfigurasjonsklasser for å laste innstillinger fra appsettings.json

### Visuell mappestruktur

```
Eksamen-OOP4/
├── Eksamen-OOP4.sln         # Løsningsfil som inneholder begge prosjekter
│
├── README.md                # Denne dokumentasjonsfilen
│
├── Question-1/              # Delprosjekt 1: ASCII og Luhn-algoritme
│   ├── Configuration/
│   │   └── AppConfig.cs     # Konfigurasjonshåndtering
│   ├── UI/
│   │   └── UIHelper.cs      # UI-abstraksjon
│   ├── AsciiConverter.cs    # Konvertering fra tekst til ASCII
│   ├── LuhnCalculator.cs    # Implementasjon av Luhn-algoritmen
│   ├── Program.cs           # Applikasjonens startpunkt
│   ├── Question-1.csproj    # Prosjektfil med avhengigheter
│   └── appsettings.json     # Konfigurasjonsfil
│
└── Question-2/              # Delprosjekt 2: API og matching-system
    ├── Configuration/
    │   └── AppConfig.cs     # Konfigurasjonshåndtering for API-innstillinger
    ├── Models/
    │   ├── Applicant.cs     # Modell for jobbsøkere
    │   ├── Position.cs      # Modell for stillinger
    │   └── ExamData.cs      # Container-modell for API-responsen
    ├── Services/
    │   ├── ExamTaskService.cs # API-kommunikasjon
    │   └── MatchService.cs  # Matching-algoritme
    ├── UI/
    │   └── Menu.cs          # Menystruktur og brukerinteraksjon
    ├── Program.cs           # Applikasjonens startpunkt
    ├── Question-2.csproj    # Prosjektfil med avhengigheter
    └── appsettings.json     # Konfigurasjonsfil for API-detaljer
```

Denne strukturen følger prinsippet om separasjon av ansvar og modularitet, hvor hver komponent har et tydelig avgrenset ansvarsområde.

## API-informasjon

API-endepunktet som brukes er:
```
exam.05093218.nip.io/api/ExamTask
```

API-nøkkelen som kreves er inkludert i koden.

## AI-bruk

I dette prosjektet har jeg benyttet AI-assistanse på følgende måter:

- **Strukturell veiledning**: Cascade AI ble brukt for å få forslag til arkitektur og modularisering
- **Kodegjennomgang**: AI-assistenten hjalp med å identifisere potensielle problemer som nullreferanser og forbedringsmuligheter
- **Implementasjon av konfigurasjonssystem**: Fikk assistanse for å implementere konfigurasjonssystem i begge delprosjekter
- **Dokumentasjonsforbedringer**: README-filen ble utarbeidet med hjelp fra AI
- 
Alle AI-genererte forslag er manuelt vurdert, og kun struktur og teknikk er brukt der det styrker kvaliteten i henhold til eksamenskravene.