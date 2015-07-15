using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Implementation of <see cref="ITypeEnumerator{TContext}"/> with support for independent branches.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class TreeTypeEnumerator<TContext> : DefaultTypeEnumerator<TContext>
    {
        private readonly List<TreeTypeEnumerator<TContext>> branches = new List<TreeTypeEnumerator<TContext>>();

        /// <summary>
        /// Creates new branch.
        /// </summary>
        /// <returns>New branch.</returns>
        public TreeTypeEnumerator<TContext> AddNewBranch()
        {
            TreeTypeEnumerator<TContext> branch = new TreeTypeEnumerator<TContext>();
            branches.Add(branch);
            return branch;
        }

        public override void Handle(Type type, TContext context)
        {
            base.Handle(type, context);

            if (IsMatched(type, context))
            {
                foreach (TreeTypeEnumerator<TContext> branch in branches)
                    branch.Handle(type, context);
            }
        }
    }
}
