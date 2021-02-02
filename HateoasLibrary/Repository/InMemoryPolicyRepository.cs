using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using static HateoasLibrary.Repository.InMemoryConditionRepository;

namespace HateoasLibrary.Repository
{
    public sealed class InMemoryPolicyRepository
    {

        public static ConcurrentBag<Policy> InMemoryPolicies { get; } = new ConcurrentBag<Policy>();

		public abstract class Policy
		{
			protected Policy(Type type, Expression expression, string name = null, [CallerMemberName] string memberName = null)
			{
				if (string.IsNullOrWhiteSpace(name))
				{
					name = memberName;
				}

				this.TypeResponse = type ?? throw new ArgumentNullException(nameof(type));
				this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
				this.Name = name;
			}

			protected Policy(Type typeResponse, Type typeRequest, Expression expression, string name = null, [CallerMemberName] string memberName = null)
			{
				if (string.IsNullOrWhiteSpace(name))
				{
					name = memberName;
				}

				this.TypeResponse = typeResponse ?? throw new ArgumentNullException(nameof(typeResponse));
				this.TypeRequest = typeRequest ?? throw new ArgumentNullException(nameof(typeResponse));
				this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
				this.Name = name;
			}

			public Condition Condition { get; set; }
			public Type TypeResponse { get; }
			public Type TypeRequest { get; }
			public Expression Expression { get; }

			public string Name { get; }

			public string Method { get; set; }
		}


		internal class ExternalPolicy : Policy
        {
            public ExternalPolicy(Type typeResponse, Expression expression,  string name = null, [CallerMemberName] string memberName = null) 
				: base(typeResponse, expression, name, memberName)
            {

            }

			public ExternalPolicy(Type typeResponse, Type typeRequest, Expression expression, string name = null, [CallerMemberName] string memberName = null)
				: base(typeResponse, typeRequest, expression, name, memberName)
			{

			}

		}

    }
}
