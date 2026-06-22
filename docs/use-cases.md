# Use Cases — BookingSystem

Dette dokument beskriver systemets use cases i et let format (inspireret af Larmans "use case brief"-stil), organiseret som korte, sammenhængende beskrivelser frem for punktopstillede trin.

## Kerne use cases (MVP)

### Når et medlem vil se ledige tider for en ressource

Medlemmet vælger en ressource og en dato, og systemet viser tidsslots markeret som ledige eller optagne, baseret på ressourcens åbningstider. Optagne slots viser kun status "optaget" uden information om hvem der har booket, med undtagelse af brugerens egne bookinger, som vises med fuld detalje.

### Når et medlem opretter en booking

Medlemmet vælger en ressource samt et start- og sluttidspunkt. Systemet validerer at tiden ligger i fremtiden, falder inden for ressourcens åbningstider, og ikke overlapper en eksisterende aktiv booking på samme ressource. Hvis alle betingelser er opfyldt, oprettes bookingen med status Active; ellers afvises forespørgslen med en relevant fejl.

### Når et medlem annullerer en booking

Medlemmet vælger en af sine egne aktive bookinger og annullerer den. Systemet sætter bookingens status til Cancelled, hvorved tidspunktet frigives til andre. Et medlem kan kun annullere sine egne bookinger.

### Når en administrator opretter eller redigerer en ressource

Administratoren angiver navn, beskrivelse og åbningstider for en ressource, enten ved oprettelse eller redigering af en eksisterende. Kun brugere med administratorrollen har adgang til denne handling.

### Når en administrator administrerer bookinger

Administratoren ser den fulde liste af bookinger på tværs af alle brugere, inklusive hvem der står bag hver booking, og kan annullere en booking på en brugers vegne. Denne fulde indsigt er forbeholdt administratorrollen.

### Når en bruger registrerer sig og logger ind

En ny bruger registrerer sig med navn, email og adgangskode, hvorefter de kan logge ind og modtage en JWT-token til efterfølgende autentificering. Emailadressen skal være unik, og adgangskoden gemmes som et hash, aldrig i klartekst.

## Stretch use cases (kun hvis tid tillader det)

### Når et medlem opretter en gentagende booking

Medlemmet angiver et mønster, fx en ugentlig tid over en given periode, og systemet opretter de tilhørende individuelle bookinger, hver underlagt samme valideringsregler som en enkeltstående booking.

### Når en booking oprettes eller annulleres

Systemet sender en email-notifikation til den relevante bruger som bekræftelse på handlingen.

### Når en ressource er fuldt booket på et ønsket tidspunkt

Medlemmet kan tilmelde sig en venteliste for den pågældende tid og notificeres, hvis tidspunktet senere frigives.