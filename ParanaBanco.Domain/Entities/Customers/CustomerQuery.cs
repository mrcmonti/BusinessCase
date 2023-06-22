using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Domain.Entities.Customers
{
    public static class CustomerQuery
    {
        public static IQueryable<Customer> WhereEmailAddress(this IQueryable<Customer> query, string emailAddress)
        {
            if (String.IsNullOrWhiteSpace(emailAddress))
                return query;

            return query.Where(x => emailAddress.Equals(x.EmailAddress));
        }

        public static IQueryable<Customer> WherePhone(this IQueryable<Customer> query, string phone)
        {
            if (String.IsNullOrWhiteSpace(phone))
                return query;

            return query.Where(x => x.Phones.Any(p => (p.AreaCode + p.PhoneNumber).Equals(phone)));
        }
    }
}
