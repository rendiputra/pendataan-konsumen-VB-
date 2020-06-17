-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 17 Jun 2020 pada 09.21
-- Versi server: 10.4.6-MariaDB
-- Versi PHP: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbpendataan`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `tkonsumen`
--

CREATE TABLE `tkonsumen` (
  `id` int(20) NOT NULL,
  `Tanggal` varchar(20) NOT NULL,
  `Nama_Konsumen` varchar(100) NOT NULL,
  `Alamat_Konsumen` varchar(400) NOT NULL,
  `Nama_Marketing` varchar(100) NOT NULL,
  `Nama_Teknisi` varchar(300) NOT NULL,
  `Laba_Antena` varchar(100) NOT NULL,
  `Laba_Parabola` varchar(100) NOT NULL,
  `Laba_CCTV` varchar(100) NOT NULL,
  `Laba_Petir` varchar(100) NOT NULL,
  `Total_Laba` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tkonsumen`
--

INSERT INTO `tkonsumen` (`id`, `Tanggal`, `Nama_Konsumen`, `Alamat_Konsumen`, `Nama_Marketing`, `Nama_Teknisi`, `Laba_Antena`, `Laba_Parabola`, `Laba_CCTV`, `Laba_Petir`, `Total_Laba`) VALUES
(1, '14/01/2019', 'Rendi Putra Pradana', 'Perum Bekasi Elok 2, desa Jejalen Jaya,Tambun Utara', 'Hanif', 'Sutrisno', '100000', '200000', '0', '0', 300000),
(3, '14/01/2019', 'Rayhan Nasuha', 'Bogor Jawa Barat', 'M Hanif', 'Sutarjo,  Parmaji', '100000', '500000', '0', '0', 600000),
(5, '14/01/2019', 'Yogga Fadhilah', 'Perumahan Bekasi Elok 2, Tambun Utara', 'M Hanif', 'Sutarjo,  Parmaji', '100000', '100000', '0', '0', 200000),
(7, '14/01/2019', 'Yoga Alfiyansyah', 'Perumahan Bekasi Elok 2 BLok E 2/No 3,Tambun Utara, Bekasi, Jawa  Barat', 'Hanif', 'Sutarjo', '100000', '0', '0', '0', 100000),
(9, '14/01/2019', 'Pandu Nurwiyansyah', 'Perum Bekasi Elok 2', 'M Hanif', 'Sutrisno', '100000', '100000', '0', '0', 200000),
(10, '16/01/2019', 'Rendi Putra Pradana', 'Perum Bekasi Elok 2', 'M Hanif', 'Parmaji', '100000', '100000', '0', '0', 200000),
(11, '24/01/2019', 'Rendi Putra Pradana', 'Perum Bekasi Elok 2 ', 'M Hanif', 'Sutarjo,  Parmaji', '100000', '100000', '0', '0', 200000),
(17, '06/02/2019', 'R Putra Pradana', 'Tambun Utara - Bekasi', 'M Hanif', 'Sutarjo,  Parmaji', '50000', '0', '0', '0', 50000),
(18, '12/03/2019', 'Aramda Kasil', 'Bekasi - Tambun', 'M Hanif', 'Sutarjo,  Parmaji', '50000', '0', '0', '0', 50000),
(19, '13/03/2019', 'Rendi Putra', 'Bekasi Elok 2 Blok e2', 'Hanif', 'Sutarjo,  Parmaji', '100000', '500000', '400000', '0', 1000000),
(20, '29/07/2019', 'wrwer', 'fewfef', 'Hanif', 'Parjo,  Sutrisno', '100000', '300000', '200000', '800000', 1400000),
(21, '29/07/2019 20.02.51', 'test', 'test', 'Hanif', 'Sutarjo,  Parmaji', '600000', '1200000', '400000', '800000', 3000000);

-- --------------------------------------------------------

--
-- Struktur dari tabel `tlogin`
--

CREATE TABLE `tlogin` (
  `id` int(10) NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` varchar(30) NOT NULL,
  `gambar` varchar(1000) NOT NULL,
  `level` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tlogin`
--

INSERT INTO `tlogin` (`id`, `username`, `password`, `gambar`, `level`) VALUES
(1, 'rendi', 'rendi', 'D:\\GALERI\\Dokumen 9.8\\DSC_0344.JPG', 1),
(2, 'putra', 'putra', 'C:\\Users\\Win10\\Downloads\\Compressed\\bahan\\196207191988031001.jpg', 1),
(3, 'admin', 'admin', 'C:\\Users\\Win10\\Downloads\\Compressed\\bahan\\196510311994031002.jpg', 0),
(5, 'hanif', 'hanif', 'C:\\Users\\Win10\\Downloads\\Compressed\\bahan\\196407251986031002 (1).jpg', 0);

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `tkonsumen`
--
ALTER TABLE `tkonsumen`
  ADD PRIMARY KEY (`id`);

--
-- Indeks untuk tabel `tlogin`
--
ALTER TABLE `tlogin`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `tkonsumen`
--
ALTER TABLE `tkonsumen`
  MODIFY `id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT untuk tabel `tlogin`
--
ALTER TABLE `tlogin`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
