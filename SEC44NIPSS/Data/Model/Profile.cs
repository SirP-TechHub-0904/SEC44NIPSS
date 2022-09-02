using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Profile
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string AccountRole { get; set; }
        public string LoginRole { get; set; }
        public string OfficialRole { get; set; }
        public string Sponsor { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string AltPhoneNumber { get; set; }
        public string PsNumber { get; set; }
        public bool PsChange { get; set; }
        public int SortOrder { get; set; }
        public string ChangeDate { get; set; }
        public DateTime DateRegistered { get; set; }
        public string PXI { get; set; }
        public string ParticipanPicture { get; set; }
        public string ProfilePhoto { get; set; }
        public OfficialRoleStatus OfficialRoleStatus { get; set; }
        public ProfileUpdateLevel ProfileUpdateLevel { get; set; }
        public string ParticipantPhoto { get; set; }
        public string StudyGroupRole { get; set; }
        public string ResidenceAddress { get; set; }
        public string StateOfOrigin { get; set; }
        public string LGA { get; set; }
        public string OfficeAddress { get; set; }
        public string ShortProfile { get; set; }
        public bool ProfileUpdateFirstTime { get; set; }
        public bool ProfileUpdatePictureFirstTime { get; set; }
        public bool DontShow { get; set; }
        public string AboutProfile { get; set; }
        public bool Sent { get; set; }
        public bool IsParticipant { get; set; }
        public bool IsExecutive { get; set; }
        public string Roles { get; set; }

        public string ProfileHandler { get; set; }

        public ICollection<Gallery> MyGallery { get; set; }
        public virtual StudyGroupMemeber StudyGroupMemeber { get; set; }

        public virtual SecParticipant Participant { get; set; }

        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }
    }
}
