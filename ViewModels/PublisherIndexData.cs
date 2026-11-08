using Orban_Gabriela_Lab2.Models;
namespace Orban_Gabriela_Lab2.ViewModels;
public class PublisherIndexData
{
    public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>();
    public IEnumerable<Book> Books { get; set; } = new List<Book>();
}
