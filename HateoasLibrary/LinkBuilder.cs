using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HateoasLibrary
{
	/// <summary>
	/// Allows link policy configuration.
	/// </summary>
	public sealed class LinkBuilder
	{
		/// <inheritdoc/>
		public LinkBuilder AddPolicy<T>(Action<PolicyBuilder<T>> modelSetup, Action<ConditionBuilder<T>> conditionSetup)
			where T : class, new ()
		{
			var policy = new PolicyBuilder<T>();
			var condition = new ConditionBuilder<T>();

			modelSetup?.Invoke(policy);
			conditionSetup?.Invoke(condition);

			return this;
		}

		public LinkBuilder AddPolicy<T>(Action<PolicyBuilder<T>> modelSetup)
	where T : class, new()
		{
			var policy = new PolicyBuilder<T>();

			modelSetup?.Invoke(policy);

			return this;
		}

		//public LinkBuilder AddPolicy<TResponse,TRequest>(Action<PolicyBuilder<TResponse, TRequest>> modelSetup, Action<ConditionBuilder<TResponse>> condition)
		//	where TResponse : class
		//where TRequest : class
		//{
		//	var policy = new PolicyBuilder<TResponse, TRequest>();
		//	var policy = new ConditionBuilder<TResponse>();

		//	modelSetup?.Invoke(policy);

		//	condition?.Invoke()

		//	return this;
		//}
	}
}
