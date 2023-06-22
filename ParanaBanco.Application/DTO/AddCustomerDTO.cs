namespace ParanaBanco.Application.DTO
{
    public class AddCustomerDTO
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public virtual IEnumerable<AddPhoneDTO> Phones { get; set; }
    }
}
