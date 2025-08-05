# M426_IM23A_LB-CasinoCode


## 🛠️ Features

### 🎰 Slot Machine
- Klassische Slot-Walzen mit Symbolen wie 🍒, 💎, 🔔 und 7️⃣
- Ein Spin kostet 10 Jetons
- Bei drei gleichen Symbolen erhält der Spieler eine Belohnung
- Dynamische Animation der Slot-Walzen
- Zufällige Gewinn-/Verlustkommentare für Immersion

### 🃏 Blackjack
- Spiele gegen 1–3 Computergegner
- Setze Jetons und versuche, 21 zu erreichen, ohne zu überkaufen
- Gegner (Dealer) ziehen Karten bis mindestens 17 Punkte
- A wird korrekt als 11 oder 1 gewertet
- Gewinn bringt den doppelten Einsatz, Unentschieden erstattet Einsatz

---

## 📦 Klassenübersicht

### `Card`
- Repräsentiert eine Spielkarte mit `Suit`, `Rank` und `Value`
- `ToString()` liefert ein schönes Kartensymbol (z. B. `K♥`)

### `Deck`
- Erzeugt ein gemischtes Kartendeck mit 52 Karten
- Methoden: `Shuffle()`, `DrawCard()`

### `Player`
- Besitzt Name, Handkarten, Kontostand und Einsatz
- Berechnet Punktestand dynamisch unter Berücksichtigung von Assen
- Methoden: `AddCard()`, `ClearHand()`, `GetScore()`, `ShowHand()`

### `SlotMachine`
- Implementiert das Slotspiel mit Animation und Zufallslogik
- Gewinnsystem je nach Symbol
- Methoden: `Start()`, `GetGewinn()`, `SpinAnimation()`

### `Program`
- Einstiegspunkt des Spiels
- Menüführung zwischen SlotMachine und Blackjack
- Beinhaltet Spiel-Loop für Blackjack mit Einsatz, Kartenziehen und Ergebnisermittlung

---

## 💰 Währungssystem

- Der Spieler startet mit **1500 Jetons**
- Gewinne aus Blackjack oder Slot Machine erhöhen den Kontostand
- Verlust bedeutet Abzug des Einsatzes oder Jetons
- Der gemeinsame `konto`-Wert wird persistent zwischen Spielen verwaltet

---

## ▶️ Spiel starten

1. Kompilieren & starten mit Visual Studio oder `dotnet run`
2. Im Menü aus folgenden Optionen wählen:
 - [ ] Slot Machine
 - [ ] Blackjack
 - [ ] Beenden
4. Folge den Anweisungen im Terminal und versuche dein Glück!

---

## 📚 Lernziele & Konzepte

- Objektorientierte Programmierung (OOP)
- Datenstrukturen (Listen, Klassen)
- Konsoleninteraktion
- Zufallszahlen und Spielmechaniken
- Benutzerfreundliche Textausgabe mit Farben & Symbolen

---

## 📝 To-Do / Erweiterungsideen

- Highscore-Liste implementieren
- Multiplayer-Unterstützung (lokal)
- Weitere Casino-Spiele integrieren (z. B. Poker, Roulette)
- Bessere Fehlerbehandlung und Eingabevalidierung
- Visuelle Oberfläche (GUI) mit WinForms oder WPF

---

## 🚀 Beispiel

```text
🎰 Willkommen im Online-Casino der doppel A's!
-----------------------------
💰 Jetons: 1500
1 - SlotMachine spielen
2 - BlackJack spielen
3 - Beenden
▶ Deine Auswahl: _




