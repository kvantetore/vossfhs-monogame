Alternative kontroller
-----------------------
  - Direkte styring av rotasjon, indirekte av hastighet

Vegger
------
  - Alternativ 1: Wrapping - ut på ene siden, inn på andre
    - Sjekke rocketPosition i forhold til screen size og flytte raketten til motsatt side
  - Alternativ 2: Vegger med "sprett"
    - Sjekke rocketPosition i forhold til screen size og endre rocketVelocity 
      - trenger trolig også litt flytting for å komme seg "ut" av veggen
  - Alternativ 3: Wrapping i X, Sprett i Y

"Luftmotstand"
--------------
    - Alternativ 1: Sette Maksfart
    - Alternativ 2: redusere farten hver update

2 player
-----------
  - tegne en rakett til 
  - styre med rakett2 med wsad i stedet for piler

Astroide
--------
  - tegne en astroide 
  - sjekke kollisjon

Skyte
------
  - tegne kule
  - sjekke kollisjon med astroide
  - Dele astroiden i to mindre astroider