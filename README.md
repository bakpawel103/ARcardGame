# Praca inżynierska:
*AR Foundation in Unity + Android/iOS + Photon Networking System*
*Gra planszowa/karciana multiplayer LAN/online.*

### Flow:
- [x] Gracz w lobby przeszukuje istniejące pokoje i dołącza do któregoś z nich, bądz tworzy własny 4-ro osobowy pokój
- [ ] Gracz poproszony jest o zeskanowanie obrazka, który będzie punktem zerowym przy trackowaniu
- [ ] Pozostali gracze mogą dołączyć do gry, wyszukując podany pokój w lobby
- [ ] Gracz widzi wizualizację kart wszystkich przeciwników. Jest w stanie położyć kartę na stole i jest to wizualizowane dla każdego gracza znajdującego się w tej grze.
- [ ] Jeśli jest to gra planszowa, to jest w stanie rzucić kostką sześcienną (lub jakąś inną- w zależności od rodzaju gry).
- [ ] Zaimplementowane reguły gry.


### News:
##### *16.07.2020*
Projekt:
- Przejście z TCPClient i TCPListener na framework Photon
- Pierwsza scena, to scena z lobby, w którym możemy przeszukać istniejące pokoje i dołączyć do jednego z nich, bądź stworzyć własny
- Po dołączeniu do pokoju, przechodzimy do sceny z grą, gdzie od razu zostaniemy poproszeni o zeskanowanie obrazka (Na początek ikonka Politechniki Łódzkiej)

Lobby:
- Przebudowany system lobby z GUI do UI, który pozwala na sprawniejszą i wydajniejszą responsywność widoku oraz wydajniejsze pisanie kodu


##### *17.07.2020*
Lobby:
- Przycisk Create Room dostępny, gdy uzupełniono polę Player Name oraz Room Name
- Przycisk Join Room dostępny, gdy uzupełniono polę Player Name

Gra:
- Dodany przycisk wychodzenia z pokoju
- Dodany panel, w którym wyświetlana jest liczebność pokoju, w którym jest gracz
- Dodany obrazek zachęty do zeskanowania zdjęcia, który będzie punktem zerowym trackowania
- Obrazek zachęty znika, gdy zdjęcie zostało zeskanowane


![Lobby image](https://github.com/bakpawel103/ARcardGame/blob/Networking/images/Lobby.png)


| Użyta technologia | URL |
| ------ | ------ |
| Photon | https://www.photonengine.com/ |
| Unity | https://unity.com/ |
| AR Foundation |  |