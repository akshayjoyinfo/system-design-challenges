using System;
using System.Collections.Generic;
using System.Linq;

// User Types
public abstract class User
{
    public string Name { get; }
    public int UserId { get; }

    protected User(string name, int userId)
    {
        Name = name;
        UserId = userId;
    }
}

public class Member : User
{
    private const int MaxBorrowLimit = 5;
    private List<Transaction> Transactions = new();

    public Member(string name, int userId) : base(name, userId) { }

    public bool BorrowBook(Book book, Library library)
    {
        if (Transactions.Count(t => !t.IsReturned) >= MaxBorrowLimit || !book.IsAvailable)
            return false;

        var transaction = new Transaction(book, this, DateTime.Now);
        Transactions.Add(transaction);
        book.IsAvailable = false;
        library.RecordTransaction(transaction);
        return true;
    }

    public bool ReturnBook(Book book, Library library, DateTime returnDate)
    {
        var transaction = Transactions.FirstOrDefault(t => t.Book == book && !t.IsReturned);
        if (transaction != null)
        {
            transaction.SetBookReceived(returnDate);
            library.ProcessReturn(transaction);
            return true;
        }
        return false;
    }
}

public class Librarian : User
{
    public Librarian(string name, int userId) : base(name, userId) { }

    public void AddBook(Library library, string title, string author, string isbn)
    {
        library.AddBook(new Book(title, author, isbn));
    }

    public void RemoveBook(Library library, Book book)
    {
        library.RemoveBook(book);
    }
}

// Book Class
public class Book
{
    public string Title { get; }
    public string Author { get; }
    public string ISBN { get; }
    public bool IsAvailable { get; set; }

    public int ReturnFee { get; } = 5;

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = true;
    }
}

public class Transaction
{
    public Book Book { get; set; }
    public Member Borrower { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime EstimatedReturnDate { get; set; }
    public DateTime? ActualReturnedDate { get; set; }
    public decimal FineAmount { get; set; }
    public bool IsReturned { get; set; }

    public Transaction(Book book, Member borrower, DateTime issueDate)
    {
        Book = book;
        Borrower = borrower;
        IssueDate = issueDate;
        EstimatedReturnDate = issueDate.AddDays(14);
        IsReturned = false;
    }

    public void SetBookReceived(DateTime actualReturnDate)
    {
        ActualReturnedDate = actualReturnDate;
        IsReturned = true;
        int daysLate = (ActualReturnedDate.Value - EstimatedReturnDate).Days;
        FineAmount = daysLate > 0 ? daysLate * Book.ReturnFee : 0;
        Book.IsAvailable = true;
    }
}

// Library Class
public class Library
{
    private List<Book> Books = new();
    private List<Transaction> Transactions = new();

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        Books.Remove(book);
    }

    public void RecordTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
    }

    public void ProcessReturn(Transaction transaction)
    {
        if (transaction.IsReturned && transaction.FineAmount > 0)
        {
            Console.WriteLine($"Book returned late! Fine: {transaction.FineAmount} EUR");
        }
    }
}

// Test Program
public class Program
{
    public static void Main()
    {
        Library library = new Library();
        Librarian librarian = new Librarian("Alice", 1);
        Member member = new Member("Bob", 2);

        librarian.AddBook(library, "C# Basics", "John Doe", "1234567890");
        Book book = new Book("C# Basics", "John Doe", "1234567890");

        if (member.BorrowBook(book, library))
        {
            Console.WriteLine($"{member.Name} borrowed {book.Title}");
        }
        else
        {
            Console.WriteLine("Book not available.");
        }

        DateTime returnDate = DateTime.Now.AddDays(16); // Late return
        if (member.ReturnBook(book, library, returnDate))
        {
            Console.WriteLine($"{member.Name} returned {book.Title}");
        }
    }
}
