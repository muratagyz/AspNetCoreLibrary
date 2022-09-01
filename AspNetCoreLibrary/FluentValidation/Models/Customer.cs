namespace FluentValidation.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public DateTime? BirthDay { get; set; }

    public IList<Address> Addresses { get; set; }
}