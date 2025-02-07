## Library Management System

### Problem Statement: Design a Library Management System

Design a Library Management System that allows users to borrow and return books. The system should support the following:

	Users can borrow books if available and return them after reading.
	Different types of users: Members and Librarians.
	Books have details such as title, author, ISBN, and availability status.
	A borrowing limit for members (e.g., max 5 books at a time).
	Due dates for borrowed books (e.g., 14-day borrowing period).
	Fine calculation for overdue books.
	Librarians can add or remove books from the catalog.
	The system should follow SOLID principles and appropriate design patterns.

	Great! With the assumption that each book has only one copy available at a time, the design simplifies since we don’t need to manage multiple copies of the same book.


##### Entities (Models)
Library

	Manages books and user transactions.

Book

	Contains details like title, author, ISBN, and availability status.
User (Abstract)

	Common base for both Member and Librarian.
Member (Inherits User)

	Can borrow and return books (max 5 at a time).
Librarian (Inherits User)

	Can add and remove books.
Transaction (Book Borrowing Record)

	Tracks borrowing, due date, and return status.
Fine Calculation

	If a book is returned late, fine is applied.

##### Users (Actors)
Member

	Borrows and returns books.
	Pays a fine if overdue.

Librarian

	Adds and removes books from the system.
	Manages users.