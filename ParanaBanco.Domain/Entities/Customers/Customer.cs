using ParanaBanco.Domain.Entities.Phones;
using ParanaBanco.Domain.Entities.SeedWork;

namespace ParanaBanco.Domain.Entities.Customers
{
    public class Customer : EntityBase
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }

        public void ChangeEmailAddress(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}
