# Praca inżynierska:
*AR Foundation in Unity + Android/iOS + Photon Networking System*
*Gra planszowa/karciana multiplayer LAN/online.*

### Flow:
- [x] Gracz w lobby przeszukuje istniejące pokoje i dołącza do któregoś z nich, bądz tworzy własny 4-ro osobowy pokój
- [ ] Gracz hostujący grę, skanuje na smartfonie przestrzeń, następnie proszony jest o wybranie miejsca, gdzie zostaną umieszczone karty/plansza do gry.
- [ ] Pozostali gracze mogą dołączyć do gry, wyszukując podany pokój w lobby
- [ ] Gracz widzi wizualizację kart wszystkich przeciwników. Jest w stanie położyć kartę na stole i jest to wizualizowane dla każdego gracza znajdującego się w tej grze.
- [ ] Jeśli jest to gra planszowa, to jest w stanie rzucić kostką sześcienną (lub jakąś inną- w zależności od rodzaju gry).
- [ ] Zaimplementowane reguły gry.


### News:
- Przejście z TCPClient i TCPListener na framework Photon
- Pierwsza scena, to scena z lobby, w którym możemy przeszukać istniejące pokoje i dołączyć do jednego z nich, bądź stworzyć własny
- Po dołączeniu do pokoju, przechodzimy do sceny z grą, gdzie od razu zostaniemy poproszeni o zeskanowanie obrazka (Na początek ikonka Politechniki Łódzkiej)


![Lobby image](https://github.com/bakpawel103/ARcardGame/blob/Networking/images/Lobby.png)


| Użyta technologia | URL |
| ------ | ------ |
| Photon | https://www.photonengine.com/ |