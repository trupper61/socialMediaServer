-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 14. Mrz 2026 um 19:17
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
-- Tabellenstruktur für Tabelle `abonnement`
--

CREATE TABLE `abonnement` (
  `abonnentId` int(11) NOT NULL,
  `abonnierteNutzerId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `abonnement`
--

INSERT INTO `abonnement` (`abonnentId`, `abonnierteNutzerId`) VALUES
(1, 4),
(1, 8),
(2, 8),
(9, 4),
(15, 1),
(15, 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `beitrag`
--

CREATE TABLE `beitrag` (
  `beitragid` int(11) NOT NULL,
  `text` text DEFAULT NULL,
  `titel` text NOT NULL,
  `erstelltAm` datetime NOT NULL,
  `autor` int(11) NOT NULL,
  `tag` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `beitrag`
--

INSERT INTO `beitrag` (`beitragid`, `text`, `titel`, `erstelltAm`, `autor`, `tag`) VALUES
(1, NULL, 'Test', '2026-03-13 08:23:15', 15, 'Sonstiges'),
(2, NULL, 'Test2', '2026-03-13 08:24:04', 15, 'Sonstiges'),
(3, NULL, 'Tieire', '2026-03-13 08:26:28', 15, 'Tiere'),
(4, NULL, 'Oh my God!', '2026-03-13 08:26:48', 15, 'Memes'),
(5, NULL, 'Deutschlad will wieder bauen', '2026-03-13 08:27:19', 15, 'News'),
(6, NULL, 'Lul', '2026-03-13 08:30:14', 15, 'Sonstiges'),
(7, NULL, 'Genius', '2026-03-13 08:30:26', 15, 'Memes'),
(8, NULL, 'Gute Schule', '2026-03-13 08:32:43', 15, 'News'),
(9, NULL, 'Neiin', '2026-03-13 08:33:02', 15, 'Memes'),
(10, NULL, 'Tst10', '2026-03-13 08:33:18', 15, 'Sonstiges'),
(11, NULL, 'Tst11', '2026-03-13 08:33:29', 15, 'Sonstiges'),
(12, NULL, 'Lole', '2026-03-13 08:35:53', 2, 'Memes'),
(14, 'aedf', 'dgsdg', '2026-03-13 13:43:38', 1, 'Tiere'),
(15, 'dfnfnsgn', 'bgdfdfh', '2026-03-13 13:44:20', 1, 'Memes');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bild`
--

CREATE TABLE `bild` (
  `bildid` int(11) NOT NULL,
  `dateiname` text NOT NULL,
  `beitragid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `bild`
--

INSERT INTO `bild` (`bildid`, `dateiname`, `beitragid`) VALUES
(1, '03945a10-8694-43db-bb6a-fd422a4a4fb5.jpg', 1),
(2, 'a761bc44-8d36-434f-bda7-250e3bcdbc52.jpg', 1),
(3, '6a330ff8-3cf5-43e8-8575-b5c4a73fd6a8.png', 2),
(4, '766ee631-8d94-480d-b083-1896a789437c.jpg', 2),
(5, 'fc564a05-267f-4fff-a53a-3910c8e062f3.jpg', 3),
(6, '8c048be7-1253-4510-bf04-35742765ce14.jpg', 3),
(7, 'a266ba74-f37e-401c-9cf5-2bb7b005ab2d.jpg', 3),
(8, 'e8482cb5-c167-4a34-8e7f-f362248b81d2.jpg', 4),
(9, '29309f33-63a9-4f3d-9ec6-785d17f59179.png', 4),
(10, '0c89b137-607c-4620-909a-259c854d3490.jpg', 4),
(11, '034423dc-3b0e-4011-b31e-ee1a350e83ee.png', 5),
(12, '5306ae10-7896-4c2c-9d77-0073f94ddedb.jpg', 6),
(13, '499c1eac-53d3-4723-aba3-83434905840f.jpg', 6),
(14, 'c263bb45-9425-49c2-a867-c294706723e5.jpg', 6),
(15, 'a7f30e30-5dc5-4759-93cf-0da39d70d50d.jpg', 7),
(16, '23dec78d-dc82-404e-8053-408550b6f67c.png', 8),
(17, '2eaa3433-9e7e-48a4-bf16-3eb38eb7647f.jpg', 9),
(18, 'c509f722-43e1-4d8c-b20b-a5314d7046d0.jpg', 9),
(19, 'c6416a51-d953-438a-87ec-9310cef6dee2.jpg', 9),
(20, '642db507-8704-43bb-95cf-76567bdcdd8a.jpg', 10),
(21, '62d8d16b-214e-494a-a6ba-6a02d0bbd4a2.jpg', 11),
(22, 'fb02faa5-b014-4a30-a3e2-36fcfcd3a755.jpg', 12),
(23, 'bbf54ce6-cad8-4e76-886d-dadca6e37cd0.jpg', 14),
(24, 'acaebec4-e223-48cb-a896-635324ebed5f.jpg', 15),
(25, '36ab7bd4-6783-4876-821d-4189bb06822d.jpg', 15);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `chat`
--

CREATE TABLE `chat` (
  `chatId` int(11) NOT NULL,
  `erstelltAm` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `chat`
--

INSERT INTO `chat` (`chatId`, `erstelltAm`) VALUES
(1, '2026-03-10 14:28:12');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `chatnachricht`
--

CREATE TABLE `chatnachricht` (
  `nachrichtId` int(11) NOT NULL,
  `chatId` int(11) DEFAULT NULL,
  `senderId` int(11) DEFAULT NULL,
  `text` text DEFAULT NULL,
  `gesendetAm` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `chatnachricht`
--

INSERT INTO `chatnachricht` (`nachrichtId`, `chatId`, `senderId`, `text`, `gesendetAm`) VALUES
(1, 1, 1, 'Hi', '2026-03-10 17:11:49'),
(2, 1, 15, 'Hi zurück', '2026-03-13 14:03:05'),
(3, 1, 1, 'HILLFFFFEEEEE', '2026-03-13 14:04:45'),
(4, 1, 15, '💀', '2026-03-13 14:05:03'),
(5, 1, 1, 'Warummmmm tust du dasssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss', '2026-03-13 14:05:52'),
(6, 1, 1, 'Hallooooo', '2026-03-14 18:22:01');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `chatteilnehmer`
--

CREATE TABLE `chatteilnehmer` (
  `chatId` int(11) NOT NULL,
  `nutzerId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `chatteilnehmer`
--

INSERT INTO `chatteilnehmer` (`chatId`, `nutzerId`) VALUES
(1, 1),
(1, 15);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `kommentar`
--

CREATE TABLE `kommentar` (
  `kommentarid` int(11) NOT NULL,
  `nachricht` varchar(99) NOT NULL,
  `timestamp` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `beitragId` int(11) NOT NULL,
  `autor` int(11) NOT NULL,
  `oberKommentarId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `kommentar`
--

INSERT INTO `kommentar` (`kommentarid`, `nachricht`, `timestamp`, `beitragId`, `autor`, `oberKommentarId`) VALUES
(1, 'Ich mag dich nicht ;(', '2026-03-13 12:46:52', 15, 15, NULL),
(2, 'Hallo wie geht es euch ;D', '2026-03-14 17:21:27', 15, 1, NULL);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `likes`
--

CREATE TABLE `likes` (
  `nutzerId` int(11) NOT NULL,
  `beitragId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `likes`
--

INSERT INTO `likes` (`nutzerId`, `beitragId`) VALUES
(1, 7),
(1, 9),
(1, 11),
(1, 12),
(2, 11),
(15, 12),
(15, 15);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `nutzer`
--

CREATE TABLE `nutzer` (
  `nutzerId` int(11) NOT NULL,
  `benutzerName` text NOT NULL,
  `passwort` text NOT NULL,
  `email` text NOT NULL,
  `zuletztAktiv` datetime NOT NULL,
  `profilBild` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `nutzer`
--

INSERT INTO `nutzer` (`nutzerId`, `benutzerName`, `passwort`, `email`, `zuletztAktiv`, `profilBild`) VALUES
(1, 'Anna_Schmidt', 'gAU0njOBbgYOKYHrJwB+u4mbXppEoePB9IhmvAyc4Ho6ZhTnvEPyarMvDzsfxgrT', 'anna.schmidt@email.com', '2026-03-09 22:05:59', '8e551208-04d6-4574-b461-8293e98ff362.jpg'),
(2, 'max_mueller', 'pWuK+tH1i8hHyeH8C8AzhPq0Ufk4FpTgFsdsEMHPoqbMWBrcLlRU7dDYO8TdN/xk', 'max.mueller@email.com', '2026-03-09 22:06:26', NULL),
(3, 'Sophie_Klein', 'rDalnsr5yf7Gg4hsSNTErgt3QcP0+9o5ySliqXPkCyMlF9GsWAfSUrSRs3sZ78Zd', 'sophie.klein@email.com', '2026-03-09 22:06:47', NULL),
(4, 'Lukas_Berg', 'tl3p+qgatFmk8tvTPgElzD+iJYcIAyIhCklIYxLKjF5dEQzNeMgBJo4AsyL0j3ye', 'lukas.berg@email.com', '2026-03-09 22:07:09', NULL),
(5, 'Laura_Hoffmann', 'VuDxY8d6S8QaFDHH8z16R/Idi13Dw55u5A9EF23V75/yiERRYP6II0UkfWWu0QZv', 'laura.hoffmann@email.com', '2026-03-09 22:07:26', NULL),
(6, 'Felix_Fischer', 'CfitT0P5Gv/Sv8TEPXNtdteaQt3gkFpIRs8VhixwdxBo6hxyMAoJ0zeIIk501xR6', 'felix.fischer@email.com', '2026-03-09 22:07:42', NULL),
(7, 'Jonas_Schneider', '8DwfLMMFwniPLgznmwOGNSj5zrtslTh2DtpHNtWOp2Hy8qZZnBl16uNePwuKd7e6', 'jonas.schneider@email.com', '2026-03-09 22:08:00', NULL),
(8, 'Emilia_Wolf', 'rxUD0hwa1J81LIpF08qAm4H+nQ3AhjtVmlR9Ir/qRTSgFr6BXx3l8QsaPS6/e4cA', 'emilia.wolf@email.com', '2026-03-09 22:08:30', NULL),
(9, 'Paul_Braun', '9nPg0nO2DrEj9rrh8pkFNhWDnZdHrDJMn4SqS7qbo2MIT0YmxMbbIrT1nb+IqFo8', 'paul.braun@email.com', '2026-03-09 22:08:46', NULL),
(10, 'Clara_Hahn', 'uOBHf0P9C5Kd7csZaivgv45uIMLKYaJSFZ/5E8fW2plfepwAWEKMFVzkyq4Y4pcN', 'clara.hahn@email.com', '2026-03-09 22:09:01', NULL),
(11, 'Tim_Sommer', 'AawjHQbfRJX6QMBE4hFnqNKXDpQqeX2TwVh5iEK3ncj+rxDts6scovFYayutbpWw', 'tim.sommer@email.com', '2026-03-09 22:09:18', NULL),
(12, 'Lina_Seidel', 'xiHcU2ER+hcMJbMZWYne/di44ZGxzGqMexjilXC9AunmkS5W4yqkWyIX4x/CL3nw', 'lina.seidel@email.com', '2026-03-09 22:09:37', NULL),
(13, 'David_Koch', '4Zig9vQJVMUXr5P2AWQAj0UdoJlRnbspG/6ryljBOsPr/ccBrrise6SKJACKTx8d', 'david.koch@email.com', '2026-03-09 22:09:53', NULL),
(14, 'Julia_Mayer', 'KJnd6wJMclmmjeQ3UZcyQZ9jmUiC4JhQadPGen20u3RxNx0Z6P7QnK5BKsNAZL3L', 'julia.mayer@email.com', '2026-03-09 22:10:08', NULL),
(15, 'hilfe', 'KAZ5pncPFfYeWgRkaHNASkemq1iDDM60Z5nZmzzR8MoMy1YCNbujQi6fzGybm02t', '124134142421', '2026-03-10 13:48:05', NULL);

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `abonnement`
--
ALTER TABLE `abonnement`
  ADD PRIMARY KEY (`abonnentId`,`abonnierteNutzerId`),
  ADD KEY `abonnierteNutzerFK` (`abonnierteNutzerId`);

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
  ADD PRIMARY KEY (`bildid`),
  ADD KEY `help` (`beitragid`);

--
-- Indizes für die Tabelle `chat`
--
ALTER TABLE `chat`
  ADD PRIMARY KEY (`chatId`);

--
-- Indizes für die Tabelle `chatnachricht`
--
ALTER TABLE `chatnachricht`
  ADD PRIMARY KEY (`nachrichtId`),
  ADD KEY `senderFK` (`senderId`),
  ADD KEY `chat2FK` (`chatId`);

--
-- Indizes für die Tabelle `chatteilnehmer`
--
ALTER TABLE `chatteilnehmer`
  ADD PRIMARY KEY (`chatId`,`nutzerId`),
  ADD KEY `nutzerFK` (`nutzerId`);

--
-- Indizes für die Tabelle `kommentar`
--
ALTER TABLE `kommentar`
  ADD PRIMARY KEY (`kommentarid`),
  ADD KEY `beitragFK` (`beitragId`),
  ADD KEY `oberKommentarId` (`oberKommentarId`),
  ADD KEY `asd` (`autor`);

--
-- Indizes für die Tabelle `likes`
--
ALTER TABLE `likes`
  ADD PRIMARY KEY (`nutzerId`,`beitragId`),
  ADD KEY `beitragIDFK` (`beitragId`);

--
-- Indizes für die Tabelle `nutzer`
--
ALTER TABLE `nutzer`
  ADD PRIMARY KEY (`nutzerId`),
  ADD UNIQUE KEY `email` (`email`) USING HASH;

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `beitrag`
--
ALTER TABLE `beitrag`
  MODIFY `beitragid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT für Tabelle `bild`
--
ALTER TABLE `bild`
  MODIFY `bildid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT für Tabelle `chat`
--
ALTER TABLE `chat`
  MODIFY `chatId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `chatnachricht`
--
ALTER TABLE `chatnachricht`
  MODIFY `nachrichtId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT für Tabelle `kommentar`
--
ALTER TABLE `kommentar`
  MODIFY `kommentarid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `nutzer`
--
ALTER TABLE `nutzer`
  MODIFY `nutzerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `abonnement`
--
ALTER TABLE `abonnement`
  ADD CONSTRAINT `abonnentFk` FOREIGN KEY (`abonnentId`) REFERENCES `nutzer` (`nutzerId`),
  ADD CONSTRAINT `abonnierteNutzerFK` FOREIGN KEY (`abonnierteNutzerId`) REFERENCES `nutzer` (`nutzerId`);

--
-- Constraints der Tabelle `beitrag`
--
ALTER TABLE `beitrag`
  ADD CONSTRAINT `autorFK` FOREIGN KEY (`autor`) REFERENCES `nutzer` (`nutzerId`);

--
-- Constraints der Tabelle `bild`
--
ALTER TABLE `bild`
  ADD CONSTRAINT `help` FOREIGN KEY (`beitragid`) REFERENCES `beitrag` (`beitragid`) ON UPDATE CASCADE;

--
-- Constraints der Tabelle `chatnachricht`
--
ALTER TABLE `chatnachricht`
  ADD CONSTRAINT `chat2FK` FOREIGN KEY (`chatId`) REFERENCES `chat` (`chatId`),
  ADD CONSTRAINT `senderFK` FOREIGN KEY (`senderId`) REFERENCES `nutzer` (`nutzerId`);

--
-- Constraints der Tabelle `chatteilnehmer`
--
ALTER TABLE `chatteilnehmer`
  ADD CONSTRAINT `chatFK` FOREIGN KEY (`chatId`) REFERENCES `chat` (`chatId`),
  ADD CONSTRAINT `nutzerFK` FOREIGN KEY (`nutzerId`) REFERENCES `nutzer` (`nutzerId`);

--
-- Constraints der Tabelle `kommentar`
--
ALTER TABLE `kommentar`
  ADD CONSTRAINT `asd` FOREIGN KEY (`autor`) REFERENCES `nutzer` (`nutzerId`),
  ADD CONSTRAINT `beitragFK` FOREIGN KEY (`beitragId`) REFERENCES `beitrag` (`beitragid`),
  ADD CONSTRAINT `oberKommentarId` FOREIGN KEY (`oberKommentarId`) REFERENCES `kommentar` (`kommentarid`);

--
-- Constraints der Tabelle `likes`
--
ALTER TABLE `likes`
  ADD CONSTRAINT `beitragIDFK` FOREIGN KEY (`beitragId`) REFERENCES `beitrag` (`beitragid`),
  ADD CONSTRAINT `nutzerIdFK` FOREIGN KEY (`nutzerId`) REFERENCES `nutzer` (`nutzerId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
