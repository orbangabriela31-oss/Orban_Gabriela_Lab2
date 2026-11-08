using Microsoft.AspNetCore.Mvc.RazorPages;
using Orban_Gabriela_Lab2.Data;
namespace Orban_Gabriela_Lab2.Models;

public class BookCategoriesPageModel : PageModel
{
    public List<AssignedCategoryData> AssignedCategoryDataList = new();

    public void PopulateAssignedCategoryData(Orban_Gabriela_Lab2Context context, Book book)
    {
        var bookCategories = new HashSet<int>(
            (book.BookCategories ?? new List<BookCategory>()).Select(c => c.CategoryID));
        AssignedCategoryDataList = new List<AssignedCategoryData>();
        foreach (var cat in context.Category)
        {
            AssignedCategoryDataList.Add(new AssignedCategoryData
            {
                CategoryID = cat.ID,
                Name = cat.CategoryName,
                Assigned = bookCategories.Contains(cat.ID)
            });
        }
    }

    public void UpdateBookCategories(Orban_Gabriela_Lab2Context context,
        string[]? selectedCategories, Book bookToUpdate)
    {
        bookToUpdate.BookCategories ??= new List<BookCategory>();
        if (selectedCategories == null)
        {
            foreach (var bc in bookToUpdate.BookCategories.ToList())
                context.Remove(bc);
            return;
        }
        var selected = new HashSet<string>(selectedCategories);
        var current = new HashSet<int>(bookToUpdate.BookCategories.Select(c => c.CategoryID));
        foreach (var cat in context.Category)
        {
            if (selected.Contains(cat.ID.ToString()))
            {
                if (!current.Contains(cat.ID))
                    bookToUpdate.BookCategories.Add(new BookCategory { BookID = bookToUpdate.ID, CategoryID = cat.ID });
            }
            else if (current.Contains(cat.ID))
            {
                context.Remove(bookToUpdate.BookCategories.Single(i => i.CategoryID == cat.ID));
            }
        }
    }
}
