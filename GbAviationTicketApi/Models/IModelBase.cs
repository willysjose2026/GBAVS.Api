namespace GbAviationTicketApi.Models
{
    public interface IModelBase
    {
        bool IsActive { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
    }
}
