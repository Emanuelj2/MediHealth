namespace Backend.Models.DTOs
{
    public class CreateAppointmentDto
    {
        public Guid ProviderId { get; set; }
        public DateTime SlotDateTime { get; set; }
        public string VisitReason { get; set; }
    }
}
