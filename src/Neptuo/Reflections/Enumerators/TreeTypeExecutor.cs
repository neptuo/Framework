using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Implementation of <see cref="ITypeDeletegateCollection{TContext}"/> with support for independent branches.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class TreeTypeExecutor<TContext> : DefaultTypeExecutor<TContext>
    {
        private readonly List<TreeTypeExecutor<TContext>> branches = new List<TreeTypeExecutor<TContext>>();

        /// <summary>
        /// Creates new branch.
        /// </summary>
        /// <returns>New branch.</returns>
        public TreeTypeExecutor<TContext> AddNewBranch()
        {
            TreeTypeExecutor<TContext> branch = new TreeTypeExecutor<TContext>();
            branches.Add(branch);
            return branch;
        }

        public override void Handle(Type type, TContext context)
        {
            base.Handle(type, context);

            if (IsMatched(type, context))
            {
                foreach (TreeTypeExecutor<TContext> branch in branches)
                    branch.Handle(type, context);
            }
        }
    }
}
