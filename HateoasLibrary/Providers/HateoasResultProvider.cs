using HateoasLibrary.Extensions;
using HateoasLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static HateoasLibrary.Repository.InMemoryConditionRepository;

namespace HateoasLibrary.Providers
{
    public class HateoasResultProvider : IHateoasResultProvider
    {

        private readonly IServiceProvider _serviceProvider;

        public HateoasResultProvider(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        public bool HasAnyPolicy(IActionResult actionResult, IList<ParameterDescriptor> parametersRequest, out ObjectResult objectResult)
        {
            //if (actionResult is ObjectResult result)
            //{
            //    objectResult = actionResult as ObjectResult;
            //    string resultType = objectResult.Value.GetType().FullName;

            //    if (HasAnyValidCondition(objectResult))
            //    {

            //        if (result.Value is IEnumerable<object> collection)
            //        {
            //            resultType = collection
            //                .Select(v => v.GetType().FullName)
            //                .Distinct()
            //                .Single();
            //        }

            //        return InMemoryPolicyRepository.InMemoryPolicies
            //            .Where(p => p.TypeResponse.FullName.Equals(resultType) 
            //            && (parametersRequest.Any( req => req.ParameterType.FullName == p.TypeRequest?.FullName) ||
            //            p.TypeRequest == null
            //            )).Any();

            //    }

            //}
            objectResult = default;
            return false;
        }

        public bool HasAnyValidCondition(IActionResult actionResult, out ObjectResult objectResult)
        {
            if (actionResult is ObjectResult result)
            {
                objectResult = actionResult as ObjectResult;
                var objectResultQuery = actionResult as ObjectResult
                    ;
                string resultType = objectResult.Value.GetType().FullName;

                var condition = InMemoryConditionRepository.InMemoryCondition.Where(p => p.Name.Equals(objectResultQuery.Value.GetType().FullName)).Single();

                if (condition != null)
                {
                    ConditionModelResponse response = new ConditionModelResponse();
                    response.Response = objectResult.Value;
                    return condition.Expression.Invoke(response);
                }

                return true;
            }
            else
            {
                objectResult = default;
                return false;
            }

            


        }

        private bool IsValidCondition(Condition condition, ObjectResult objectResult)
        {
            if (condition != null)
            {
                ConditionModelResponse response = new ConditionModelResponse();
                response.Response = objectResult.Value;
                return condition.Expression.Invoke(response);
            }

            return true;
        }

        public async Task<IActionResult> GetContentResultAsync(ObjectResult result, IList<ParameterDescriptor> parametersRequest)
        {
            var policies = GetFilteredPolicies(result, parametersRequest);
            if (!policies.Any())
            {
                return null;
            }

            var content = default(JsonResult);

            async Task<IList<object>> GetLinksAsync(object item)
            {
                var links = new List<object>();

                foreach (var policy in policies.Where(p => p != null))
                {
                    if (IsValidCondition(policy.Condition, result))
                    {
                        var lambdaResult = GetLambdaResult(policy.Expression, item, policy.TypeRequest);
                        var link = await GetPolicyLinkAsync(policy, lambdaResult).ConfigureAwait(false);
                        links.Add(link);
                    }
                }

                return links;
            }

            async Task<object> GetFinalJsonPayloadAsync(object item)
            {
                var links = await GetLinksAsync(item).ConfigureAwait(false);

                return await Task.FromResult(item.ToFinalPayload(links)).ConfigureAwait(false);
            }

            if (result.Value is IEnumerable<object> collection)
            {
                var links = await Task.WhenAll(collection.Select(item => GetFinalJsonPayloadAsync(item))).ConfigureAwait(false);
                var json = new List<object>(links);

                //LinkModelExtension<List<object>> data = new LinkModelExtension<List<object>>();
                //data.Data = json;

                content = new JsonResult(json);
            }
            else
            {
                var links = await GetFinalJsonPayloadAsync(result.Value).ConfigureAwait(false);
                content = new JsonResult(links);
            }

            return await Task.FromResult(content).ConfigureAwait(false);
        }

        private async Task<object> GetPolicyLinkAsync(InMemoryPolicyRepository.Policy policy, object model)
        {
            var uriProviderType = typeof(HateoasUriProvider<>).MakeGenericType(policy.GetType());
            var uriProvider = _serviceProvider.GetService(uriProviderType);

            var endpoint = (dynamic)uriProvider.GetType()
                .InvokeMember(
                    nameof(HateoasUriProvider<InMemoryPolicyRepository.Policy>.GenerateEndpoint),
                    BindingFlags.InvokeMethod,
                    null,
                    uriProvider,
                    new[] { policy, model });

            var link = new
            {
                Method = endpoint.Item1,
                Uri = endpoint.Item2,
                Relation = policy.Name
            };

            return await Task.FromResult(link).ConfigureAwait(false);
        }

        private object GetLambdaResult(Expression expression, object sourcePayload, object sourceRequest)
        {
            var lambdaExpression = (expression as LambdaExpression);

            if (lambdaExpression == null)
                return null;

            var body = lambdaExpression.Body;
            var parameter = lambdaExpression.Parameters[0];

            if (sourceRequest == null)
                return Expression.Lambda(body, parameter).Compile().DynamicInvoke(sourcePayload);
            else
                return Expression.Lambda(body, parameter).Compile().DynamicInvoke(sourcePayload, sourceRequest);
        }

        private bool GetConditionResult(Func<Type, bool> condition, object sourcePayload)
        {

            return condition.Invoke(sourcePayload.GetType());

        }


        private IEnumerable<InMemoryPolicyRepository.Policy> GetFilteredPolicies(ObjectResult result, IList<ParameterDescriptor> parametersRequest)
        {
            string resultType = result.Value.GetType().FullName;

            if (result.Value is IEnumerable<object> collection)
            {
                resultType = collection
                    .Select(v => v.GetType().FullName)
                    .Distinct()
                    .Single();
            }

            return InMemoryPolicyRepository.InMemoryPolicies
                        .Where(p => p.TypeResponse.FullName.Equals(resultType)
                        && (parametersRequest.Any(req => req.ParameterType.FullName == p.TypeRequest?.FullName) ||
                        p.TypeRequest == null
                        )).AsEnumerable();
        }
    }
}
