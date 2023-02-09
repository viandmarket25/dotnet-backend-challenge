-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Feb 09, 2023 at 09:24 PM
-- Server version: 10.4.21-MariaDB
-- PHP Version: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dotnet_library`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `show_issued` ()  begin
select DISTINCT * from `issued_books`,`books` where books.BOOK_ID=issued_books.BOOK_ID;
end$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `show_issued_books` ()  begin
select DISTINCT * from `issued_books`,`books` where books.BOOK_ID=issued_books.BOOK_ID and books.IS_AVAILABLE=0;
end$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `books`
--

CREATE TABLE `books` (
  `BOOK_ID` int(11) NOT NULL,
  `ISBN` varchar(20) NOT NULL,
  `BOOK_NAME` varchar(100) NOT NULL,
  `BOOK_DESCRIPTION` text NOT NULL,
  `BOOK_COVER_URL` varchar(500) NOT NULL,
  `GENRE_ID` int(11) NOT NULL,
  `CREATED_BY` int(11) NOT NULL,
  `BOOK_AUTHOR` varchar(100) NOT NULL,
  `IS_AVAILABLE` int(11) NOT NULL,
  `IS_RESERVED` int(11) NOT NULL,
  `BOOK_SHELVE_ID` int(11) NOT NULL,
  `BOOK_EDITION` varchar(50) NOT NULL,
  `LIST_DATE` varchar(50) NOT NULL,
  `LIST_TIME` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `books`
--

INSERT INTO `books` (`BOOK_ID`, `ISBN`, `BOOK_NAME`, `BOOK_DESCRIPTION`, `BOOK_COVER_URL`, `GENRE_ID`, `CREATED_BY`, `BOOK_AUTHOR`, `IS_AVAILABLE`, `IS_RESERVED`, `BOOK_SHELVE_ID`, `BOOK_EDITION`, `LIST_DATE`, `LIST_TIME`) VALUES
(12, '978-0-13-603', 'Bad time shapings', 'story of people surviving through very tough times', 'https://network9.biz/judging-a-book-by-its-cover/', 1, 14, 'Author Dotnet', 0, 1, 1, 'First Edition', 'string', 'string');

-- --------------------------------------------------------

--
-- Table structure for table `genres`
--

CREATE TABLE `genres` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `genres`
--

INSERT INTO `genres` (`ID`, `NAME`) VALUES
(1, 'Fiction'),
(2, 'Science Fiction'),
(3, 'Fantasy'),
(4, 'Dystopian'),
(5, 'Action & Adventure'),
(6, 'Mystery');

-- --------------------------------------------------------

--
-- Table structure for table `issued_books`
--

CREATE TABLE `issued_books` (
  `ID` int(11) NOT NULL,
  `ISSUED_BY` int(11) NOT NULL,
  `ISSUED_TO` int(11) NOT NULL,
  `BOOK_ID` int(11) NOT NULL,
  `EXPIRY_DATE` varchar(50) NOT NULL,
  `DATE_ISSUED` varchar(50) NOT NULL,
  `TIME_ISSUED` varchar(50) NOT NULL,
  `RETURN_DATE` varchar(50) NOT NULL,
  `RETURN_TIME` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `issued_books`
--

INSERT INTO `issued_books` (`ID`, `ISSUED_BY`, `ISSUED_TO`, `BOOK_ID`, `EXPIRY_DATE`, `DATE_ISSUED`, `TIME_ISSUED`, `RETURN_DATE`, `RETURN_TIME`) VALUES
(3, 12, 12, 12, '2/16/2023', '2/10/2023', '1:48 AM', '2/16/2023', '8:00 AM');

-- --------------------------------------------------------

--
-- Table structure for table `notifications`
--

CREATE TABLE `notifications` (
  `ID` int(11) NOT NULL,
  `TITLE` varchar(100) NOT NULL,
  `BODY` text NOT NULL,
  `IS_READ` int(50) NOT NULL,
  `CUSTOMER_ID` int(11) NOT NULL,
  `DATE_` varchar(50) NOT NULL,
  `TIME_` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `reserved_books`
--

CREATE TABLE `reserved_books` (
  `ID` int(11) NOT NULL,
  `RESERVED_BY` int(11) NOT NULL,
  `RESERVED_FOR` int(11) NOT NULL,
  `BOOK_ID` int(11) NOT NULL,
  `RESERVE_EXPIRY_DATE` varchar(100) NOT NULL,
  `RESERVE_EXPIRY_TIME` varchar(50) NOT NULL,
  `RESERVE_DATE` varchar(100) NOT NULL,
  `RESERVE_TIME` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `shelves`
--

CREATE TABLE `shelves` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(200) NOT NULL,
  `DESCRIPTION` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `shelves`
--

INSERT INTO `shelves` (`ID`, `NAME`, `DESCRIPTION`) VALUES
(1, 'A-1', 'A-1 4'),
(2, 'A-1', 'A-1 5'),
(3, 'A-1', 'A-1 6'),
(4, 'A-1', 'A-1 7'),
(5, 'A-2', 'A-2 1'),
(6, 'A-2', 'A-2 2'),
(7, 'A-2', 'A-2 3'),
(8, 'A-2', 'A-2 4');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `ID` int(11) NOT NULL,
  `USER_OFFICIAL_ID` varchar(50) NOT NULL,
  `NAME` varchar(50) NOT NULL,
  `USERNAME` varchar(200) NOT NULL,
  `EMAIL` varchar(100) NOT NULL,
  `PASSWORD` text NOT NULL,
  `ADDRESS` varchar(100) NOT NULL,
  `ROLE` int(11) NOT NULL,
  `GENDER` varchar(11) NOT NULL,
  `DATE_` varchar(50) NOT NULL,
  `TIME_` varchar(50) NOT NULL,
  `PHONE_NUMBER` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`ID`, `USER_OFFICIAL_ID`, `NAME`, `USERNAME`, `EMAIL`, `PASSWORD`, `ADDRESS`, `ROLE`, `GENDER`, `DATE_`, `TIME_`, `PHONE_NUMBER`) VALUES
(12, '', 'bugga', 'bugga', 'bugga', '$2a$11$h1un/FdO3OdDODIiXUoKD.DM0imq1UY94benkLqCWYh4W4xoBpdSy', 'bugga', 10, 'female', 'string', 'string', '000000000'),
(14, '', 'admin', 'admin', 'admin', '$2a$11$CZo3G0jyRaUVtnUlVda3sO3Z0erQ6oikHQPv1lQNzox25CQ9oZFBq', 'string', 20, 'string', 'string', 'string', 'string'),
(15, '', 'string', 'test', 'string', '$2a$11$U8Pw8AOnPXEkrUhAWo4aUu3NdK9XCLAiEtdgftcGJYxdxXdqD6MmS', 'string', 10, 'string', 'string', 'string', 'string');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `books`
--
ALTER TABLE `books`
  ADD PRIMARY KEY (`BOOK_ID`),
  ADD KEY `fk_CRBY` (`CREATED_BY`),
  ADD KEY `gnr_id` (`GENRE_ID`),
  ADD KEY `shel_id` (`BOOK_SHELVE_ID`);

--
-- Indexes for table `genres`
--
ALTER TABLE `genres`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `issued_books`
--
ALTER TABLE `issued_books`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fk_isbsb` (`ISSUED_BY`),
  ADD KEY `fk_ISTO` (`ISSUED_TO`),
  ADD KEY `fk_Bookd_` (`BOOK_ID`);

--
-- Indexes for table `notifications`
--
ALTER TABLE `notifications`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `not_usr` (`CUSTOMER_ID`);

--
-- Indexes for table `reserved_books`
--
ALTER TABLE `reserved_books`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `res_bk_id` (`BOOK_ID`),
  ADD KEY `res_us_id` (`RESERVED_FOR`);

--
-- Indexes for table `shelves`
--
ALTER TABLE `shelves`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `books`
--
ALTER TABLE `books`
  MODIFY `BOOK_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `genres`
--
ALTER TABLE `genres`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `issued_books`
--
ALTER TABLE `issued_books`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `notifications`
--
ALTER TABLE `notifications`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `reserved_books`
--
ALTER TABLE `reserved_books`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `shelves`
--
ALTER TABLE `shelves`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `books`
--
ALTER TABLE `books`
  ADD CONSTRAINT `fk_CRBY` FOREIGN KEY (`CREATED_BY`) REFERENCES `users` (`ID`),
  ADD CONSTRAINT `gnr_id` FOREIGN KEY (`GENRE_ID`) REFERENCES `genres` (`ID`),
  ADD CONSTRAINT `shel_id` FOREIGN KEY (`BOOK_SHELVE_ID`) REFERENCES `shelves` (`ID`);

--
-- Constraints for table `issued_books`
--
ALTER TABLE `issued_books`
  ADD CONSTRAINT `fk_Bookd_` FOREIGN KEY (`BOOK_ID`) REFERENCES `books` (`BOOK_ID`),
  ADD CONSTRAINT `fk_ISTO` FOREIGN KEY (`ISSUED_TO`) REFERENCES `users` (`ID`),
  ADD CONSTRAINT `fk_isbsb` FOREIGN KEY (`ISSUED_BY`) REFERENCES `users` (`ID`),
  ADD CONSTRAINT `iss_bk` FOREIGN KEY (`BOOK_ID`) REFERENCES `books` (`BOOK_ID`),
  ADD CONSTRAINT `iss_to_id` FOREIGN KEY (`ISSUED_TO`) REFERENCES `users` (`ID`);

--
-- Constraints for table `notifications`
--
ALTER TABLE `notifications`
  ADD CONSTRAINT `not_usr` FOREIGN KEY (`CUSTOMER_ID`) REFERENCES `users` (`ID`);

--
-- Constraints for table `reserved_books`
--
ALTER TABLE `reserved_books`
  ADD CONSTRAINT `res_bk_id` FOREIGN KEY (`BOOK_ID`) REFERENCES `books` (`BOOK_ID`),
  ADD CONSTRAINT `res_us_id` FOREIGN KEY (`RESERVED_FOR`) REFERENCES `users` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
