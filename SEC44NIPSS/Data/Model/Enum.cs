using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SEC44NIPSS.Data.Model
{
    public enum ReminderTime
    {
        [Description("NONE")]
        NONE = 0,
        [Description("10 min")]
        Min10 = 2,
        [Description("15 Min")]
        Min15 = 3,
        [Description("20 Min")]
        Min20 = 4,
        [Description("30 Min")]
        Min30 = 5,
        [Description("1 Hr")]
        Hr1 = 5,
    }
    public enum PlannerType
    {
        [Description("NONE")]
        NONE = 0,
        [Description("Call Reminder")]
        CallReminder = 2,
        [Description("Send Customised SMS to Someone")]
        SendCustomisedSMStoSomeone = 3,
        [Description("Text Reminder")]
        TextReminder = 4,
        [Description("Reminder")]
        Reminder = 5,

    }
    public enum RecurrenceType
    {
        [Description("NONE")]
        NONE = 0,
        [Description("Dialy")]
        Dialy = 2,
        [Description("Weekly")]
        Weekly = 3,
        [Description("Yearly")]
        Yearly = 4,
        [Description("Monthly")]
        Monthly = 5,
        [Description("Hourly")]
        Hourly = 6,
        [Description("Minute")]
        Minute = 7,



    }
    public enum VotingType
    {
        [Description("NONE")]
        NONE = 0,
        [Description("Project")]
        Project = 2,
        [Description("Contribution")]
        Contribution = 3,
        [Description("Dues")]
        Dues = 4,
    }

    public enum EmailPhoneStatus
    {
        [Description("No")]
        No = 0,
        [Description("Required")]
        Required = 2,
        [Description("Optional")]
        Optional = 3,
    }
    public enum EventType
    {

        [Description("NONE")]
        NONE = 0,
        [Description("Timetable")]
        Timetable = 2,
        [Description("Event")]
        Event = 3,

    }
    public enum OptionType
    {

        [Description("NONE")]
        NONE = 0,
        [Description("ShortNote")]
        ShortNote = 2,
        [Description("LongNote")]
        LongNote = 3,
        [Description("YesNo")]
        YesNo = 4,
        [Description("FourOption")]
        FourOption = 5,
        [Description("FiveOption")]
        FiveOption = 6,

        [Description("MultipleOption")]
        MultipleOption = 7,

    }
    public enum PostFileType
    {

        [Description("IMG")]
        IMG = 0,
        [Description("VIDEO")]
        VIDEO = 2,

    }
    public enum DocType
    {

        [Description("PDF")]
        PDF = 0,
        [Description("DOC")]
        DOC = 2,
        [Description("PowerPoint")]
        PowerPoint = 3,

    }

    public enum ProfileUpdateLevel
    {

        [Description("ONE")]
        ONE = 0,
        [Description("TWO")]
        TWO = 2,
        [Description("THREE")]
        THREE = 3,

    }

    public enum ContentStatus
    {

        [Description("NONE")]
        NONE = 0,
        [Description("IsBold")]
        IsBold = 2,
        [Description("IsRed")]
        IsRed = 3,

    }

    public enum ContentType
    {

        [Description("NONE")]
        NONE = 0,
        [Description("IsSingle")]
        IsSingle = 2,

    }

    public enum NotificationStatus
    {
        [Description("NotDefind")]
        NotDefind = 0,
        [Description("Sent")]
        Sent = 1,

        [Description("NotSent")]
        NotSent = 2,


    }
    public enum NotificationType
    {
        [Description("NotDefind")]
        NotDefind = 0,
        [Description("SMS")]
        SMS = 1,

        [Description("Email")]
        Email = 2


    }
}