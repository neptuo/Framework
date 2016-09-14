using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpKit.UnobtrusiveFeatures.Expressions
{
    /// <summary>
    /// Cache pro přístup k metodám, které mají mezi parametry System.Linq.Expressions.Expression.
    /// </summary>
    public class ExpressionCache
    {
        List<ExpressionCacheItem> items = new List<ExpressionCacheItem>();

        public void Add(ExpressionCacheItem item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Vrací registraci z cache pro metodu se jménem <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Jméno metody (aplikuje se též pro jsclr jména metod).</param>
        /// <returns>Registraci z cache pro metodu.</returns>
        public IEnumerable<ExpressionCacheItem> GetByMethodName(string name)
        {
            foreach (ExpressionCacheItem item in items)
            {
                if (item.Method.Name == name || name.StartsWith(item.Method.Name + "$$") || name.StartsWith(item.Method.Name + "$1") || name.StartsWith(item.Method.Name + "$2"))
                    yield return item;
            }
        }
    }
}
 