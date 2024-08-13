-- Insert data into Category table
INSERT INTO `library-management-system`.`Category` (`Id`, `Name`) VALUES
('289ac3de-1cf3-4a0d-8795-f6e9b48b9ddb', 'Fiction'),
('8a4Id90a77-d9fc-4ce6-bfc3-dc94f72d3c30', 'Non-Fiction'),
('4cf3922a-76b7-4b5d-83e3-59b2bc2a779f', 'Science Fiction'),
('b4e3121c-2a13-4dde-a58e-a4619dfc8183', 'Fantasy'),
('f5cc7950-1491-467e-9745-58a30b1e8304', 'Mystery'),
('b5b51160-1354-4329-b9b8-adfa63ccb38b', 'Thriller'),
('72a17923-04a6-494b-9d3c-2c0cb161434c', 'Romance'),
('a6b1b4f1-66fb-4a6c-b006-610530f90561', 'Historical Fiction'),
('cfa08ae5-a95d-42b8-befc-16652b74b21f', 'Biography'),
('a4d6c663-1c5d-4197-8f50-258af84b6de0', 'Self-Help');

-- Insert data into Book table
INSERT INTO `library-management-system`.`Book` (`Id`, `Name`, `Author`, `Category_Id`) VALUES
('8e6bb877-d9d0-4506-aa37-6798b8b23ce9', 'The Lord of the Rings', 'J.R.R. Tolkien', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Fantasy')),
('50a77a02-c363-473a-a3ad-1959dd49c9cc', 'The Hitchhiker\'s Guide to the Galaxy', 'Douglas Adams', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Science Fiction')),
('2062c57a-a212-41b3-803e-85402f2d79b9', 'Pride and Prejudice', 'Jane Austen', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Romance')),
('f6b08a74-3cf4-49cd-a258-c8f7bad5bb92', 'The Da Vinci Code', 'Dan Brown', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Thriller')),
('ed269c50-f3e1-4dd9-b5b7-b11f0961cd0c', 'To Kill a Mockingbird', 'Harper Lee', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Fiction')),
('608562c5-0e28-4f15-bab7-3406173c3bed', 'The Book Thief', 'Markus Zusak', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Historical Fiction')),
('37ad38a5-0509-4472-ac2f-3df5b3aa5ec7', 'And Then There Were None', 'Agatha Christie', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Mystery')),
('e215ec55-48af-44ff-b671-1bcf8a3b00a2', '1984', 'George Orwell', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Science Fiction')),
('2c1541b8-3a93-47bf-9ab7-fbe807bdf68d', 'The Great Gatsby', 'F. Scott Fitzgerald', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Fiction')),
('2062c57a-a212-41b3-803e-85402f2d79b9', 'Steve Jobs', 'Walter Isaacson', (SELECT Id FROM `library-management-system`.`Category` WHERE Name = 'Biography'));