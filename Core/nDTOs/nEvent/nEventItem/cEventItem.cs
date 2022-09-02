using Core.Enums;
using Core.nDTOs.nEvent.nEventItem.nUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.nDTOs.nEvent.nEventItem
{
    public class cEventItem
    {
        public Guid app;
        public EEventTypes type;
        public DateTime time;
        public bool isSucceeded;
        public Object meta;
        public cUser user;
        public Object attributes;
    }
}
