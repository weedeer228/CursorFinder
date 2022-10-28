﻿using System;
using System.Runtime.Serialization;

namespace CursorFinder.Models
{
    [DataContract]
    public class CursorPosition
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int XPos { get; set; }
        [DataMember]
        public int YPos { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public MouseActionType ActionType { get; set; }
    }
    [DataContract]
    public enum MouseActionType
    {
        [EnumMember]
        Shift = 0,
        [EnumMember]
        LeftButtonClick = 1,
        [EnumMember]
        RightButtonClick = 2,
        [EnumMember]
        MiddleButtonClick = 3,
    }
}
