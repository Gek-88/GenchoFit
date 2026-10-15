CREATE DATABASE IF NOT EXISTS genchofit CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE genchofit;

-- Схемата съвпада с GenchoFit.Data.ShopDbContext.
CREATE TABLE IF NOT EXISTS Products (
    Id INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Description VARCHAR(500) NOT NULL,
    Emoji LONGTEXT NOT NULL,
    PRIMARY KEY (Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS Users (
    Id INT NOT NULL AUTO_INCREMENT,
    Email VARCHAR(160) NOT NULL,
    Name VARCHAR(80) NOT NULL,
    PasswordHash LONGTEXT NOT NULL,
    CreatedAt DATETIME(6) NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE KEY UX_Users_Email (Email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Demo products. Safe to run repeatedly: duplicates are avoided by product name.
INSERT INTO Products (Name, Category, Price, Description, Emoji)
SELECT 'Whey протеин', 'Протеин', 49.90, 'Суроватъчен протеин с ванилов вкус – 1 кг.', '🥛'
WHERE NOT EXISTS (SELECT 1 FROM Products WHERE Name = 'Whey протеин');
INSERT INTO Products (Name, Category, Price, Description, Emoji)
SELECT 'Креатин монохидрат', 'Сила', 29.90, 'Чист креатин за ежедневна тренировка – 300 г.', '⚡'
WHERE NOT EXISTS (SELECT 1 FROM Products WHERE Name = 'Креатин монохидрат');
INSERT INTO Products (Name, Category, Price, Description, Emoji)
SELECT 'Мултивитамини', 'Витамини', 19.90, 'Базов комплекс от витамини и минерали – 60 капсули.', '💊'
WHERE NOT EXISTS (SELECT 1 FROM Products WHERE Name = 'Мултивитамини');
INSERT INTO Products (Name, Category, Price, Description, Emoji)
SELECT 'Предтренировъчен бустер', 'Енергия', 34.90, 'Енергия и фокус преди тренировка – 300 г.', '🔥'
WHERE NOT EXISTS (SELECT 1 FROM Products WHERE Name = 'Предтренировъчен бустер');
INSERT INTO Products (Name, Category, Price, Description, Emoji)
SELECT 'Омега 3', 'Здраве', 24.90, 'Рибено масло с EPA и DHA – 90 капсули.', '🐟'
WHERE NOT EXISTS (SELECT 1 FROM Products WHERE Name = 'Омега 3');
