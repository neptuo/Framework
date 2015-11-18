﻿using Neptuo.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public class EventData
    {
        public int Index { get; private set; }

        public EventData(int index)
        {
            Index = index;
        }
    }
}
