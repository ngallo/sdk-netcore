

using Permguard.AzReq;
using Policydecisionpoint;
using Action = Permguard.AzReq.Action;
using ContextResponse = Permguard.AzReq.ContextResponse;
using Entities = Permguard.AzReq.Entities;
using EvaluationResponse = Permguard.AzReq.EvaluationResponse;
using PolicyStore = Permguard.AzReq.PolicyStore;
using Principal = Permguard.AzReq.Principal;
using ReasonResponse = Permguard.AzReq.ReasonResponse;
using Resource = Permguard.AzReq.Resource;
using Subject = Permguard.AzReq.Subject;

namespace Permguard.Grpc
{
    internal static class MapService
    {
        private static Policydecisionpoint.PolicyStore? MapPolicyStoreToGrpcPolicyStore(PolicyStore? policyStore)
        {
            if (policyStore == null)
            {
                return null;
            }

            return new Policydecisionpoint.PolicyStore
            {
                ID = policyStore.Id,
                Kind = policyStore.Kind
            };
        }

        private static Policydecisionpoint.Principal? MapPrincipalToGrpcPrincipal(Principal? principal)
        {
            if (principal == null)
            {
                return null;
            }

            return new Policydecisionpoint.Principal
            {
                ID = principal.Id,
                Type = principal.Type,
                Source = string.IsNullOrEmpty(principal.Source) ? null : principal.Source
            };
        }

        private static Policydecisionpoint.Entities? MapEntitiesToGrpcEntities(Entities? entities)
        {
            if (entities == null)
            {
                return null;
            }

            var target = new Policydecisionpoint.Entities
            {
                Schema = entities.Schema
            };
            var results = Converter.ToRepeatedField(entities.Items);
            foreach(var result in results)
            {
                target.Items.Add(result);
            }
            
            return target;
        }

        private static Policydecisionpoint.Subject? MapSubjectToGrpcSubject(Subject? subject)
        {
            if (subject == null)
            {
                return null;
            }

            var target = new Policydecisionpoint.Subject
            {
                ID = subject.Id,
                Type = subject.Type,
                Source = string.IsNullOrEmpty(subject.Source) ? null : subject.Source,
                Properties = subject.Properties == null ? null : Converter.FromDictionary(subject.Properties)
            };

            return target;
        }

        private static Policydecisionpoint.Resource? MapResourceToGrpcResource(Resource? resource)
        {
            if (resource == null)
            {
                return null;
            }

            return new Policydecisionpoint.Resource
            {
                ID = resource.Id,
                Type = resource.Type,
                Properties = resource.Properties == null ? null : Converter.FromDictionary(resource.Properties)
            };
        }

        private static Policydecisionpoint.Action? MapActionToGrpcAction(Action? action)
        {
            if (action == null)
            {
                return null;
            }

            return new Policydecisionpoint.Action
            {
                Name = action.Name,
                Properties = Grpc.Converter.FromDictionary(action.Properties)
            };
        }

        private static EvaluationRequest? MapEvaluationToGrpcEvaluationRequest(Evaluation? evaluation)
        {
            if (evaluation == null)
            {
                return null;
            }

            var target = new EvaluationRequest
            {
                RequestID = evaluation.RequestId,
                Subject = evaluation.Subject == null ? null : MapSubjectToGrpcSubject(evaluation.Subject),
                Resource = evaluation.Resource == null ? null : MapResourceToGrpcResource(evaluation.Resource),
                Action = evaluation.Action == null ? null : MapActionToGrpcAction(evaluation.Action),
                Context = evaluation.Context == null ? null : Grpc.Converter.FromDictionary(evaluation.Context)
            };

            return target;
        }

        private static AuthorizationModelRequest MapAuthZModelToGrpcAuthorizationModelRequest(AZModel azModel)
        {
            var req = new AuthorizationModelRequest()
            {
                ZoneID = azModel.ZoneId
            };

            if (azModel.PolicyStore != null)
            {
                req.PolicyStore = MapPolicyStoreToGrpcPolicyStore(azModel.PolicyStore);
            }

            if (azModel.Principal != null)
            {
                req.Principal = MapPrincipalToGrpcPrincipal(azModel.Principal);
            }

            if (azModel.Entities != null)
            {
                req.Entities = MapEntitiesToGrpcEntities(azModel.Entities);
            }

            return req;
        }
        
        public static AuthorizationCheckRequest? MapAzRequestToGrpcAuthorizationCheckRequest(AZRequest? azRequest)
        {
            if (azRequest == null)
            {
                return null;
            }

            var req = new AuthorizationCheckRequest
            {
                RequestID = azRequest.RequestId,
                AuthorizationModel = MapAuthZModelToGrpcAuthorizationModelRequest(azRequest.AuthorizationModel)
            };

            if (azRequest.Subject != null)
            {
                req.Subject = MapSubjectToGrpcSubject(azRequest.Subject);
            }

            if (azRequest.Resource != null)
            {
                req.Resource = MapResourceToGrpcResource(azRequest.Resource);
            }

            if (azRequest.Action != null)
            {
                req.Action = MapActionToGrpcAction(azRequest.Action);
            }

            if (azRequest.Context != null)
            {
                req.Context = Converter.FromDictionary(azRequest.Context);
            }

            req.Evaluations.Add(azRequest.Evaluations.Select(MapEvaluationToGrpcEvaluationRequest).ToList());

            return req;
        }

        private static ReasonResponse? MapGrpcReasonResponseToReasonResponse(Policydecisionpoint.ReasonResponse? reasonResponse)
        {
            if (reasonResponse == null)
            {
                return null;
            }

            return new ReasonResponse
            {
                Code = reasonResponse.Code,
                Message = reasonResponse.Message
            };
        }

        private static ContextResponse? MapGrpcContextResponseToContextResponse(Policydecisionpoint.ContextResponse? contextResponse)
        {
            if (contextResponse == null)
            {
                return null;
            }

            var target = new ContextResponse
            {
                Id = contextResponse.ID,
                ReasonAdmin = contextResponse.ReasonAdmin == null ? null : MapGrpcReasonResponseToReasonResponse(contextResponse.ReasonAdmin),
                ReasonUser = contextResponse.ReasonUser == null ? null : MapGrpcReasonResponseToReasonResponse(contextResponse.ReasonUser)
            };

            return target;
        }

        private static EvaluationResponse? MapGrpcEvaluationResponseToEvaluationResponse(Policydecisionpoint.EvaluationResponse? evaluationResponse)
        {
            if (evaluationResponse == null)
            {
                return null;
            }

            var target = new EvaluationResponse
            {
                Decision = evaluationResponse.Decision,
                RequestId = evaluationResponse.RequestID ?? ""
            };

            if (evaluationResponse.Context != null)
            {
                target.Context = MapGrpcContextResponseToContextResponse(evaluationResponse.Context);
            }

            return target;
        }
        
        public static AZResponse? MapGrpcAuthorizationCheckResponseToAzResponse(AuthorizationCheckResponse? response)
        {
            if (response == null)
            {
                return null;
            }

            var target = new AZResponse
            {
                Decision = response.Decision,
                RequestId = response.RequestID ?? ""
            };

            if (response.Context != null)
            {
                target.Context = MapGrpcContextResponseToContextResponse(response.Context);
            }

            if (response.Evaluations != null)
            {
                target.Evaluations = response.Evaluations.Select(MapGrpcEvaluationResponseToEvaluationResponse).ToList()!;
            }

            return target;
        }
    }
}
