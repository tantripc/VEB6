using System.Linq.Expressions;
using System.Reflection;

namespace MiddlewareTool.Utility
{
    /// <summary>
    /// ExpressionUtility
    /// </summary>
    public class ExpressionUtility
    {
        /// <summary>
        /// ctror
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "")]
        public ExpressionUtility()
        {
        }

        /// <summary>
        /// Build Equal
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="propertyName">property Name</param>
        /// <param name="value">object value</param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> BuildEqual<TEntity>(string propertyName, object value) where TEntity : class
        {
            Expression<Func<TEntity, bool>> m_Expression = null;

            var m_TypeEntity = typeof(TEntity);
            var m_ParameterEntity = Expression.Parameter(m_TypeEntity, "e");
            var m_Property = Expression.Property(m_ParameterEntity, propertyName);
            var m_ConstantFalse = Expression.Constant(false);
            m_Expression = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(m_Property, m_ConstantFalse), new[] { m_ParameterEntity });

            return m_Expression;
        }

        /// <summary>
        /// Trans form
        /// </summary>
        /// <typeparam name="TSource">T Source</typeparam>
        /// <typeparam name="TTarget">T Target</typeparam>
        /// <param name="expression">expression</param>
        /// <returns></returns>
        public static Expression<Func<TTarget, bool>> Transform<TSource, TTarget>(Expression<Func<TSource, bool>> expression)
        {
            Expression<Func<TTarget, bool>> m_Expression = null;

            //parameter that will be used in generated expression
            var param = Expression.Parameter(typeof(TTarget));
            //visiting body of original expression that gives us body of the new expression
            var body = new Visitor<TTarget>(param).Visit(expression.Body);
            //generating lambda expression form body and parameter 
            //notice that this is what you need to invoke the Method_2
            m_Expression = Expression.Lambda<Func<TTarget, bool>>(body, param);

            return m_Expression;
        }


        /// <summary>
        /// Get Property Name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>        
        public static PropertyInfo GetPropertyName<T>(Expression<Func<T, object>> property)
        {
            LambdaExpression lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression unaryExpression)
            {
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)(lambda.Body);
            }

            return ((PropertyInfo)memberExpression.Member);
        }
    }

    public class Visitor<T> : ExpressionVisitor
    {
        readonly ParameterExpression _parameter;

        //there must be only one instance of parameter expression for each parameter 
        //there is one so one passed here
        public Visitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        //this method replaces original parameter with given in constructor
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }

        //this one is required because PersonData does not implement IPerson and it finds
        //property in PersonData with the same name as the one referenced in expression 
        //and declared on IPerson
        protected override Expression VisitMember(MemberExpression node)
        {
            Expression m_Expression = null;

            //only properties are allowed if you use fields then you need to extend
            // this method to handle them
            if (node.Member.MemberType != System.Reflection.MemberTypes.Property)
            {
                m_Expression = base.VisitMember(node);
            }
            else
            {
                //name of a member referenced in original expression in your 
                //sample Id in mine Prop
                var memberName = node.Member.Name;
                //find property on type T (=PersonData) by name
                var otherMember = typeof(T).GetProperty(memberName);
                //visit left side of this expression p.Id this would be p
                var inner = Visit(node.Expression);
                m_Expression = Expression.Property(inner, otherMember);
            }

            return m_Expression;
        }
    }
}
