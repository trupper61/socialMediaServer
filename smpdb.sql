-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 17. Mrz 2026 um 11:42
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
CREATE DATABASE IF NOT EXISTS `smpdb` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `smpdb`;

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
(1, 2),
(1, 4),
(1, 8),
(1, 12),
(1, 15),
(1, 22),
(2, 3),
(2, 8),
(2, 15),
(2, 17),
(3, 10),
(3, 16),
(3, 17),
(4, 16),
(5, 3),
(5, 16),
(5, 17),
(7, 22),
(8, 14),
(9, 4),
(9, 5),
(9, 17),
(10, 11),
(10, 12),
(12, 8),
(14, 7),
(14, 17),
(15, 1),
(16, 10),
(17, 20),
(18, 10),
(18, 13),
(20, 10),
(21, 1),
(21, 12),
(21, 22),
(22, 1),
(22, 15),
(23, 15);

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
(1, '', 'Immer diese Trauben Soda', '2026-03-15 20:10:18', 9, 'Memes'),
(2, '', 'Schätze dann laufe ich einfach', '2026-03-15 20:11:22', 9, 'Memes'),
(3, '', 'Sein größter Fehler', '2026-03-15 20:12:18', 9, 'Memes'),
(4, '', 'Wie software genutzt wird', '2026-03-15 20:17:53', 2, 'Memes'),
(5, '', 'Wir alle sind drinn!', '2026-03-15 20:19:36', 2, 'Memes'),
(6, '', 'In der Sonne chillen', '2026-03-15 20:54:57', 5, 'Tiere'),
(7, '', 'Früher hatte ich Blumen, jetzt Etagenbett', '2026-03-15 20:58:00', 5, 'Tiere'),
(8, '', 'Rate my Cat', '2026-03-15 21:00:32', 5, 'Tiere'),
(10, '', 'Hallo Wilkommen', '2026-03-15 21:05:30', 16, 'Sonstiges'),
(11, '', 'Let him cook', '2026-03-15 21:20:37', 3, 'Memes'),
(12, '', 'Mein bester Freund hat Geburtstag', '2026-03-15 21:23:14', 3, 'Tiere'),
(13, '', 'Hallo', '2026-03-15 21:25:14', 16, 'Memes'),
(14, '', 'Deutsche Hogwarts Brief', '2026-03-15 21:27:21', 4, 'Memes'),
(15, '', 'Zombies', '2026-03-15 21:32:05', 18, 'News'),
(17, '', 'It\'s an art form... or is it?', '2026-03-15 21:33:32', 18, 'News'),
(18, '', 'Donald makes America Great AGAIN!', '2026-03-15 21:34:07', 17, 'News'),
(19, 'Ich mag süße Tiere, und du?', 'Süße Tiere', '2026-03-15 21:40:22', 6, 'Tiere'),
(20, 'We all know it Donny. You don\'t have to hide it!', 'Donny appears to be a shrimp', '2026-03-15 21:48:33', 18, 'News'),
(21, 'He\'s so like me :)', 'Trump be like', '2026-03-15 21:53:04', 18, 'Memes'),
(22, '🫣', 'amogus', '2026-03-15 21:56:48', 13, 'Memes'),
(23, 'blabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalbablabalblalbballalalalablblbblblablalblablalbalba', 'Trump announces freedom in Venezuela', '2026-03-15 21:58:08', 19, 'News'),
(24, 'Fucking again', 'Trump is invading Greenland', '2026-03-15 22:03:21', 20, 'News'),
(25, '', 'I love Shrek', '2026-03-15 22:08:20', 16, 'Memes'),
(26, '', 'The pinnecale of Man', '2026-03-15 22:12:12', 10, 'Sonstiges'),
(27, '', 'The pinnecale of Mankind part 2', '2026-03-15 22:14:47', 10, 'Sonstiges'),
(28, 'So einfach, bloß alle haben kein Köpfchen!', 'Der Plan für die deutsche Writschaft', '2026-03-15 22:17:02', 11, 'News'),
(29, 'Fass ihn Södi und Merz hinterher!', 'Dort sind die Grünen!', '2026-03-15 22:22:05', 21, 'Sonstiges'),
(30, '', 'Deutsche Bahn be like..', '2026-03-15 22:24:02', 21, 'Memes'),
(31, '', 'Bruder muss los', '2026-03-15 22:28:55', 1, 'Memes'),
(32, '', 'Neuste Bilder in der Nähe von Moskau', '2026-03-15 22:43:45', 12, 'News'),
(33, 'Ist leider nach mehrmaligen Ausprobieren kompletter Schrott. Schreibt mir wenn jemand sie mir für gutes Geld abkaufen möchte.', 'Neuste Maschine gekauft', '2026-03-15 22:47:37', 8, 'Sonstiges'),
(34, '', 'Cursed Cats', '2026-03-15 22:57:56', 14, 'Memes'),
(35, '', 'Cursed Dogs', '2026-03-15 23:01:56', 7, 'Sonstiges'),
(36, '', 'WTF happend with Mickey', '2026-03-15 23:03:02', 7, 'Sonstiges'),
(37, '', 'Für die Welse!!!', '2026-03-15 23:13:00', 22, 'News'),
(38, '', 'Typisch Söder', '2026-03-15 23:14:56', 1, 'Memes'),
(39, '', 'Das alles nur geklaut (eh-oh)', '2026-03-15 23:17:53', 2, 'News'),
(40, 'Diese kleine Staubträger bringen jeden zum Lächeln!', 'Cute Katzenbilder', '2026-03-16 20:35:55', 14, 'Tiere'),
(41, 'Die neuste Generation der Smartphone wurde soeben vorgestellt! Lasst bitti bitti ein Like da :)', 'Neues von der Tech-Welt', '2026-03-16 20:41:02', 2, 'News'),
(42, 'Nich schon wieder :(', 'Montagsmemes', '2026-03-16 20:42:45', 3, 'Memes'),
(43, 'Diese Spinne in Australien hat man einfach \"Big Boy\" genannt. Sie steht im Vergleich zu einer anderen Trichternetzspinne', 'Neue Spinnenart in Australien', '2026-03-16 21:08:55', 4, 'News'),
(44, '', 'Bavaria One spotted', '2026-03-16 22:07:59', 13, 'Memes'),
(45, '', 'Danke Merkel', '2026-03-16 22:11:23', 14, 'Memes'),
(46, 'Sleepy Joe sends his regards. MAGA!', 'Thank you Biden', '2026-03-16 22:12:54', 17, 'Memes'),
(47, '', 'Rede', '2026-03-16 22:13:42', 12, 'Sonstiges'),
(48, '', 'Zoll-Pingpong', '2026-03-16 22:18:40', 10, 'Memes'),
(49, '', 'Trump sucht Namen', '2026-03-16 22:27:21', 20, 'News'),
(50, '', 'Kundensupport mal richtig', '2026-03-16 22:30:20', 8, 'Sonstiges'),
(51, '', 'Me after Abitur', '2026-03-16 22:43:43', 16, 'Memes'),
(52, '', 'Informatik Memes', '2026-03-16 22:45:51', 15, 'Memes'),
(53, '', 'No Haydn', '2026-03-17 09:23:03', 23, 'Memes');

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
(1, '9b747ec9-ec59-400e-86de-a182c2fe6d46.jpeg', 1),
(2, 'f9c132a2-dcf3-4ee5-8af4-a5de72935219.png', 2),
(3, '67ca15d6-32f3-4834-a9c0-381e458f01b6.jpeg', 3),
(4, '71be9d05-6dca-4432-bb9b-0ff8b1c2af99.png', 4),
(5, '59448c65-98a3-40bd-b4c7-bba795c448e3.png', 5),
(6, 'f8e6f5e4-44cc-4f71-934e-807002698e76.png', 6),
(7, 'c7d24644-8e6c-4154-be9e-4dea79bed4b3.jpeg', 7),
(8, '87ffd4b2-4c82-46dd-8894-ac166273cb79.jpeg', 8),
(9, 'cf25bbd3-cf0f-4db2-8118-271bd72c7842.png', 10),
(10, 'fcf8eea8-dfa0-4c8a-bb97-d6c6dbf7adbe.png', 11),
(11, '785b9425-01c0-41e1-a7d8-ef8fd19f073f.png', 12),
(12, 'c038d34d-a466-4951-b3b7-47b3bf0badb6.png', 12),
(13, 'de414141-9faf-4fbe-a251-632258bb3b0b.png', 13),
(14, '37a21ea5-120b-4f2c-a97d-5dad5fab8338.jpeg', 14),
(15, 'c06d229f-827d-4124-973a-5d5acd3b6e9f.jpg', 15),
(16, 'a8cb7497-4387-44d1-aff1-87865d38e555.jpg', 17),
(17, 'bcafb231-42e0-4939-a988-671f15cdacb8.png', 18),
(18, '567848e0-4749-433f-86cf-2b8e47152289.png', 18),
(19, 'a1ea5388-9b0b-4786-a1b3-b72d8285bee5.png', 18),
(20, '6ec57d67-b1df-4e01-a3c9-14f99acf14ed.png', 18),
(21, '4355ee9e-2eca-4878-b03a-b127be564dd1.jpg', 19),
(22, '2a6e5765-ac99-4aea-a91e-e260fd97e506.jpg', 19),
(23, 'f5eb3c9d-e575-45bc-b250-7fde3f26621f.jpg', 19),
(24, '7a1a08b4-3c37-48c6-90c3-71fd50436bec.jpg', 20),
(25, 'dd38ece0-57b4-437c-ae99-b0bb3632b6ef.jpg', 21),
(26, '23a95a1f-05fb-45d2-ab73-adf30a09ea30.jpg', 22),
(27, 'bda459c6-ad72-4049-a152-7ce06a410b29.png', 23),
(28, 'cffa9228-20d9-4b6d-8f22-6380b17ec788.jpg', 24),
(29, '87109cda-bd79-4c96-8bb0-c16caa7725bd.png', 25),
(30, '8a986af2-56a6-4a83-9e36-73e78865877a.jpg', 26),
(31, '8a0ff511-4eba-4fc5-931d-c647895f943f.jpg', 27),
(32, 'f6e95eab-52ad-4ecf-8df0-f4f95f8f9341.jpg', 27),
(33, 'ff189b81-cafa-4071-9e0c-e1e7c2c5237a.jpg', 27),
(34, '76411180-059d-43d9-a6de-e35d506a859e.jpg', 28),
(35, 'edb08a7a-b8e9-414c-842b-fdfe5f68c7a9.png', 28),
(36, 'becc5b8d-9b85-42ea-8579-b0617fb25a65.jpg', 29),
(37, 'e2a840f7-a230-4986-82a5-e8080100f8f2.jpeg', 30),
(38, '6f070b24-0f70-49e3-96f5-364570b37ba3.jpeg', 31),
(39, '02f9c164-78a1-47b9-962d-fcfa78f5031a.jpg', 32),
(40, '5a25957f-1810-496c-a7f8-9c378fdd4a41.jpg', 32),
(41, '9c5ae73b-1f89-4f59-b844-d891abf6cb71.png', 33),
(42, '600597ab-a2c7-44e5-8c03-d437fc386f10.png', 34),
(43, '11391feb-2ab4-4dbe-8f79-b11dfa612af9.jpg', 34),
(44, 'e3b999a4-3338-4171-8e0b-4c1cd34a7045.jpg', 34),
(45, '061c590b-ebac-4053-8398-bbe6045c3ade.jpg', 34),
(46, 'd285fe40-8f07-45c9-8cd4-9616affe9e32.jpg', 34),
(47, '79541f40-9d14-4fe2-81e9-d844c1d0cbb4.jpg', 35),
(48, '34ad91cf-b437-451d-b7db-3e71a7195a01.jpg', 35),
(49, 'a016d1c5-ff76-4c47-8c20-164e7cd3f0a0.jpg', 35),
(50, 'b10ed54a-647f-4511-b8c8-f0268c58575e.png', 35),
(51, '177f9796-e3cb-4f46-9445-1adaeb8ef2bd.jpg', 36),
(52, '16a35f9f-4274-46dd-9176-47dcc824182a.jpg', 37),
(53, '5d42f532-0423-4754-a466-c872092be925.jpg', 37),
(54, 'a3dc4e2e-3eba-4363-83b1-9af7d508cd4e.jpeg', 37),
(55, 'a88f5b40-4d45-4a89-b701-42bb89f56bed.png', 38),
(56, '26a7a1cb-3957-4874-b6a0-a8e4fa37ce7b.jpeg', 39),
(57, '4784f987-f2f9-44b6-9beb-e0ffcf535d33.jpg', 40),
(58, '2afbf080-1d9f-4324-8f6f-31e9ac4ece30.jpg', 40),
(59, 'f65fe0a6-c101-409e-a412-c7b3cfd69d8f.jpg', 40),
(60, 'b43e629c-d26f-4cf7-8348-bce4f52200ad.jpg', 40),
(61, 'd1d0095c-6618-43ff-9bba-1dde52feddc5.jpg', 40),
(62, 'd1eb41e9-8aeb-4097-bd41-10bd354084dd.jpg', 41),
(63, '86e29e56-d18a-4d28-9ac0-35459a2bd83f.jpg', 42),
(64, '29b5034d-1d3e-4b4d-9282-f2068b6da822.jpg', 42),
(65, '15aeb292-4dc6-48e6-9546-8c7f1dc81ea2.jpg', 43),
(66, '01fcf890-bc6e-4cd1-9bc7-5dc8b899d354.jpg', 44),
(67, '2343983b-988b-4922-a761-962684349841.jpeg', 45),
(68, '53aede65-3c82-4edc-8a04-b936e3a0b53c.png', 46),
(69, '5311e72f-f476-4323-9d9b-cb0df67cf08d.png', 47),
(70, 'd073c6cb-32c8-4ccb-9ab3-2ea59e478f2a.jpeg', 48),
(71, 'e3495d4e-174a-421c-a1b2-a12863648423.jpeg', 49),
(72, '2a6aa295-1340-4d4b-854a-954878c1a9bb.jpeg', 50),
(73, '9d2a7636-1b44-42b3-a17b-5268b667ea11.png', 51),
(74, '55011295-05f1-4fbd-bd8c-a6bfad34f59d.png', 52),
(75, 'ca6c3e09-4e9a-4b54-85c3-4f855c9fe665.jpg', 52),
(76, 'de40cbbc-e861-4cf2-b6cb-4025962e82bc.jpg', 52),
(77, '5e6d5031-c55b-4e0d-a8c4-2e8deab08529.jpg', 52),
(78, '948d928b-b4ee-4d3e-8eef-ee31d208ecf6.png', 52),
(79, 'de9c22d0-bb28-4de4-aafd-3d07fd64afd7.jpg', 52),
(80, '7342f0c4-d72f-4a39-bf57-ead6eb9dcef3.jpg', 52),
(81, '699198ac-6465-4b0f-8d17-2f8b568d6fb4.jpg', 53);

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
(1, '2026-03-12 21:02:11'),
(2, '2026-03-15 20:46:12'),
(3, '2026-03-15 20:46:57'),
(4, '2026-03-15 21:05:52'),
(5, '2026-03-15 21:06:43'),
(6, '2026-03-15 21:27:30'),
(7, '2026-03-15 21:33:51'),
(8, '2026-03-15 22:24:23'),
(9, '2026-03-16 17:02:37'),
(10, '2026-03-16 22:10:35'),
(11, '2026-03-16 22:10:43');

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
(31, 2, 15, 'Hallo max', '2026-03-15 20:52:08'),
(32, 4, 16, 'Hallo', '2026-03-15 21:05:54'),
(33, 4, 16, 'HALLOOOOOOO ANTOWRTE', '2026-03-15 21:06:31'),
(34, 5, 16, 'Hallo', '2026-03-15 21:06:46'),
(35, 5, 16, 'Antworte bitte', '2026-03-15 21:06:52'),
(36, 5, 16, ';((((', '2026-03-15 21:06:56'),
(37, 5, 3, 'Sorry sorry', '2026-03-15 21:22:04'),
(38, 7, 18, 'ey jo wats up', '2026-03-15 21:34:00'),
(39, 7, 17, 'OBAMMAMAMMAMAMAAMMAMAMAMAMA', '2026-03-15 22:02:25'),
(40, 8, 1, 'Du hast ja immer so sehr recht!', '2026-03-15 22:24:33'),
(41, 8, 1, 'Komm, wir kriegen dich schon irgendwie wieder ins Amt!', '2026-03-15 22:24:54'),
(42, 1, 15, 'Hallo, du coole Socke!', '2026-03-16 18:28:44'),
(43, 2, 15, 'AAAAAAAAAAAAAAAAAAAAaasdadaaahahhahahhhahhahahhahahahahahhahaahhaha', '2026-03-16 18:54:35'),
(44, 10, 14, 'Sach mal wer bist denn du?', '2026-03-16 22:10:45'),
(45, 11, 17, 'Who are you? The Radical left?', '2026-03-16 22:10:59'),
(46, 10, 14, 'Wer hat dir diesen Zugang gegeben?', '2026-03-16 22:11:00');

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
(1, 15),
(2, 2),
(2, 15),
(3, 2),
(3, 5),
(4, 5),
(4, 16),
(5, 3),
(5, 16),
(6, 4),
(6, 16),
(7, 17),
(7, 18),
(8, 1),
(8, 21),
(9, 1),
(9, 2),
(10, 14),
(10, 23),
(11, 17),
(11, 23);

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
(1, 'Schon eine 10/10', '2026-03-15 20:01:44', 8, 3, NULL),
(2, 'Schon sehr KI', '2026-03-15 20:06:32', 10, 3, NULL),
(3, 'Probkem', '2026-03-15 20:07:26', 10, 16, NULL),
(4, 'Vallah, ich schwör nur einmal', '2026-03-15 20:27:40', 14, 4, NULL),
(5, 'Ohh ma gad, donny. A true patriot', '2026-03-15 20:36:53', 18, 18, NULL),
(6, 'FAKKKEEEEE NEWWSS!!!', '2026-03-15 20:49:10', 20, 17, NULL),
(7, 'LEFTIST PRROPAGANDA', '2026-03-15 20:49:18', 20, 17, NULL),
(8, 'I dont speak mexican', '2026-03-15 20:51:19', 19, 17, NULL),
(9, 'Sleepy Joe at its best. ', '2026-03-15 20:52:13', 15, 17, NULL),
(10, 'Fake News! Drain the swamp!', '2026-03-15 20:59:36', 23, 17, NULL),
(11, 'Dieser Hundesohn verfolgt mich in meinem Allpträimen', '2026-03-15 21:08:56', 25, 10, NULL),
(12, 'Soooooo süßßßß', '2026-03-15 21:13:29', 12, 10, NULL),
(13, 'Oh bitte herate mich, du gottheit!!!', '2026-03-15 21:15:54', 26, 11, NULL),
(14, 'Du hattest scon immer Recht!', '2026-03-15 21:26:21', 30, 1, NULL),
(15, 'Miau miau miau miau, miau, miau, miau miau miau, miau, miau, miau miau miau, miau, miau, miau miau ', '2026-03-15 22:00:58', 34, 7, NULL),
(16, 'Sicher', '2026-03-15 22:02:23', 30, 17, NULL),
(17, 'Das gehört nicht in die BRD!', '2026-03-15 22:04:38', 36, 21, NULL),
(18, 'Für die Welse!', '2026-03-15 22:08:33', 29, 22, NULL),
(19, 'So cute', '2026-03-16 19:43:46', 40, 15, NULL),
(20, 'Well i dont think biden would make it this far.', '2026-03-16 20:13:22', 2, 17, NULL),
(21, 'True', '2026-03-16 20:43:01', 38, 16, NULL),
(22, 'That was my News!!!', '2026-03-16 21:25:27', 48, 20, NULL),
(23, 'based. MAGA', '2026-03-16 21:31:29', 48, 17, NULL);

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
(1, 28),
(1, 30),
(1, 37),
(1, 39),
(1, 43),
(2, 1),
(2, 3),
(2, 40),
(3, 4),
(3, 8),
(4, 4),
(4, 8),
(4, 25),
(4, 27),
(4, 31),
(4, 40),
(4, 42),
(6, 13),
(6, 15),
(6, 17),
(6, 18),
(7, 34),
(8, 4),
(8, 23),
(8, 27),
(8, 31),
(8, 32),
(8, 46),
(8, 47),
(8, 48),
(8, 49),
(10, 4),
(10, 13),
(10, 14),
(10, 17),
(10, 20),
(10, 21),
(10, 23),
(10, 24),
(10, 25),
(10, 31),
(10, 40),
(10, 41),
(10, 44),
(10, 45),
(10, 46),
(10, 47),
(11, 26),
(11, 27),
(12, 25),
(12, 27),
(12, 30),
(12, 31),
(12, 46),
(13, 21),
(13, 23),
(13, 43),
(14, 4),
(14, 27),
(14, 30),
(14, 32),
(14, 33),
(14, 50),
(15, 4),
(15, 37),
(15, 38),
(15, 39),
(15, 40),
(15, 41),
(15, 42),
(15, 51),
(16, 1),
(16, 4),
(16, 8),
(16, 11),
(17, 1),
(17, 4),
(17, 8),
(17, 12),
(17, 19),
(17, 25),
(17, 33),
(17, 36),
(17, 40),
(17, 41),
(17, 43),
(20, 23),
(20, 48),
(21, 28),
(21, 35),
(21, 36),
(22, 31),
(22, 35),
(22, 36);

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
(1, 'Anna_Schmidt', 'yhWlrd+9JGGwiociVhjlyGOlWUNUVsBu5E34t2Tq1XMnEw6uBwDO2n7dDqNL55Uf', 'anna.schmidt@email.com', '2026-03-16 21:37:14', 'f615dbac-3907-4639-953a-1ed4fd7a0342.jpg'),
(2, 'max_mueller', 'pWuK+tH1i8hHyeH8C8AzhPq0Ufk4FpTgFsdsEMHPoqbMWBrcLlRU7dDYO8TdN/xk', 'max.mueller@email.com', '2026-03-16 20:41:02', '7e351615-ec02-435c-a71c-dd901b6334d0.jpg'),
(3, 'Sophie_Klein', 'rDalnsr5yf7Gg4hsSNTErgt3QcP0+9o5ySliqXPkCyMlF9GsWAfSUrSRs3sZ78Zd', 'sophie.klein@email.com', '2026-03-16 20:42:45', '97b44cc6-073c-4f78-bc53-564f40ea32a3.jpg'),
(4, 'Lukas_Berg', 'tl3p+qgatFmk8tvTPgElzD+iJYcIAyIhCklIYxLKjF5dEQzNeMgBJo4AsyL0j3ye', 'lukas.berg@email.com', '2026-03-16 21:08:55', 'df7cc9f7-3d31-4c87-821c-9038daa43cd3.jpg'),
(5, 'Laura_Hoffmann', 'VuDxY8d6S8QaFDHH8z16R/Idi13Dw55u5A9EF23V75/yiERRYP6II0UkfWWu0QZv', 'laura.hoffmann@email.com', '2026-03-09 22:07:26', 'b1deb309-25b0-4801-95f1-0af983c37953.jpeg'),
(6, 'Felix_Fischer', 'CfitT0P5Gv/Sv8TEPXNtdteaQt3gkFpIRs8VhixwdxBo6hxyMAoJ0zeIIk501xR6', 'felix.fischer@email.com', '2026-03-09 22:07:42', NULL),
(7, 'Jonas_Schneider', '8DwfLMMFwniPLgznmwOGNSj5zrtslTh2DtpHNtWOp2Hy8qZZnBl16uNePwuKd7e6', 'jonas.schneider@email.com', '2026-03-09 22:08:00', 'b118000e-d4a0-4dab-8142-7d441e936105.png'),
(8, 'Emilia_Wolf', 'rxUD0hwa1J81LIpF08qAm4H+nQ3AhjtVmlR9Ir/qRTSgFr6BXx3l8QsaPS6/e4cA', 'emilia.wolf@email.com', '2026-03-16 22:30:20', '4baa6970-8403-451d-a8dd-601d6bf403e7.png'),
(9, 'Paul_Braun', '9nPg0nO2DrEj9rrh8pkFNhWDnZdHrDJMn4SqS7qbo2MIT0YmxMbbIrT1nb+IqFo8', 'paul.braun@email.com', '2026-03-09 22:08:46', NULL),
(10, 'Clara_Hahn', 'uOBHf0P9C5Kd7csZaivgv45uIMLKYaJSFZ/5E8fW2plfepwAWEKMFVzkyq4Y4pcN', 'clara.hahn@email.com', '2026-03-16 22:18:40', '9a1329e3-8ba3-4bf3-81a8-4244e524517c.jpg'),
(11, 'Tim_Sommer', 'AawjHQbfRJX6QMBE4hFnqNKXDpQqeX2TwVh5iEK3ncj+rxDts6scovFYayutbpWw', 'tim.sommer@email.com', '2026-03-09 22:09:18', NULL),
(12, 'Lina_Seidel', 'xiHcU2ER+hcMJbMZWYne/di44ZGxzGqMexjilXC9AunmkS5W4yqkWyIX4x/CL3nw', 'lina.seidel@email.com', '2026-03-16 22:13:42', 'fc60d7e0-8bf8-4ce5-ab51-7e864198a42c.png'),
(13, 'David_Koch', '4Zig9vQJVMUXr5P2AWQAj0UdoJlRnbspG/6ryljBOsPr/ccBrrise6SKJACKTx8d', 'david.koch@email.com', '2026-03-16 22:07:59', '913f2d65-f674-46f2-85c0-6e0119191db4.jpg'),
(14, 'Julia_Mayer', 'KJnd6wJMclmmjeQ3UZcyQZ9jmUiC4JhQadPGen20u3RxNx0Z6P7QnK5BKsNAZL3L', 'julia.mayer@email.com', '2026-03-16 22:33:25', '164a9385-9a15-473e-9834-016ccaa7e6fb.jpg'),
(15, 'hilfe', 'NyUop4mD8NGZy/iycRNpab2Z402WduXNI2lj/qfguaViYw1qLOi61Cflw9N5EP6v', 'Email Eingeben', '2026-03-17 09:09:54', '7823d15d-4181-4507-9f1e-904604b98283.jpg'),
(16, 'GenerischerNutzer', '2esuG9LyVq8tDnqesU9qFaUMuf8DWIv/r5sH2soah203uU1AQ2djSZpAuFWZomfd', 'generischer@de', '2026-03-16 22:46:15', 'c905240d-4a21-48c6-a39e-ca4d68643a03.png'),
(17, 'DonaldNews', 'HY1s2KohW82s59jh0H23iT0bdYAkAbTIMYFtNkpKwtECGBk6KAZt6cNHehmVfTSJ', 'donalds@gmail.com', '2026-03-16 22:31:29', '37d23bfc-1df6-4021-b65f-8e103ed38c4b.png'),
(18, 'Obama@news', 'qDNTrEmomxJBg0jS1XD+7IxwmHQZu6saJaHnMsVroAsecJgNJeXfnXwP9r3zeup5', 'obama@usa', '2026-03-15 21:28:59', 'a28c876d-38c2-411d-9977-6ec3723fa023.jpg'),
(19, 'MSNBC', 'XZQI9lrlb6lQxfhAowdjjH1kTMeyIopoi2wHZ+pJjTCsuk81TcQ5lK0IsY1/NEUk', 'msnbc@gmail.com', '2026-03-15 21:54:31', '96743c21-6594-4b99-b61d-6aa1ff23b036.png'),
(20, 'shit@news', 'J3PV6RAPKXPOnpBcQiGWbHvxDi9ye03rWNvVRWioNdDnO6Ny4WxYc9zGNNEQrnx1', 'shitnews@usa', '2026-03-16 22:27:21', '48720c5b-fc75-4a58-87bd-a7eb96635dff.jpg'),
(21, 'Angela_Mergel', 'WKD99FT17NSi0u6waojZe5+GqhEEJ1ypeG43p6pvp+plUxF4z6Z6tT/YamW6l9fF', 'angela.merkel@deutschland', '2026-03-15 22:18:23', 'd6da40fa-496d-4a13-b36d-78ac80be839f.jpg'),
(22, 'Welsing', 'tRwYvKG7fziaLlDgiLTGdT6mIS/KpzYGpdM283t13IUBG557pE/pECYGtIDjI+81', 'wels.van@welsingen', '2026-03-15 23:07:43', 'b0cfc97d-8bdb-4c1b-97ad-706b9d4649e5.jpg'),
(23, 'just', 'QhLYG91uNCnOFDKYeuUrzWsnhlf4ep2YIiVOiA0ulkc/NiMUDE6//MSmfNy6/Wg+', 'just@en.biber', '2026-03-17 09:23:03', '3f7052b4-cbf5-4990-98d0-1d8d2c32fd19.jpg');

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
  ADD PRIMARY KEY (`nutzerId`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `beitrag`
--
ALTER TABLE `beitrag`
  MODIFY `beitragid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=54;

--
-- AUTO_INCREMENT für Tabelle `bild`
--
ALTER TABLE `bild`
  MODIFY `bildid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=82;

--
-- AUTO_INCREMENT für Tabelle `chat`
--
ALTER TABLE `chat`
  MODIFY `chatId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT für Tabelle `chatnachricht`
--
ALTER TABLE `chatnachricht`
  MODIFY `nachrichtId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=47;

--
-- AUTO_INCREMENT für Tabelle `kommentar`
--
ALTER TABLE `kommentar`
  MODIFY `kommentarid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT für Tabelle `nutzer`
--
ALTER TABLE `nutzer`
  MODIFY `nutzerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

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
