# Domænemodel — BookingSystem

Dette dokument beskriver de centrale entiteter i systemet, deres felter, og relationerne mellem dem.

## Entiteter

### User

Repræsenterer en bruger af systemet — enten et almindeligt medlem eller en administrator.

| Felt | Type | Beskrivelse |
|---|---|---|
| Id | Guid | Unik identifikator |
| Name | string | Brugerens navn |
| Email | string | Unik emailadresse, bruges til login |
| PasswordHash | string | Hashet adgangskode, aldrig gemt i klartekst |
| Role | UserRole | Member eller Admin |

### Resource

Repræsenterer den bookbare ressource — fx et mødelokale eller et stykke udstyr. Systemet bruger én generisk ressourcetype frem for flere underklasser, da det dækker behovet uden unødig kompleksitet.

| Felt | Type | Beskrivelse |
|---|---|---|
| Id | Guid | Unik identifikator |
| Name | string | Ressourcens navn |
| Description | string | Kort beskrivelse |
| OpeningTime | TimeOnly | Tidspunkt ressourcen åbner for booking, dagligt |
| ClosingTime | TimeOnly | Tidspunkt ressourcen lukker for booking, dagligt |

### Booking

Repræsenterer en konkret reservation af en ressource i et givent tidsrum.

| Felt | Type | Beskrivelse |
|---|---|---|
| Id | Guid | Unik identifikator |
| ResourceId | Guid (FK) | Reference til den bookede ressource |
| UserId | Guid (FK) | Reference til brugeren der har booket |
| StartTime | DateTime | Starttidspunkt for bookingen |
| EndTime | DateTime | Sluttidspunkt for bookingen |
| Status | BookingStatus | Active eller Cancelled |
| CreatedAt | DateTime | Tidspunkt bookingen blev oprettet |

## Enums

### UserRole
- `Member` — kan oprette og annullere egne bookinger, se ledige tider
- `Admin` — kan administrere ressourcer og se/administrere alle bookinger

### BookingStatus
- `Active` — bookingen er gyldig og optager tiden
- `Cancelled` — bookingen er annulleret, tiden er frigivet

## Relationer

- En **User** kan have mange **Booking**-poster (1-til-mange), men relationen er ensrettet: `Booking` refererer til `User` via `UserId`, der findes ingen navigerbar `User.Bookings`-liste i modellen. Dette er et bevidst valg for at holde modellen simpel, indtil et konkret behov for navigation opstår.
- En **Resource** kan have mange **Booking**-poster (1-til-mange), på samme ensrettede måde via `Booking.ResourceId`.
- En **Booking** hører altid til præcis én **User** og én **Resource**.

## Bevidste designvalg i modellen

- **Synlighed af bookinger:** Almindelige medlemmer kan se hvornår en ressource er optaget, men ikke hvem der har booket den — med undtagelse af deres egne bookinger. Dette håndteres ikke i selve domænemodellen (Booking-entiteten har stadig et UserId-felt), men i hvilke data der eksponeres til hvilken rolle via separate DTO'er i API-laget (se architecture.md).
- **Én generisk Resource-type:** Systemet skelner ikke mellem "lokale" og "udstyr" som separate klasser. Hvis behovet opstår, kan et `Category`-felt tilføjes uden at ændre kernemodellen.
- **Åbningstider som globalt dagligt interval:** OpeningTime/ClosingTime gælder alle dage. Differentierede åbningstider pr. ugedag er identificeret som en mulig udvidelse, men er udeladt fra MVP for at holde valideringslogikken simpel.