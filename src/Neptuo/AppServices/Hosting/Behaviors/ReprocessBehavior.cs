﻿using Neptuo.ComponentModel.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Behaviors.Hosting
{
    public class ReprocessBehavior : IBehavior<object>
    {
        private readonly int count;

        public ReprocessBehavior()
            : this(3)
        { }

        public ReprocessBehavior(int count)
        {
            Guard.PositiveOrZero(count, "count");
            this.count = count;
        }

        public Task ExecuteAsync(object handler, IBehaviorContext context)
        {
            return ExecuteAsync(handler, context, count);
        }

        private Task ExecuteAsync(object handler, IBehaviorContext context, int remaingCount)
        {
            IBehaviorContext contextState = context.Clone();
            try
            {
                return context.NextAsync();
            }
            catch (Exception e)
            {
                remaingCount--;
                if (remaingCount > 0)
                {
                    Task result = ExecuteAsync(handler, contextState, remaingCount);
                    context.CustomValues = contextState.CustomValues;
                    return result;
                }
                else
                    throw e;
            }
        }
    }
}
