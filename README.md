## Praca inżynierska:
*AR Foundation in Unity + Android/iOS*
*Gra planszowa/karciana multiplayer LAN/online.*

*(Konspekt początkowy, jeszcze dużo może się zmienić z powodu na przykład braku możliwości zaimplementowania którejś
z funkcjonalności, spowodowane samą technologią AR Foundation. Jestem w trakcie researchu w wolnym czasie, jak można
wykorzystać różne feature'y AR Foundation do pracy).*
- [x] Gracz wybiera w menu, czy chce utworzyć nowy pokój do gry, czy chce dołączyć do istniejącego pokoju
- [ ] Gracz hostujący grę, skanuje na smartfonie przestrzeń, następnie proszony jest o wybranie miejsca, gdzie zostaną umieszczone karty/plansza do gry.
- [ ] Pozostali gracze mogą dołączyć do gry poprzez wpisanie unikatowego kodu lub zeskanowanie automatyczniewygenerowanego QRCode przez hosta.
- [ ] Gracz widzi wizualizację kart wszystkich przeciwników. Jest w stanie położyć kartę na stole i jest to wizualizowane dla każdego gracza znajdującego się w tej grze.
- [ ] Jeśli jest to gra planszowa, to jest w stanie rzucić kostką sześcienną (lub jakąś inną- w zależności od rodzaju gry).
- [ ] Zaimplementowane reguły gry.



Zrobione:
- [x] Zaimplementowany model tcp Client-Server:
	- po uruchomieniu aplikacji użytkownik wybiera pomiędzy hostowaniem nowego pokoju, a połączeniem z istniejącym pokojem;
	- po wybraniu Host, tworzony jest nowy server i client i w prawym górnym rogu wyświetlają się informacje potrzebne podczas dołączania do pokoju
	przez pozostałych graczy
	- po wybraniu Join, wyświetla się formularz, w którym należy uzupełnić address ip oraz port, który wyświetlany jest na aplikacji hostującej
	- w stworzonym panelu developerskim wyświetlając się informacje o przesyłanych i odbieranych danych;