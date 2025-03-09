using System.Text.RegularExpressions;
using Policydecisionpoint;
using Google.Protobuf.WellKnownTypes;
using Permguard;
using Permguard.Grpc;
using Policydecisionpoint;

namespace Permguard
{
    public class MapService
    {
        public static Policydecisionpoint.PolicyStore MapPolicyStoreToGrpcPolicyStore(PolicyStore policyStore)
        {
            if (policyStore == null)
            {
                return null;
            }

            return new Policydecisionpoint.PolicyStore
            {
                ID = policyStore.ID,
                Kind = policyStore.Kind
            };
        }
        
        public static Policydecisionpoint.Principal MapPrincipalToGrpcPrincipal(Permguard.Principal principal)
        {
            if (principal == null)
            {
                return null;
            }

            return new Policydecisionpoint.Principal
            {
                ID = principal.ID,
                Type = principal.Type,
                Source = string.IsNullOrEmpty(principal.Source) ? null : principal.Source
            };
        }
        
        public static Policydecisionpoint.Entities MapEntitiesToGrpcEntities(Permguard.Entities entities)
        {
            if (entities == null)
            {
                return null;
            }

            var target = new Policydecisionpoint.Entities
            {
                Schema = entities.Schema
            };
            var results = Permguard.Grpc.Converter.ToRepeatedField(entities.Items);
            foreach(var result in results)
            {
                target.Items.Add(result);
            }
            
            return target;
        }

        public static Policydecisionpoint.Subject MapSubjectToGrpcSubject(Permguard.Subject subject)
        {
            if (subject == null)
            {
                return null;
            }

            var target = new Policydecisionpoint.Subject
            {
                ID = subject.ID,
                Type = subject.Type,
                Source = string.IsNullOrEmpty(subject.Source) ? null : subject.Source,
                Properties = subject.Properties == null ? null : Permguard.Grpc.Converter.FromDictionary(subject.Properties)
            };

            return target;
        }
        
        public static Policydecisionpoint.Resource MapResourceToGrpcResource(Permguard.Resource resource)
        {
            if (resource == null)
            {
                return null;
            }

            return new Policydecisionpoint.Resource
            {
                ID = resource.ID,
                Type = resource.Type,
                Properties = resource.Properties == null ? null : Permguard.Grpc.Converter.FromDictionary(resource.Properties)
            };
        }
        
        public static Policydecisionpoint.Action MapActionToGrpcAction(Permguard.Action action)
        {
            if (action == null)
            {
                return null;
            }

            return new Policydecisionpoint.Action
            {
                Name = action.Name,
                Properties = Permguard.Grpc.Converter.FromDictionary(action.Properties)
            };
        }
        
        public static Policydecisionpoint.EvaluationRequest MapEvaluationToGrpcEvaluationRequest(Permguard.Evaluation evaluation)
        {
            if (evaluation == null)
            {
                return null;
            }

            var target = new Policydecisionpoint.EvaluationRequest
            {
                RequestID = evaluation.RequestID,
                Subject = evaluation.Subject == null ? null : MapSubjectToGrpcSubject(evaluation.Subject),
                Resource = evaluation.Resource == null ? null : MapResourceToGrpcResource(evaluation.Resource),
                Action = evaluation.Action == null ? null : MapActionToGrpcAction(evaluation.Action),
                Context = evaluation.Context == null ? null : Permguard.Grpc.Converter.FromDictionary(evaluation.Context)
            };

            return target;
        }
        
        public static Policydecisionpoint.AuthorizationModelRequest MapAuthZModelToGrpcAuthorizationModelRequest(Permguard.AZModel azModel)
        {
            var req = new Policydecisionpoint.AuthorizationModelRequest()
            {
                ZoneID = azModel.ZoneID
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
        
        public static AuthorizationCheckRequest MapAZRequestToGrpcAuthorizationCheckRequest(Permguard.AZRequest azRequest)
        {
            if (azRequest == null)
            {
                return null;
            }

            var req = new AuthorizationCheckRequest
            {
                RequestID = azRequest.RequestID
            };

            if (azRequest.AuthorizationModel != null)
            {
                req.AuthorizationModel = MapAuthZModelToGrpcAuthorizationModelRequest(azRequest.AuthorizationModel);
            }

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
                req.Context = Permguard.Grpc.Converter.FromDictionary(azRequest.Context);
            }

            if (azRequest.Evaluations != null)
            {
                req.Evaluations.Add(azRequest.Evaluations.Select(MapEvaluationToGrpcEvaluationRequest).ToList());
            }

            return req;
        }
        
        public static Permguard.ReasonResponse MapGrpcReasonResponseToReasonResponse(Policydecisionpoint.ReasonResponse reasonResponse)
        {
            if (reasonResponse == null)
            {
                return null;
            }

            return new Permguard.ReasonResponse
            {
                Code = reasonResponse.Code,
                Message = reasonResponse.Message
            };
        }
        
        public static Permguard.ContextResponse MapGrpcContextResponseToContextResponse(Policydecisionpoint.ContextResponse contextResponse)
        {
            if (contextResponse == null)
            {
                return null;
            }

            var target = new Permguard.ContextResponse
            {
                ID = contextResponse.ID,
                ReasonAdmin = contextResponse.ReasonAdmin == null ? null : MapGrpcReasonResponseToReasonResponse(contextResponse.ReasonAdmin),
                ReasonUser = contextResponse.ReasonUser == null ? null : MapGrpcReasonResponseToReasonResponse(contextResponse.ReasonUser)
            };

            return target;
        }
        
        public static Permguard.EvaluationResponse MapGrpcEvaluationResponseToEvaluationResponse(Policydecisionpoint.EvaluationResponse evaluationResponse)
        {
            if (evaluationResponse == null)
            {
                return null;
            }

            var target = new Permguard.EvaluationResponse
            {
                Decision = evaluationResponse.Decision,
                RequestID = evaluationResponse.RequestID ?? ""
            };

            if (evaluationResponse.Context != null)
            {
                target.Context = MapGrpcContextResponseToContextResponse(evaluationResponse.Context);
            }

            return target;
        }
        
        public static AZResponse MapGrpcAuthorizationCheckResponseToAZResponse(AuthorizationCheckResponse response)
        {
            if (response == null)
            {
                return null;
            }

            var target = new AZResponse
            {
                Decision = response.Decision,
                RequestID = response.RequestID ?? ""
            };

            if (response.Context != null)
            {
                target.Context = MapGrpcContextResponseToContextResponse(response.Context);
            }

            if (response.Evaluations != null)
            {
                target.Evaluations = response.Evaluations.Select(evaluationResponse => MapGrpcEvaluationResponseToEvaluationResponse(evaluationResponse)).ToList();
            }

            return target;
        }
    }
}
