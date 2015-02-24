using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing
{
    internal class FuncList<TInput, TOutput>
    {
        private readonly int offset;
        private readonly List<Func<TInput, TOutput>> delegates = new List<Func<TInput, TOutput>>();

        public FuncList(params Func<TInput, TOutput>[] handlers)
        {
            foreach (Func<TInput, TOutput> handler in handlers)
            {
                delegates.Add(handler);
                offset++;
            }
        }

        public FuncList<TInput, TOutput> Add(Func<TInput, TOutput> handler)
        {
            Guard.NotNull(handler, "handler");
            delegates.Insert(delegates.Count - offset, handler);
            return this;
        }

        public TOutput Execute(TInput input)
        {
            TOutput output;
            foreach (Func<TInput, TOutput> handler in delegates)
            {
                output = handler(input);
                if (output != null)
                    return output;
            }

            return default(TOutput);
        }
    }
}
