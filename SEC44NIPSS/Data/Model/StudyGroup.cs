using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class StudyGroup
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AKA { get; set; }
        public int SortNumber { get; set; }
        public string Photo { get; set; }
        public string DirectingStaffOne { get; set; }
        public string DirectingStaffTwo { get; set; }
        public long? AlumniId { get; set; }
        public Alumni Alumni { get; set; }
        public ICollection<StudyGroupMemeber> StudyGroupMemebers { get; set; }
        public ICollection<TourSubCategory> TourSubCategory { get; set; }
        public ICollection<Tour> Tours { get; set; }
        public ICollection<SecParticipant> SecParticipants { get; set; }
    }
}
