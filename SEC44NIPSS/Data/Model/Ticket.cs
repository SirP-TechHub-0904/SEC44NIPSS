using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Ticket
    {
        public long Id { get; set; }
        public string Priority { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public bool Closed { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ClosedTime { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string StudyGroup { get; set; }
        public string RequestedBy { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Details { get; set; }
        public string HouseOfficeNumber { get; set; }
        public string Image { get; set; }

        public string Stages { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public long? ForwardedToId { get; set; }
        public Profile ForwardedTo { get; set; }
        public DateTime ForwardedToTime { get; set; }

        public long? ReceivedAndPassToId { get; set; }
        public Profile ReceivedAndPassTo { get; set; }
        public string NoteByReceivedAndPassTo { get; set; }
        public DateTime ReceivedAndPassToTime { get; set; }



        public long? ApprovedById { get; set; }
        public Profile ApprovedBy { get; set; }
        public string NoteApprovedBy { get; set; }
        public DateTime ApprovedByTime { get; set; }

        public long? JobCompletionCertifiedById { get; set; }
        public Profile JobCompletionCertifiedBy { get; set; }
        public string NoteJobCompletionCertifiedBy { get; set; }
        public DateTime JobCompletionCertifiedByTime { get; set; }
        public string JobCompletionCertifiedBySignature { get; set; }
        

        public ICollection<TicketResponse> TicketResponses { get;set;}
        public ICollection<TicketRequirement> TicketRequirements { get;set; }
        public ICollection<TicketStaff> TicketStaff { get;set; }

    }
}
