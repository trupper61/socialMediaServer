-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 20. Feb 2026 um 10:34
-- Server-Version: 10.4.32-MariaDB
-- PHP-Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `smpdb`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `beitrag`
--

CREATE TABLE `beitrag` (
  `beitragid` int(11) NOT NULL,
  `text` text DEFAULT NULL,
  `titel` text NOT NULL,
  `erstelltAm` datetime NOT NULL,
  `autor` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `beitrag`
--

INSERT INTO `beitrag` (`beitragid`, `text`, `titel`, `erstelltAm`, `autor`) VALUES
(1, NULL, 'Test', '2026-02-13 12:54:41', 2),
(2, NULL, 'Test', '2026-02-13 14:00:09', 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bild`
--

CREATE TABLE `bild` (
  `bildid` int(11) NOT NULL,
  `dateiname` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `bild`
--

INSERT INTO `bild` (`bildid`, `dateiname`) VALUES
(1, '27e60095-5c0a-4c41-9e0b-18a2fab0aa68.png'),
(2, 'bd24f52c-924b-4ddc-8994-eac4a1713bbe.png');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `inhalt`
--

CREATE TABLE `inhalt` (
  `beitragIdFK` int(11) NOT NULL,
  `bildId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `inhalt`
--

INSERT INTO `inhalt` (`beitragIdFK`, `bildId`) VALUES
(1, 1),
(2, 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `likes`
--

CREATE TABLE `likes` (
  `zeitstempel` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `benutzerName` int(11) NOT NULL,
  `beitragId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `nutzer`
--

CREATE TABLE `nutzer` (
  `nutzerId` int(11) NOT NULL,
  `benutzerName` text NOT NULL,
  `passwort` text NOT NULL,
  `email` text NOT NULL,
  `zuletztAktiv` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `nutzer`
--

INSERT INTO `nutzer` (`nutzerId`, `benutzerName`, `passwort`, `email`, `zuletztAktiv`) VALUES
(1, 'Nutzername...', 'Passwort festlegen...', 'Email Eingeben', '2026-02-13 12:14:59'),
(2, 'hilfe', '234', 'hilfe@gmail.com', '2026-02-13 12:16:50');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `beitrag`
--
ALTER TABLE `beitrag`
  ADD PRIMARY KEY (`beitragid`),
  ADD KEY `autorFK` (`autor`);

--
-- Indizes für die Tabelle `bild`
--
ALTER TABLE `bild`
  ADD PRIMARY KEY (`bildid`);

--
-- Indizes für die Tabelle `inhalt`
--
ALTER TABLE `inhalt`
  ADD PRIMARY KEY (`beitragIdFK`,`bildId`),
  ADD KEY `bildId` (`bildId`);

--
-- Indizes für die Tabelle `likes`
--
ALTER TABLE `likes`
  ADD PRIMARY KEY (`beitragId`,`benutzerName`),
  ADD KEY `benutzerFK` (`benutzerName`);

--
-- Indizes für die Tabelle `nutzer`
--
ALTER TABLE `nutzer`
  ADD PRIMARY KEY (`nutzerId`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `beitrag`
--
ALTER TABLE `beitrag`
  MODIFY `beitragid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `bild`
--
ALTER TABLE `bild`
  MODIFY `bildid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `nutzer`
--
ALTER TABLE `nutzer`
  MODIFY `nutzerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `beitrag`
--
ALTER TABLE `beitrag`
  ADD CONSTRAINT `autorFK` FOREIGN KEY (`autor`) REFERENCES `nutzer` (`nutzerId`);

--
-- Constraints der Tabelle `inhalt`
--
ALTER TABLE `inhalt`
  ADD CONSTRAINT `beitragFK2` FOREIGN KEY (`beitragIdFK`) REFERENCES `beitrag` (`beitragid`),
  ADD CONSTRAINT `bildId` FOREIGN KEY (`bildId`) REFERENCES `bild` (`bildid`);

--
-- Constraints der Tabelle `likes`
--
ALTER TABLE `likes`
  ADD CONSTRAINT `beitragId` FOREIGN KEY (`beitragId`) REFERENCES `beitrag` (`beitragid`),
  ADD CONSTRAINT `benutzerFK` FOREIGN KEY (`benutzerName`) REFERENCES `nutzer` (`nutzerId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
